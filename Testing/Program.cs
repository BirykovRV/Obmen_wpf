using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Obmen_wpf.Model;
using Obmen_wpf;

namespace Testing
{
    class Program
    {
        private static Object syncObject = new Object();
        private static void Write()
        {
            lock (syncObject)
            {
                Console.WriteLine("test");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start");

            Console.ReadKey();

            lock (syncObject)
            {
                Write();
            }
            //StartUpload(@"G:\Реестр коммунальных платежей", "200001/Реестр коммунальных платежей/");

            Console.ReadKey();
        }
        //private static void StartUpload(string pathFrom, string pathTo)
        //{
        //    ftp server = new ftp("10.87.6.143", "support", "trd19afo");

        //    string[] files = Directory.GetFiles(pathFrom, "*.*");
        //    string[] subDirs = Directory.GetDirectories(pathFrom);

        //    foreach (string file in files)
        //    {
        //        server.Upload(pathTo + Path.GetFileName(file), file);
        //    }

        //    foreach (string subDir in subDirs)
        //    {
        //        server.CreateDirectory(pathTo + Path.GetFileName(subDir));
        //        StartUpload(subDir, pathTo + Path.GetFileName(subDir) + "/");
        //    }
        //}
    }
}
