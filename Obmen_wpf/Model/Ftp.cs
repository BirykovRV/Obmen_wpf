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
        public string Uri { get; set; }
        public string Username { get; set; } 
        public string Password { private get; set; }

        //public Ftp(string uri, string login, string pass)
        //{
        //    Uri = uri;
        //    Username = login;
        //    Password = pass;
        //}

        /// <summary>
        /// Скачивание файлов с ftp
        /// </summary>
        /// <param name="remotePath">Путь к файлам на сервере</param>
        /// <param name="localPath">Путь куда скачивать файлы на локальном компьютере</param>
        public void Download(string remotePath, string localPath)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Uri + remotePath);
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
                    string[] tokens = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    // имя файла
                    string name = tokens[8];
                    // Тип файла ('d' - папка)
                    string category = tokens[0];
                    string combinedLocalPath = Path.Combine(localPath, name);
                    string fileUrl = remotePath + name;

                    if (category.StartsWith("d"))
                    {
                        if (!Directory.Exists(combinedLocalPath))
                        {
                            Directory.CreateDirectory(combinedLocalPath);
                        }

                        Download(fileUrl + "/", combinedLocalPath);
                    }
                    else
                    {
                        FtpWebRequest downloadRequest = (FtpWebRequest)WebRequest.Create(Uri + fileUrl);
                        downloadRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                        downloadRequest.Credentials = new NetworkCredential(Username, Password);

                        using (FtpWebResponse response = (FtpWebResponse)downloadRequest.GetResponse())
                        using (Stream responseStream = response.GetResponseStream())
                        using (Stream targetStream = File.Create(combinedLocalPath))
                        {
                            byte[] buffer = new byte[32768];
                            int bytesRead;
                            while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                targetStream.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Загрузка файлов на сервер
        /// </summary>
        /// <param name="localPath">Полный путь до файла включая его имя</param>
        /// <param name="remotePath">Путь к папке на сервере с именем файла</param>
        public void Upload(string localPath, string remotePath)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Uri + remotePath);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(Username, Password);

                using (FileStream uploadedFile = new FileStream(localPath, FileMode.Open, FileAccess.Read))
                {
                    byte[] byteBuffer = new byte[uploadedFile.Length];
                    uploadedFile.Read(byteBuffer, 0, byteBuffer.Length);
                    using (Stream writer = request.GetRequestStream())
                        writer.Write(byteBuffer, 0, byteBuffer.Length);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Создание папки на сервере
        /// </summary>
        /// <param name="remotePath">Название папки, которую создать на сервере</param>
        public void CreateDir(string remotePath)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Uri + remotePath);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential(Username, Password);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}
