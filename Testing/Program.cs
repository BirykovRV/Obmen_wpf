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
            FTPClient server = new FTPClient("10.87.6.143", "support", "trd19afo");

            var remoutePath = "/ToOPS/PostPay/DB/";
            var localPath = @"E:\PostPay\DB";

            var localFile = Directory.GetFiles(localPath).FirstOrDefault();
            var file = server.DirectoryListSimple(remoutePath).FirstOrDefault();
            var time = server.GetFileCreatedDateTime(remoutePath + file);

            File.SetLastWriteTime(localFile, time);

            Console.WriteLine(time);



            Console.WriteLine("Загрузка успешно завершена! Для выхода нажмите Enter.");
            //Console.WriteLine(Environment.ExpandEnvironmentVariables(path));
            //StartUpload(@"G:\Реестр коммунальных платежей", "200001/Реестр коммунальных платежей/");

            Console.ReadKey();
        }
        public static void StartUpload(string pathFrom, string pathTo)
        {
            FTPClient server = new FTPClient("ftp://10.87.6.143/", "support", "trd19afo");

            string[] files = Directory.GetFiles(pathFrom, "*.*");
            string[] subDirs = Directory.GetDirectories(pathFrom);

            foreach (string file in files)
            {
                server.Upload(pathTo + Path.GetFileName(file), file);
            }

            foreach (string subDir in subDirs)
            {

                var list = server.DirectoryListDetailed(pathTo);

                for (int i = 0; i < list.Length - 1; i++)
                {
                    string[] tokens = list[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    // имя файла
                    string name = tokens[8];
                    string category = tokens[0];
                    if (category.StartsWith("d"))
                    {
                        if (name == Path.GetFileName(subDir))
                        {
                            StartUpload(subDir, pathTo + Path.GetFileName(subDir) + "/");
                        }
                        else
                        {
                            server.CreateDirectory(pathTo + Path.GetFileName(subDir));
                            StartUpload(subDir, pathTo + Path.GetFileName(subDir) + "/");
                        }
                    }

                }
            }
        }
    }
}
