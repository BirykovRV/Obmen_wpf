using Obmen_wpf.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Obmen_wpf.Model
{
    class CopyPostPay : ICopyFiles
    {
        public void Start(string key, string value, bool isInfoPoint)
        {
            // Reg PostPay
            string postPayRegFrom = Settings.Default.postPayRegFrom;
            string postPayRegTo = Settings.Default.postPayRegTo + "\\";
            // DB PostPay
            string postPayDBFrom = Settings.Default.postPayDBFrom;
            string postPayDBTo = Settings.Default.postPayDBTo + "\\";
            // Update PostPay
            string postPayUpdateFrom = Settings.Default.postPayUpdateFrom;
            string postPayUpdateTo = Settings.Default.postPayUpdateTo + "\\";
            // Reg PostPay
            Operations.CopyFile(postPayRegFrom, key + postPayRegTo, false);

            if (CheckForUpdate(key + postPayUpdateFrom, postPayUpdateTo))
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
                    Operations.CopyFile(key + postPayUpdateFrom, postPayUpdateTo, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (CheckForUpdate(key + postPayDBFrom, postPayDBTo))
            {
                // DB PostPay
                Operations.CopyFile(key + postPayDBFrom, postPayDBTo, true);
            }                       
        }

        private bool CheckForUpdate(string from, string to)
        {
            DirectoryInfo pluginInfo = new DirectoryInfo(to);
            DirectoryInfo updateInfo = new DirectoryInfo(from);
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
            return false;
        }
    }
}
