using Obmen;
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
        private ftp Server;

        public ServerFtpModel()
        {
            Server = new ftp(Settings.Default.serverIp, Settings.Default.serverLogin, Settings.Default.serverPass);
        }

        public ServerFtpModel(string ip, string login, string pass)
        {
            Server = new ftp(ip, login, pass);
        }

        public void StartUpload(string pathFrom, string pathTo)
        {         
            //DirectoryInfo info = new DirectoryInfo(pathFrom);
            //FileInfo[] files = info.GetFiles();
            //DirectoryInfo[] dirs = info.GetDirectories();

            //foreach (var file in files)
            //{
            //    Server.Upload(pathTo + file.Name, file.FullName);
            //}
            //foreach (var dir in dirs)
            //{                
            //    Server.CreateDirectory($"{pathTo}{dir.Name}");
            //    StartUpload($"{pathFrom}{dir.Name}", $"{pathTo}{dir.Name}/");
            //}

            string[] files = Directory.GetFiles(pathFrom, "*.*");
            string[] subDirs = Directory.GetDirectories(pathFrom);

            foreach (string file in files)
            {
                Server.Upload(pathTo + Path.GetFileName(file), file);
            }

            foreach (string subDir in subDirs)
            {
                Server.CreateDirectory(pathTo + Path.GetFileName(subDir));
                StartUpload(subDir, pathTo + Path.GetFileName(subDir) + "/");
            }
        }

        public void StartDownload(string pathFrom, string pathTo)
        {
            Server.Download(pathFrom, pathTo);
        }
    }
}
