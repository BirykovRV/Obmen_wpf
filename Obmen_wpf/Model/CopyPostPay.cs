using NLog;
using Obmen;
using Obmen_wpf.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Linq;

namespace Obmen_wpf.Model
{
    class CopyPostPay : ICopyFiles
    {
        // Логирование Nlogs
        private static Logger log = LogManager.GetCurrentClassLogger();
        private object files;

        public void Start(string key, string value, bool isInfoPoint)
        {
            // Reg PostPay
            string postPayRegFrom = Settings.Default.postPayRegFrom;
            string postPayRegTo = key + Settings.Default.postPayRegTo + "\\";
            // DB PostPay
            string postPayDBFrom = key + Settings.Default.postPayDBFrom;
            string postPayDBTo = Settings.Default.postPayDBTo + "\\";
            // Update PostPay
            string postPayUpdateFrom = key + Settings.Default.postPayUpdateFrom;
            string postPayUpdateTo = Settings.Default.postPayUpdateTo + "\\";

            string serverRegTo = $"{value}/Реестр коммунальных платежей/";
            string serverUpdateFrom = "ToOPS/PostPay/Update/";
            string serverDBFrom = "ToOPS/PostPay/DB/";

            // Reg PostPay
            if (isInfoPoint)
            {
                ServerFtpModel server = new ServerFtpModel();
                server.StartUpload(postPayRegTo, serverRegTo);
                server.StartDownload(serverUpdateFrom, postPayUpdateFrom);
                server.StartDownload(serverDBFrom, postPayDBFrom);
            }
            else
            {
                // Копирование реестра
                Operations.CopyFile(postPayRegFrom, postPayRegTo, false);
                //Delete old files
                //Operations.DeleteOldObj(postPayRegFrom);

                if (CheckForUpdate(postPayUpdateFrom, postPayUpdateTo))
                {
                    try
                    {
                        foreach (Process process in Process.GetProcesses())
                        {
                            if (process.ProcessName.StartsWith("PpsPlugin.Scheduler"))
                            {
                                process.Kill();
                                process.WaitForExit();
                            }

                            if (process.ProcessName.StartsWith("GM_Scheduler"))
                            {
                                process.Kill();
                                process.WaitForExit();
                            }

                            if (process.ProcessName.StartsWith("POS"))
                            {
                                process.Kill();
                                process.WaitForExit();
                            }
                        }
                        // Update PostPay
                        Operations.CopyFile(postPayUpdateFrom, postPayUpdateTo, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (CheckForUpdate(postPayDBFrom, postPayDBTo))
                {
                    // DB PostPay
                    Operations.CopyFile(postPayDBFrom, postPayDBTo, true);
                }
            }
        }

        private bool CheckForUpdate(string from, string to)
        {           
            try
            {
                DirectoryInfo plugin = new DirectoryInfo(to);           // Куда копировать БД или Модуль
                DirectoryInfo update = new DirectoryInfo(from);     // От куда копировать
                // Дата создания плагина или БД на компьютере                
                var updateFile = update.GetFiles().FirstOrDefault();

                if (plugin.GetDirectories().FirstOrDefault() == null)
                {
                    return updateFile.LastWriteTime > plugin.GetFiles().FirstOrDefault().LastWriteTime;
                }
                else   // Дата и время изменения новой БД или плагина больше чем на компьютере?
                {
                    return updateFile.LastWriteTime > plugin.GetDirectories().FirstOrDefault().LastWriteTime;
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            return false;
        }
    }
}
