using System.IO;

namespace Obmen_wpf
{
    class Copy
    {
        public void CopyFile(string from, string to)
        {
            DirectoryInfo directoryFrom = new DirectoryInfo(from);
            DirectoryInfo directoryTo = new DirectoryInfo(to);

            if (directoryFrom.Exists && directoryTo.Exists)
            {
                DirectoryInfo[] dirs = directoryFrom.GetDirectories();
                FileInfo[] files = directoryFrom.GetFiles();

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
                directoryFrom.Create();
                directoryTo.Create();

                CopyFile(from, to);

            }
        }
    }
}
