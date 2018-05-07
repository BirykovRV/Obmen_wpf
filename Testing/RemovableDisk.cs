using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;

namespace Updater
{
    /// <summary>
    /// Возвращает словарь где key - буква диска, value - название
    /// </summary>
    public class RemovableDisk
    {
        /// <summary>
        /// Возвращает словарь, где Key - это буква диска, а Value - название
        /// </summary>
        public static Dictionary<string, string> RemovableDrives { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Метод для поика съемных USB устройств
        /// </summary>
        public static bool FindDisk()
        {
            Regex regex = new Regex(@"^\d{6}$");
            DriveInfo[] driveInfo = DriveInfo.GetDrives();
            bool isEmpty = false;
            foreach (var item in driveInfo)
            {
                if (item.DriveType == DriveType.Removable && regex.IsMatch(item.VolumeLabel))
                {
                    RemovableDrives.Add(item.Name, item.VolumeLabel);
                    isEmpty = true;
                }
            }
            return isEmpty;
        }
    }
}
