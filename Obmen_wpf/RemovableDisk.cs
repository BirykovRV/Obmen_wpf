using System.Collections.Generic;
using System.IO;
using NLog;

namespace Obmen_wpf
{
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
        public static void FindDisk()
        {
            DriveInfo[] driveInfo = DriveInfo.GetDrives();
            foreach (var item in driveInfo)
            {
                if (item.DriveType == DriveType.Removable)
                {
                    removableDrive.Add(item.Name, item.VolumeLabel);
                }
            }
        }
    }
}
