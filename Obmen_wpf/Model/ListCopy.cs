using Obmen_wpf.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obmen_wpf.Model
{
    class ListCopy : ICopyFiles
    {
        public void Start(string key, string value, bool isInfoPoint)
        {
            // Config
            string listFrom = key + Settings.Default.listFrom;
            string listTo = Settings.Default.listTo + "\\";
                  
            string serverPathFrom = "/";

            if (isInfoPoint)
            {                
                ServerFtpModel.StartDownload(serverPathFrom, listFrom);
            }
            else
            {
                // Config
                Operations.CopyFile(listFrom, listTo, false);
                
                //Delete old files
                // Operations.DeleteOldObj(f130From);
            }
        }
    }
}
