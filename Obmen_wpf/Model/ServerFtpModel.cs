using Obmen_wpf.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Obmen_wpf.Model
{
    class ServerFtpModel
    {
        private FTPClient Server;

        public ServerFtpModel()
        {
            Server = new FTPClient(Settings.Default.serverIp + "/", Settings.Default.serverLogin, Settings.Default.serverPass);
        }

        public ServerFtpModel(string ip, string login, string pass)
        {
            Server = new FTPClient(ip, login, pass);
        }

        public void StartUpload(string pathFrom, string pathTo)
        {
            string[] files = Directory.GetFiles(pathFrom);
            string[] subDirs = Directory.GetDirectories(pathFrom);
            // полный список файлов и папок с фтп                        

            foreach (string file in files)
            {
                Server.Upload(pathTo + Path.GetFileName(file), file);
            }
            // subDirs - список папок на компе
            for (int i = 0; i < subDirs.Length; i++)
            {
                var list = Server.DirectoryListDetailed(pathTo);
                bool isSameFolder = false;

                for (int j = 0; j < list.Length; j++)
                {
                    // если это папка (d) и имя совпадает с именем из первого массива
                    if (list[j].StartsWith("d") && list[j].Contains(Path.GetFileName(subDirs[i])))
                    {
                        isSameFolder = true;
                        break;
                    }
                }
                if (!isSameFolder)
                {
                    //если совпадений имен не найдено в массиве list, то создаем папку subDirs[i]
                    Server.CreateDirectory(pathTo + Path.GetFileName(subDirs[i]));
                    StartUpload(subDirs[i], pathTo + Path.GetFileName(subDirs[i]) + "/");
                }
            }
        }

        public void StartDownload(string pathFrom, string pathTo)
        {
            Server.Download(pathFrom, pathTo);
        }

        public DateTime GetTime(string remoutePath)
        {
            if (remoutePath == null)
                throw new ArgumentNullException("Путь не может быть null");
            else if (String.IsNullOrWhiteSpace(remoutePath))
                throw new ArgumentException("Путь не может состоять из пробелов");

            var file = Server.DirectoryListSimple(remoutePath).FirstOrDefault();
            var time = Server.GetFileCreatedDateTime(remoutePath + file);

            return time;
        }

        public string[] GetDirList(string from)
        {            
            var list = Server.DirectoryListSimple(from);
            return list;
        }
    }
}
