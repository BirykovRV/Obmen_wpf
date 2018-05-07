using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Configuration;

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

                var updateFrom = $"{usb.Key}Offline_Helper";
                var updateTo = "C:\\Offline_Helper\\";


                var fileInfo = Directory.GetFiles(updateFrom);

                foreach (var file in fileInfo)
                {
                    File.Copy(file, updateTo + Path.GetFileName(file), true);
                }
            }
            System.Windows.Forms.MessageBox.Show("Обновление завершено успешно! Нажмите Ок.", "Внимание", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);
            Process.Start("Obmen_wpf.exe");
        }
    }
}

