﻿using NUnrar.Archive;
using NLog;
using System.IO;
using System.IO.Compression;

namespace Obmen_wpf
{
    /// <summary>
    /// Определяет методы для копирования файлов, папок, разархивации zip и rar
    /// </summary>
    class Operation
    {
        // Логирование Nlogs
        private static Logger log = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Позволяет копировать файлы, папки, подпапки и разархивировать zip и rar
        /// </summary>
        /// <param name="pathFrom">От куда копировать</param>
        /// <param name="pathTo">Куда копировать</param>
        /// <param name="isArchive">Это архив?</param>
        public void CopyFile(string pathFrom, string pathTo, bool isArchive)
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

        /// <summary>
        /// Позволяет разархивировать zip и rar
        /// </summary>
        /// <param name="pathFrom">От куда брать архив</param>
        /// <param name="pathTo">Куда разархивировать</param>
        private void ExtractArchive(string pathFrom, string pathTo)
        {
            DirectoryInfo dirFrom = new DirectoryInfo(pathFrom);
            DirectoryInfo dirTo = new DirectoryInfo(pathTo);
            FileInfo[] files = dirFrom.GetFiles();

            if (dirTo.Exists) dirTo.Delete(true);
            for (int i = 0; i < files.Length; i++)
            {
                string _pathFrom = dirFrom + "\\" + files[i].Name;
                if (files[i].Name.Contains(".zip"))
                {
                    ZipFile.ExtractToDirectory(_pathFrom, pathTo); // Разархивация .zip
                }
                else if (files[i].Name.Contains(".rar"))
                {
                    RarArchive.WriteToDirectory(_pathFrom, pathTo); // Разархивация .rar 
                }
            }
        }
    }
}