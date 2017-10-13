using NUnrar.Archive;
using System.IO;
using System.IO.Compression;

namespace Obmen_wpf
{
    class Copy
    {
        public void CopyFile(string pathFrom, string pathTo)
        {
            DirectoryInfo directoryFrom = new DirectoryInfo(pathFrom);
            DirectoryInfo directoryTo = new DirectoryInfo(pathTo);

            if (directoryFrom.Exists && directoryTo.Exists)
            {
                DirectoryInfo[] dirs = directoryFrom.GetDirectories();
                FileInfo[] files = directoryFrom.GetFiles();

                foreach (FileInfo file in files)
                {
                    file.CopyTo(pathTo + file.Name, true);
                }

                foreach (DirectoryInfo dir in dirs)
                {
                    Directory.CreateDirectory(pathTo + dir.Name);
                    CopyFile(pathFrom + "\\" + dir.Name, pathTo + dir.Name + "\\");
                }
            }
            else
            {
                directoryFrom.Create();
                directoryTo.Create();

                CopyFile(pathFrom, pathTo);

            }
        }

        public void ExtractArchive(string pathFrom, string pathTo)
        {
            try
            {
                DirectoryInfo dirFrom = new DirectoryInfo(pathFrom);
                DirectoryInfo dirTo = new DirectoryInfo(pathTo);
                FileInfo[] files = dirFrom.GetFiles();

                if (dirTo.Exists) dirTo.Delete(true);
                for (int i = 0; i < files.Length; i++)
                {
                    string _pathFrom = dirFrom + files[i].Name;
                    ZipFile.ExtractToDirectory(_pathFrom, pathTo); // Разархивация .zip
                    RarArchive.WriteToDirectory(pathFrom, pathTo); // Разархивация .rar 
                }
            }
            catch (IOException)
            {

            }
        }
    }
}
