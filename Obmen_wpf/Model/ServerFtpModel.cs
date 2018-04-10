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
            Server = new FTPClient(Settings.Default.serverIp, Settings.Default.serverLogin, Settings.Default.serverPass);
        }

        public ServerFtpModel(string ip, string login, string pass)
        {
            Server = new FTPClient(ip, login, pass);
        }

        public void StartUpload(string pathFrom, string pathTo)
        {
            string[] files = Directory.GetFiles(pathFrom, "*.*");
            string[] subDirs = Directory.GetDirectories(pathFrom);

            foreach (string file in files)
            {
                Server.Upload(pathTo + Path.GetFileName(file), file);
            }

            foreach (string subDir in subDirs)
            {

                var list = Server.DirectoryListDetailed(pathTo);

                for (int i = 0; i < list.Length - 1; i++)
                {
                    string[] tokens = list[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    // имя файла
                    string name = tokens[8];
                    // тип файла. d - папка
                    string category = tokens[0];
                    if (category.StartsWith("d"))
                    {
                        if (name == Path.GetFileName(subDir))
                        {
                            StartUpload(subDir, pathTo + Path.GetFileName(subDir) + "/");
                        }
                        else
                        {
                            Server.CreateDirectory(pathTo + Path.GetFileName(subDir));
                            StartUpload(subDir, pathTo + Path.GetFileName(subDir) + "/");
                        }
                    }
                }
            }
        }

        public void StartDownload(string pathFrom, string pathTo)
        {
            Server.Download(pathFrom, pathTo);
        }
    }
}
