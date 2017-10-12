
using System.IO;

namespace Obmen_wpf
{
    class RemovableDisk
    {
        public string GetDiskVolumeLable { get; private set; }
        public string GetDiskChar { get; private set; }

        public string FindDiskChar()
        {
            DriveInfo[] driveInfo = DriveInfo.GetDrives();
            for (int i = 0; i < driveInfo.Length; i++)
            {
                if (driveInfo[i].DriveType == DriveType.Removable)
                {
                    GetDiskChar = driveInfo[i].Name;
                    GetDiskVolumeLable = driveInfo[i].VolumeLabel;
                }
            }
            return null;
        }
    }
}
