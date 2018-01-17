using Obmen_wpf.Model;
using Obmen_wpf.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Obmen_wpf.ViewModel
{
    class ViewModelBase
    {
        public ICommand OnClick
        {
            get
            {
                return new Command(o =>
               {


                   if (RemovableDisk.FindDisk())
                   {
                       string from = Properties.Settings.Default.configFrom;
                       string to = Properties.Settings.Default.configTo;

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

        public ICommand OnSave
        {
            get
            {
                return new Command(o =>
               {
                   Settings.Default.Save();
               }, o => Settings.IsMyPropertyChanged);
            }
        }
    }
}
