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
        public static object Year { get; set; } = null;
        public static object Number { get; set; } = 1;

        static void Main(string[] args)
        {
            //FTPClient server = new FTPClient("10.87.6.143", "support", "trd19afo");

            //var remoutePath = "/ToOPS/PostPay/DB/";
            //var localPath = @"E:\PostPay\DB";

            //var localFile = Directory.GetFiles(localPath).FirstOrDefault();
            //var file = server.DirectoryListSimple(remoutePath).FirstOrDefault();
            //var time = server.GetFileCreatedDateTime(remoutePath + file);

            //File.SetLastWriteTime(localFile, time);

            //Console.WriteLine(time);            

            //Console.WriteLine("Загрузка успешно завершена! Для выхода нажмите Enter.");
            //Console.WriteLine(Environment.ExpandEnvironmentVariables(path));
            //StartUpload(@"G:\Реестр коммунальных платежей", "200001/Реестр коммунальных платежей/");

            Console.ReadKey();
        }

        public override string ToString() => $"{Year}";


    }
}
