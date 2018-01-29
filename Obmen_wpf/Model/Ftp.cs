using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Obmen_wpf.Model
{
    public class Ftp
    {
        public string Url { get; set; } = "ftp://10.87.6.143/";
        public string Username { get; set; } = "support";
        public string Password { get; set; } = "trd19afo";        

        public Ftp(string url, string username, string password)
        {
            Url = url;
            Username = username;
            Password = password;
        }
        /// <summary>
        /// Скачивание файлов с ftp
        /// </summary>
        /// <param name="remotePath">Путь к файлам на сервере</param>
        /// <param name="localPath">Путь куда скачивать файлы на локальном компьютере</param>
        public void Download(string remotePath, string localPath)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Url + remotePath);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.Credentials = new NetworkCredential(Username, Password);

            List<string> lines = new List<string>();

            using (FtpWebResponse listResponse = (FtpWebResponse)request.GetResponse())
            using (Stream listStream = listResponse.GetResponseStream())
            using (StreamReader listReader = new StreamReader(listStream))
            {
                while (!listReader.EndOfStream)
                {
                    lines.Add(listReader.ReadLine());
                }
            }

            foreach (var line in lines)
            {
                string combinedLocalPath = Path.Combine(localPath, line);
                string fileUrl = Url + remotePath + line;
                Console.WriteLine(combinedLocalPath);
                string pattern = "/$";
                Regex regex = new Regex(pattern);
                //if (regex.IsMatch(fileUrl))
                //{
                //    if (!Directory.Exists(combinedLocalPath))
                //    {
                //        Directory.CreateDirectory(combinedLocalPath);
                //    }

                //    Download(fileUrl + "/", combinedLocalPath);
                //}
                //else
                //{
                    //FtpWebRequest downloadRequest = (FtpWebRequest)WebRequest.Create(fileUrl);
                    //downloadRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                    //downloadRequest.Credentials = new NetworkCredential(Username, Password);

                    //using (FtpWebResponse response = (FtpWebResponse)downloadRequest.GetResponse())
                    //using (Stream responseStream = response.GetResponseStream())
                    //using (Stream targetStream = File.Create(combinedLocalPath))
                    //{
                    //    byte[] buffer = new byte[256];
                    //    int bytesRead;
                    //    while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                    //    {
                    //        targetStream.Write(buffer, 0, bytesRead);
                    //    }
                    //}
                //}
            }

            Console.WriteLine("Загрузка и сохранение файла завершены");
            Console.Read();
        }
    }
}
