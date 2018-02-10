using Obmen_wpf.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obmen_wpf.Model
{
    class CopyFSG : ICopyFiles
    {
        public void Start(string key, string value, bool isInfoPoint)
        {
            // FSG Reg
            string fsgRegFrom = Settings.Default.fsgRegFrom;
            string fsgRegTo = key + Settings.Default.fsgRegTo + "\\";
            // FSG Cash
            string cashFsgFrom = key + Settings.Default.cashFsgFrom;
            string cashFsgTo = Settings.Default.cashFsgTo + "\\";

            string serverPathTo = $"{value}/FSG/";
            string serverPathFrom = $"ToOPS/FSG/{value}/";

            if (isInfoPoint)
            {
                ServerFtpModel.StartUpload(fsgRegTo, serverPathTo);
                ServerFtpModel.StartDownload(serverPathFrom, cashFsgFrom);
            }
            else
            {
                // FSG Reg
                Operations.CopyFile(fsgRegFrom, fsgRegTo, false);
                // FSG Cash
                Operations.CopyFile(cashFsgFrom, cashFsgTo, false);
            }
        }
    }
}
