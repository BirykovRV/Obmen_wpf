using Obmen_wpf.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obmen_wpf.Model
{
    class AskuFtpModel
    {
        public static Ftp AskuServer = new Ftp
        {
            Uri = $"ftp://{Settings.Default.askuIp}/",
            Username = Settings.Default.askuLogin,
            Password = Settings.Default.askuPass
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
                    AskuServer.Upload(file.FullName, pathTo + file.Name);
                });
            }
            foreach (var dir in dirs)
            {
                Task.Factory.StartNew(() =>
                {
                    AskuServer.CreateDir($"{pathTo}/{dir.Name}");
                    StartUpload($"{pathFrom}{dir.Name}", $"{pathTo}/{dir.Name}/");
                });
            }
        }
    }
}
