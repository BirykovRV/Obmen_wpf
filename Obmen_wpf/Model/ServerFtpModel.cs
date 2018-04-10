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
            string[] files = Directory.GetFiles(pathFrom, "*.*");
            string[] subDirs = Directory.GetDirectories(pathFrom);

            foreach (string file in files)
            {
                Server.Upload(pathTo + Path.GetFileName(file), file);
            }

            for (int i = 0; i < subDirs.Length; i++)
            {
                var list = Server.DirectoryListDetailed(pathTo);
                if (list.FirstOrDefault() != "")
                {
                    for (int j = 0; j < list.Length - 1; j++)
                    {
                        string[] tokens = list[j].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        // имя файла
                        string name = tokens[8];
                        // тип файла. d - папка
                        string category = tokens[0];
                        if (category.StartsWith("d"))
                        {
                            if (name != Path.GetFileName(subDirs[i]))
                            {
                                Server.CreateDirectory(pathTo + Path.GetFileName(subDirs[i]));
                                StartUpload(subDirs[i], pathTo + Path.GetFileName(subDirs[i]) + "/");
                            }
                        }
                    }
                }
                else
                {
                    Server.CreateDirectory(pathTo + Path.GetFileName(subDirs[i]));
                    StartUpload(subDirs[i], pathTo + Path.GetFileName(subDirs[i]) + "/");
                }
                
            }
        }

        public void StartDownload(string pathFrom, string pathTo)
        {
            Server.Download(pathFrom, pathTo);
        }
    }
}
