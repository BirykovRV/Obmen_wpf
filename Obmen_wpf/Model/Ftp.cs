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
        public string Uri { get; }
        public string Username { get; } 
        public string Password { private get; set; }
        private FtpWebRequest ftpRequest = null;
        private FtpWebResponse ftpResponse = null;
        private Stream ftpStream = null;
        private int bufferSize = 2048;

        public Ftp(string uri, string login, string pass)
        {
            Uri = uri;
            Username = login;
            Password = pass;
        }

        /// <summary>
        /// Скачивание файлов с ftp
        /// </summary>
        /// <param name="remotePath">Путь к файлам на сервере</param>
        /// <param name="localPath">Путь куда скачивать файлы на локальном компьютере</param>
        public void Download(string remotePath, string localPath)
        {
            try
            {
                ftpRequest = (FtpWebRequest)WebRequest.Create(Uri + remotePath);
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                ftpRequest.Credentials = new NetworkCredential(Username, Password);

                List<string> lines = new List<string>();

                using (ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
                using (Stream listStream = ftpResponse.GetResponseStream())
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
                            byte[] buffer = new byte[bufferSize];
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
                /* Create an FTP Request */
                ftpRequest = (FtpWebRequest)WebRequest.Create(Uri + remotePath);
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(Username, Password);
                /* When in doubt, use these options */
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                /* Establish Return Communication with the FTP Server */
                ftpStream = ftpRequest.GetRequestStream();
                /* Open a File Stream to Read the File for Upload */
                FileStream uploadedFile = new FileStream(localPath, FileMode.Open, FileAccess.Read);
                /* Buffer for the Downloaded Data */
                byte[] byteBuffer = new byte[uploadedFile.Length];
                uploadedFile.Read(byteBuffer, 0, byteBuffer.Length);
                /* Upload the File by Sending the Buffered Data Until the Transfer is Complete */
                uploadedFile.Close();

                ftpStream.Write(byteBuffer, 0, byteBuffer.Length);
                ftpStream.Close();
                ftpRequest = null;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            return;
        }
        /// <summary>
        /// Создание папки на сервере
        /// </summary>
        /// <param name="remotePath">Название папки, которую создать на сервере</param>
        public void CreateDir(string remotePath)
        {
            try
            {
                ftpRequest = (FtpWebRequest)WebRequest.Create(Uri + remotePath);
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive = true;
                ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                ftpRequest.Credentials = new NetworkCredential(Username, Password);

                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                ftpResponse.Close();
                ftpRequest = null;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return;
        }
    }
}
