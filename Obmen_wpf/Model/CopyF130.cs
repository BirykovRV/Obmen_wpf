using Obmen_wpf.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Obmen_wpf.Model
{
    class CopyF130 : ICopyFiles
    {
        public void Start(string key, string value, bool isInfoPoint)
        {
            // Config
            string configFrom = key + Settings.Default.configFrom;
            string configTo = Settings.Default.configTo + "\\";
            // ASKU
            string f130From = Settings.Default.f130From;
            string f130To = key + Settings.Default.f130To + "\\";
            // Почтамт
            string path = Settings.Default.askuPath;

            string serverPathTo = $"/{path}/{value}/";
            string serverPathFrom = "ToOPS/Config/";

            if (isInfoPoint)
            {                 
                ServerFtpModel server = new ServerFtpModel();
                ServerFtpModel askuServer = new ServerFtpModel(Settings.Default.askuIp, Settings.Default.askuLogin, Settings.Default.askuPass);
                askuServer.StartUpload(f130To, serverPathTo);
                server.StartDownload(serverPathFrom, configFrom);
            }
            else
            {
                try
                {
                    foreach (Process process in Process.GetProcesses())
                    {
                        if (process.ProcessName.StartsWith("TransportModule"))
                        {
                            process.Kill();
                            process.WaitForExit();
                        }                        
                    }
                    // Config
                    Operations.CopyFile(configFrom, configTo, false);
                    // ASKU
                    Operations.CopyFile(f130From, f130To, false);
                    //Delete old files
                    // Operations.DeleteOldObj(f130From);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }                
            }
        }
    }
}
