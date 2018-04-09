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
        static void Main(string[] args)
        {
            Ftp server = new  Ftp("ftp://10.87.6.143/", "support", "trd19afo");

            var path = @"ToOPS/PostPay/DB/";
            var pathRar = @"E:\PostPay\DB\";

            server.Download(path, pathRar);

            //Console.WriteLine(Environment.ExpandEnvironmentVariables(path));
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
