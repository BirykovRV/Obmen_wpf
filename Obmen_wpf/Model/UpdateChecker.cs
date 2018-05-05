using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Obmen_wpf.Model
{
    class UpdateChecker
    {
        public static bool CheckUpdate(string fileName)
        {
            if (RemovableDisk.FindDisk())
            {
                var usb = RemovableDisk.RemovableDrives.FirstOrDefault();

                var filePath = $"{usb.Key}{usb.Value}\\Offline_Helper\\{fileName}";


                if (File.Exists(filePath))
                {
                    var remoteFileVersion = new Version(Convert.ToString(FileVersionInfo.GetVersionInfo(filePath)));
                    var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;

                    return true ? remoteFileVersion > currentVersion : false;
                }
                else
                    throw new FileNotFoundException();
            }
            return false;
        }
    }
}
