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
            string url = "10.87.6.143";

            Ftp server = new Ftp
            {
                Url = $"ftp://{url}/",
                Username = "support",
                Password = "trd19afo"
            };

            string localPath = "D:\\TEST\\";
            string remotePath = "test/";
            server.CreateDir(remotePath + "1");
            DirectoryInfo info = new DirectoryInfo(localPath);
            FileInfo[] files = info.GetFiles();
            DirectoryInfo[] dirs = info.GetDirectories(localPath);


            foreach (var file in files)
            {
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Копирование:\t{file.FullName}\t{DateTime.Now}");
                    server.Upload($"{file.FullName}", remotePath + file.Name);
                });
            }
            foreach (var dir in dirs)
            {

            }
            Console.ReadKey();
        }        
    }
}
