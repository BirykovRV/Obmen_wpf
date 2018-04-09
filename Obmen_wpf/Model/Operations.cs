using NUnrar.Archive;
using NLog;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Obmen_wpf.Model
{
    /// <summary>
    /// Определяет методы для копирования файлов, папок, разархивации zip и rar
    /// </summary>
    class Operations
    {
        // Логирование Nlogs
        private static Logger log = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Позволяет копировать файлы, папки, подпапки и разархивировать zip и rar
        /// </summary>
        /// <param name="pathFrom">От куда копировать</param>
        /// <param name="pathTo">Куда копировать</param>
        /// <param name="isArchive">Это архив?</param>
        public static void CopyFile(string pathFrom, string pathTo, bool isArchive)
        {
            try
            {
                DirectoryInfo directoryFrom = new DirectoryInfo(pathFrom);
                DirectoryInfo directoryTo = new DirectoryInfo(pathTo);

                if (directoryFrom.Exists && directoryTo.Exists)
                {
                    if (isArchive)
                    {
                        ExtractArchive(pathFrom, pathTo);
                    }
                    else
                    {
                        DirectoryInfo[] dirs = directoryFrom.GetDirectories();
                        FileInfo[] files = directoryFrom.GetFiles();

                        foreach (FileInfo file in files)
                        {
                            string newPathTo = pathTo + file.Name;
                            file.CopyTo(newPathTo, true);
                        }

                        foreach (DirectoryInfo dir in dirs)
                        {
                            string newPathTo = pathTo + dir.Name;
                            string newPathFrom = pathFrom + "\\" + dir.Name;
                            Directory.CreateDirectory(newPathTo);
                            CopyFile(newPathFrom, newPathTo + "\\", isArchive);
                        }
                        DeleteOldObj(pathFrom);
                    }
                }
                else
                {
                    directoryFrom.Create();
                    directoryTo.Create();

                    if (isArchive)
                        ExtractArchive(pathFrom, pathTo);
                    else
                        CopyFile(pathFrom, pathTo, isArchive);
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Позволяет разархивировать zip и rar
        /// </summary>
        /// <param name="pathFrom">От куда брать архив</param>
        /// <param name="pathTo">Куда разархивировать</param>
        public static void ExtractArchive(string pathFrom, string pathTo)
        {
            DirectoryInfo dirFrom = new DirectoryInfo(pathFrom);
            DirectoryInfo dirTo = new DirectoryInfo(pathTo);
            FileInfo[] files = dirFrom.GetFiles();

            //
            for (int i = 0; i < files.Length; i++)
            {
                string _pathFrom = pathFrom + "\\" + files[i].Name;
                if (files[i].Name.Contains(".zip"))
                {
                    if (dirTo.Exists) dirTo.Delete(true);

                    ZipFile.ExtractToDirectory(_pathFrom, pathTo); // Разархивация .zip

                }
                else if (files[i].Name.Contains(".rar"))
                {
                    RarArchive.WriteToDirectory(_pathFrom, pathTo); // Разархивация .rar 
                }
            }
        }

        public static void DeleteOldObj(string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);

            var files = info.GetFiles().OrderBy(f => f.CreationTime).ToArray();
            var dirs = info.GetDirectories().OrderBy(d => d.CreationTime).ToArray();
            
            for (int i = 0; i < files.Length - 9; i++)
            {
                files[i].Delete();
                log.Debug($"{DateTime.Now} - Delite file - {files[i].FullName}");
            }

            for (int i = 0; i < dirs.Length - 9; i++)
            {
                dirs[i].Delete(true);
                log.Debug($"{DateTime.Now} - Delite directory - {dirs[i].FullName}");
            }
        }
    }
}
