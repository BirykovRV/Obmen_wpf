using Obmen_wpf.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obmen_wpf.Model
{
    class ListCopy : ICopyFiles
    {
        public void Start(string key, string value, bool isInfoPoint)
        {
            // list
            string listFrom = key + Settings.Default.listFrom;
            string listTo = Settings.Default.listTo + "\\";


            if (isInfoPoint)
            {
                ServerFtpModel server = new ServerFtpModel(Settings.Default.listIp + "/", Settings.Default.listLogin, Settings.Default.listPass);

                server.StartDownload("", listFrom);

            }
            else
            {   // Config
                // Cписок файлов на флешке
                var list = Directory.GetFiles(listFrom);
                // Список файлов Рабочего стола
                var desktopFiles = Directory.GetFiles(listTo);
                // Имя с расширением файла
                var fileName = list.FirstOrDefault();

                foreach (var item in desktopFiles)
                {
                    if (item.Contains("Список_террористов"))
                    {
                        File.Delete(item);
                    }
                }
                var newFile = listTo + $"Список_террористов_{DateTime.Today.ToShortDateString()}.docx";
                File.Copy(fileName, newFile, true);                

                //Delete old files
                // Operations.DeleteOldObj(f130From);
            }
        }
    }
}
