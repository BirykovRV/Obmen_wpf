using System.IO;

namespace Obmen_wpf
{
    class Copy
    {
        public void CopyFile(string from, string to)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(from);
            DirectoryInfo directoryTo = new DirectoryInfo(to);

            if (directoryInfo.Exists && directoryTo.Exists)
            {
                DirectoryInfo[] dirs = directoryInfo.GetDirectories();
                FileInfo[] files = directoryInfo.GetFiles();

                foreach (FileInfo file in files)
                {
                    file.CopyTo(to + file.Name, true);
                }

                foreach (DirectoryInfo dir in dirs)
                {
                    Directory.CreateDirectory(to + dir.Name);
                    CopyFile(from + "\\" + dir.Name, to + dir.Name + "\\");
                }
            }
            else
            {
                throw new System.Exception("Ошибка Копирования, проверьте наличие папок");
            }
        }
    }
}
