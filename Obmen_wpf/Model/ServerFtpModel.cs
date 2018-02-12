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
        private static Ftp server = new Ftp
        (
            $"ftp://{Settings.Default.serverIp}/",
            Settings.Default.serverLogin,
            Settings.Default.serverPass
        );

        private static ftp Server = new ftp(Settings.Default.serverIp, Settings.Default.serverLogin, Settings.Default.serverPass);

        public static void StartUpload(string pathFrom, string pathTo)
        {

            DirectoryInfo info = new DirectoryInfo(pathFrom);
            FileInfo[] files = info.GetFiles();
            DirectoryInfo[] dirs = info.GetDirectories();

            foreach (var file in files)
            {
                Task.Factory.StartNew(() =>
                {
                    Server.Upload(pathTo + file.Name, file.FullName);
                });
            }
            foreach (var dir in dirs)
            {
                Task.Factory.StartNew(() =>
                {
                    Server.CreateDirectory($"{pathTo}/{dir.Name}");
                    StartUpload($"{pathFrom}{dir.Name}", $"{pathTo}/{dir.Name}/");
                });
            }
        }

        public static void StartDownload(string pathFrom, string pathTo)
        {

            server.Download(pathFrom, pathTo);

        }
    }
}
