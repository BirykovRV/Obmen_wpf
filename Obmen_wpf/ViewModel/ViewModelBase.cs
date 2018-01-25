using Obmen_wpf.Model;
using Obmen_wpf.Properties;
using Obmen_wpf.View;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Obmen_wpf.ViewModel
{
    class ViewModelBase
    {
        private List<ICopyFiles> listOfOperations;
        
        public ViewModelBase()
        {
            listOfOperations = new List<ICopyFiles>()
            {
                new CopyF130(),
                new CopyPostPay(),
                new CopyEspp(),
                new CopyFSG()
            };
        }
        /// <summary>
        /// Запускает программу на выполнение
        /// </summary>
        public ICommand OnClick
        {
            get
            {
                return new Command(o =>
               {
                   Task.Factory.StartNew(() =>
                   {
                       if (RemovableDisk.FindDisk())
                       {
                           foreach (var item in RemovableDisk.RemovableDrives)
                           {
                               foreach (var oper in listOfOperations)
                               {
                                   oper.Start(item.Key);                                   
                               }                               
                           }
                           RemovableDisk.RemovableDrives.Clear();
                       }
                       else
                       {
                           MessageBox.Show("Нет USB");
                       }
                   });
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
                    settingsView.ShowDialog();
                });
            }
        }
    }
}
