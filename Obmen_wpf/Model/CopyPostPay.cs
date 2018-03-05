﻿using NLog;
using Obmen;
using Obmen_wpf.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

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
                ServerFtpModel.StartUpload(postPayRegTo, serverRegTo);                
                ServerFtpModel.StartDownload(serverUpdateFrom, postPayUpdateFrom);
                ServerFtpModel.StartDownload(serverDBFrom, postPayDBFrom);
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
            DirectoryInfo pluginInfo = new DirectoryInfo(to);
            DirectoryInfo updateInfo = new DirectoryInfo(from);
            try
            {
                var files = updateInfo.GetFiles();

                var pluginUpdateTime = pluginInfo.CreationTimeUtc;

                if (files.Length > 0)
                {
                    foreach (var file in files)
                    {
                        if (file.LastWriteTimeUtc > pluginUpdateTime)
                        {
                            return true;
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                log.Debug(ex.ToString());
            }
            return false;
        }
    }
}
