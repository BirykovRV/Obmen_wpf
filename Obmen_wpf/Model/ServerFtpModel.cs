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
        private static Ftp Server = new Ftp
        {
            Uri = $"ftp://{Settings.Default.serverIp}/",
            Username = Settings.Default.serverLogin,
            Password = Settings.Default.serverPass
        };

        public static void StartUpload(string pathFrom, string pathTo)
        {

            DirectoryInfo info = new DirectoryInfo(pathFrom);
            FileInfo[] files = info.GetFiles();
            DirectoryInfo[] dirs = info.GetDirectories();

            foreach (var file in files)
            {
                Task.Factory.StartNew(() =>
                {
                    Server.Upload(file.FullName, pathTo + file.Name);
                });
            }
            foreach (var dir in dirs)
            {
                Task.Factory.StartNew(() =>
                {
                    Server.CreateDir($"{pathTo}/{dir.Name}");
                    StartUpload($"{pathFrom}{dir.Name}", $"{pathTo}/{dir.Name}/");
                });
            }
        }

        public static void StartDownload(string pathFrom, string pathTo)
        {

            Server.Download(pathFrom, pathTo);

        }
    }
}
