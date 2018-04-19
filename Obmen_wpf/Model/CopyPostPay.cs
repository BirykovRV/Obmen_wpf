using NLog;
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

                var dbTime = server.GetTime(serverDBFrom);
                var ppsTime = server.GetTime(serverUpdateFrom);
                server.StartUpload(postPayRegTo, serverRegTo);

                if (CheckUpdateFromFTP(dbTime, postPayDBFrom))
                {
                    server.StartDownload(serverDBFrom, postPayDBFrom);
                    // установка времени изменения файла для БД с фтп
                    var dnFile = Directory.GetFiles(postPayDBFrom).FirstOrDefault();
                    File.SetLastWriteTime(dnFile, dbTime);
                }
                if (CheckUpdateFromFTP(ppsTime, postPayUpdateFrom))
                {
                    server.StartDownload(serverUpdateFrom, postPayUpdateFrom);
                    // установка времени изменения файла для update с фтп
                    var updateFile = Directory.GetFiles(postPayUpdateFrom).FirstOrDefault();
                    File.SetLastWriteTime(updateFile, ppsTime);
                }
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
                DirectoryInfo update = new DirectoryInfo(from);         // От куда копировать
                DirectoryInfo plugin = new DirectoryInfo(to);           // Куда копировать БД или Модуль
                
                // Дата создания плагина или БД                
                var updateDate = update.GetFiles().FirstOrDefault()?.LastWriteTime;
                var pluginDate = plugin.GetFiles().FirstOrDefault()?.LastWriteTime;

                if (updateDate == null)
                    throw new ArgumentNullException(from , "Отсутствует архив с обновлением.");
                else if (pluginDate == null)
                    return true;
                return updateDate > pluginDate;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            return false;
        }

        private bool CheckUpdateFromFTP(DateTime from, string to)
        {
            try
            {
                DirectoryInfo update = new DirectoryInfo(to);

                var updateFile = update.GetFiles().FirstOrDefault();

                if (updateFile == null)
                    return true;
                return from > updateFile.LastWriteTime;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            return false;
        }
    }
}
