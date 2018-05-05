using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Obmen_wpf.Model;
using Obmen_wpf;
using System.Xml.Linq;
using System.Reflection;
using System.Diagnostics;

namespace Updater
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var proc in Process.GetProcesses())
            {
                if (proc.ProcessName.Contains("Obmen_wpf"))
                {
                    proc.Kill();
                    proc.WaitForExit();
                }
            }

            if (RemovableDisk.FindDisk())
            {
                var usb = RemovableDisk.RemovableDrives.FirstOrDefault();


            }
        }
    }
}
