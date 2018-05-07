﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Obmen_wpf.Model
{
    class UpdateChecker
    {
        /// <summary>
        /// Проверка на наличие файла программы новой версии
        /// </summary>
        /// <param name="fileName">Название программы с расширением</param>
        /// <returns></returns>
        public static bool CheckUpdate(string fileName)
        {
            if (RemovableDisk.FindDisk())
            {
                var usb = RemovableDisk.RemovableDrives.FirstOrDefault();

                var filePath = $"{usb.Key}Offline_Helper\\{fileName}";

                if (File.Exists(filePath))
                {
                    var remoteFileVersion = new Version(FileVersionInfo.GetVersionInfo(filePath).FileVersion);
                    var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;

                    return true ? remoteFileVersion > currentVersion : false;
                }
                else
                    throw new FileNotFoundException($"Файл не найден.\n{usb.Key}Offline_Helper\\{fileName}");
            }
            return false;
        }
    }
}
