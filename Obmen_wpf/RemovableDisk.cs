using System.Collections.Generic;
using System.IO;

namespace Obmen_wpf
{
    class RemovableDisk
    {
        /// <summary>
        /// Возвращает словарь, где Key - это буква диска, а Value - название
        /// </summary>
        public Dictionary<string, string> RemovableDrives { get { return removableDrive; } }
        // Словарь для буквы диска и имени
        Dictionary<string, string> removableDrive = new Dictionary<string, string>();

        /// <summary>
        /// Метод для поика съемных USB устройств
        /// </summary>
        public void FindDisk()
        {
            DriveInfo[] driveInfo = DriveInfo.GetDrives();
            for (int i = 0; i < driveInfo.Length; i++)
            {
                if (driveInfo[i].DriveType == DriveType.Removable)
                {
                    removableDrive.Add(driveInfo[i].Name, driveInfo[i].VolumeLabel);
                }
            }
        }
    }
}
