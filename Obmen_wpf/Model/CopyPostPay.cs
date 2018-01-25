using Obmen_wpf.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Obmen_wpf.Model
{
    class CopyPostPay : ICopyFiles
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

        public void Start(string key)
        {
            // Reg PostPay
            Operations.CopyFileAsync(postPayRegFrom, key + postPayRegTo, false);
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
                Operations.CopyFileAsync(key + postPayUpdateFrom, postPayUpdateTo, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            // DB PostPay
            Operations.CopyFileAsync(key + postPayDBFrom, postPayDBTo, true);            
        }
    }
}
