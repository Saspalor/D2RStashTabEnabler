using System;
using System.IO;



namespace StashTabEnabler
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 1;
            if (args.Length == 0)
            {
                Console.WriteLine("You have to Drag & Drop your Shared Stash File onto this File in order to proceed.");
                return; // return if no file was dragged onto exe
            }
            Console.WriteLine("How many shared Stash Tabs shall be added? (4 max.):");
            int amnt_tabs = Convert.ToInt32(Console.ReadLine());
            if (amnt_tabs <= 4 && amnt_tabs > 0)
            {
                int amnt_added = 0;
                string path = Path.GetDirectoryName(args[0])
                   + Path.DirectorySeparatorChar
                   + Path.GetFileNameWithoutExtension(args[0])
                   + "_orig_backup" + Path.GetExtension(args[0]);

                if (File.Exists(path))
                {
                    string bakfilename = string.Format("{0}({1})", path, count++);
                    while (File.Exists(bakfilename))
                    {
                        bakfilename = string.Format("{0}({1})", path, count++);
                    }
                    File.Copy(args[0], bakfilename);
                }
                else
                {
                    File.Copy(args[0], path);
                }

                string strexepath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                string strworkpath = System.IO.Path.GetDirectoryName(strexepath);
                byte[] stashbytes = File.ReadAllBytes(strworkpath + "/stashtab.dat");
                while (amnt_added < amnt_tabs)
                {


                    using (var stream = new FileStream(args[0], FileMode.Append))
                    {
                        stream.Write(stashbytes, 0, stashbytes.Length);
                    }
                    amnt_added++;
                }
                Console.WriteLine("Done!");
            }
            else
            {
                Console.WriteLine("You have to choose a Number between 1-4");
            }
            Console.WriteLine("Press any Key to continue");
            Console.ReadLine();
        }
    }
}
