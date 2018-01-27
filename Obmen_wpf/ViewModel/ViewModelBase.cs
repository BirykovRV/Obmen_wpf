using Obmen_wpf.Model;
using Obmen_wpf.Properties;
using Obmen_wpf.View;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Obmen_wpf.ViewModel
{
    class ViewModelBase : INotifyPropertyChanged
    {
        private List<ICopyFiles> listOfOperations;
        private bool isComplited;
        private int progress;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsComplited
        {
            get
            {
                return isComplited;
            }
            set
            {
                isComplited = value;
                OnPropertyChanged();
            }
        }

        public int Progress
        {
            get
            {
                return progress;
            }
            set
            {
                progress = value;
                OnPropertyChanged();
            }
        }

        public int MaxOperation { get; set; }

        public ViewModelBase()
        {
            listOfOperations = new List<ICopyFiles>()
            {
                new CopyF130(),
                new CopyPostPay(),
                new CopyEspp(),
                new CopyFSG()
            };
            IsComplited = true;
            progress = 0;
            MaxOperation = listOfOperations.Count;
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
                       IsComplited = false;
                      
                       if (RemovableDisk.FindDisk())
                       {
                           foreach (var item in RemovableDisk.RemovableDrives)
                           {
                               foreach (var oper in listOfOperations)
                               {
                                   oper.Start(item.Key);
                                   Progress++;
                               }
                           }

                           Task.Delay(500).Wait();
                           // Очистка списка usb drives
                           RemovableDisk.RemovableDrives.Clear();
                           // Сброс полоски прогрессбара
                           Progress = 0;
                           MessageBox.Show("Копирование файлов завершено.\nМожете закрыть программу.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                       }
                       else
                       {
                           MessageBox.Show("Нет USB");
                       }
                       // Открываем доступ к кнопке
                       IsComplited = true;
                   });
               }, o => isComplited);
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
        /// <summary>
        /// Выйти из приложения
        /// </summary>
        public ICommand OnClose
        {
            get
            {
                return new Command(o =>
                {
                    Application.Current.Shutdown();
                }, o => isComplited);
            }
        }

        public void OnPropertyChanged(string param = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(param));
        }
    }
}
