using Obmen_wpf.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            string serverPathTo = $"{path}/{value}/";
            string serverPathFrom = "ToOPS/Config/";

            if (isInfoPoint)
            {
                AskuFtpModel.StartUpload(f130To, serverPathTo);
                ServerFtpModel.StartDownload(serverPathFrom, configFrom);               
            }
            else
            {
                // Config
                Operations.CopyFile(configFrom, configTo, false);
                // ASKU
                Operations.CopyFile(f130From, f130To, false);
            }
        }
    }
}
