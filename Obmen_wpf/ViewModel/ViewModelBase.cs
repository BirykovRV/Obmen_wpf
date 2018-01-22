﻿using Obmen_wpf.Model;
using Obmen_wpf.Properties;
using Obmen_wpf.View;
using System.Windows;
using System.Windows.Input;

namespace Obmen_wpf.ViewModel
{
    class ViewModelBase
    {
        /// <summary>
        /// Запускает программу на выполнение
        /// </summary>
        public ICommand OnClick
        {
            get
            {
                return new Command(o =>
               {


                   if (RemovableDisk.FindDisk())
                   {
                       string from = Settings.Default.configFrom;
                       string to = Settings.Default.configTo;

                       foreach (var item in RemovableDisk.RemovableDrives)
                       {
                           Operations.CopyFileAsync(from, to, false);
                           
                       }
                       RemovableDisk.RemovableDrives.Clear();
                   }
                   else
                   {
                       //TODO: 
                       MessageBox.Show("Нет USB");
                   }
               });
            }
        }
        /// <summary>
        /// Открывает окно с настройками программы
        /// </summary>
        public ICommand OnSettings
        {
            get
            {
                return new Command(o =>
                {
                    SettingsView settingsView = new SettingsView();
                    settingsView.Show();
                });
            }
        }
    }
}
