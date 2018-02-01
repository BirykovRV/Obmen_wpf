using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Obmen_wpf.Model;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Ftp ftp = new Ftp
            {
                Url = "ftp://10.87.6.143/",
                Username = "support",
                Password = "trd19afo"
            };

            string localPath = "D:\\TEST\\";
            string remotePath = "test/";

            DirectoryInfo info = new DirectoryInfo(localPath);
            FileInfo [] fileInfo = info.GetFiles();

            foreach (var file in fileInfo)
            {
                Task.Factory.StartNew(()=>
                {
                    Console.WriteLine($"Копирование:\t{file.FullName}\t{DateTime.Now}");
                    ftp.Upload($"{file.FullName}", remotePath + file.Name);
                });
            }
            Console.ReadKey();
        }        
    }
}
