using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using NLog;

namespace Obmen_wpf.Model
{
    /// <summary>
    /// Возвращает словарь где key - буква диска, value - название
    /// </summary>
    class RemovableDisk
    {
        private static Logger log = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Возвращает словарь, где Key - это буква диска, а Value - название
        /// </summary>
        public static Dictionary<string, string> RemovableDrives { get { return removableDrive; } }
        // Словарь для буквы диска и имени
        private static Dictionary<string, string> removableDrive = new Dictionary<string, string>();

        /// <summary>
        /// Метод для поика съемных USB устройств
        /// </summary>
        public static bool FindDisk()
        {
            DriveInfo[] driveInfo = DriveInfo.GetDrives();
            bool isEmpty = false;
            foreach (var item in driveInfo)
            {           
                if ( item.DriveType == DriveType.Removable )
                {
                    string lable = item.VolumeLabel;
                    Regex regex = new Regex(@"^\d{6}&");

                    if (regex.IsMatch(lable))
                    {
                        removableDrive.Add(item.Name, item.VolumeLabel);
                        isEmpty = true;
                    }
                }
            }
            return isEmpty;
        }
    }
}
