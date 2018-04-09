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
                new CopyFSG(),
                new ListCopy()
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
                    // внешняя задача чтобы не зависал интерфейс
                    var outer = Task.Factory.StartNew(() =>
                    {
                        // запрещаем нажимать на кнопки во премя выполнения
                        IsComplited = false;
                        // получаем значение из настроек для определения режима работы (ОПС или ИП)
                        bool isInfoPoint = Settings.Default.IsInfoPoinChecked;
                        // есть ли съемные носители
                        if (RemovableDisk.FindDisk())
                        {
                            // создаем паралельную задачу
                            Parallel.Invoke(() =>
                            {
                                // съемные носители найдены и получаем их список
                                foreach (var item in RemovableDisk.RemovableDrives)
                                {
                                    // внутренняя задача чтобы все работало синхронно на разных ядрах
                                    var inner = Task.Factory.StartNew(() =>
                                    {
                                        System.Console.WriteLine("Start working with - {0}", item.Value);
                                        // для каждого списка операций вызываем выполнение
                                        foreach (var oper in listOfOperations)
                                        {
                                            System.Console.WriteLine("{0} - Working with - {1}", item.Value, oper.ToString());
                                            oper.Start(item.Key, item.Value, isInfoPoint);
                                            // увеличиваем прогресбар
                                            Progress++;
                                        }
                                        System.Console.WriteLine("End working with - {0}", item.Value);
                                        // Если необходимо, чтобы вложенная задача выполнялась вместе с внешней, необходимо использовать значение TaskCreationOptions.AttachedToParent
                                    }, TaskCreationOptions.AttachedToParent); 
                                }
                            });
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

        private void DoJob()
        {

        }
    }
}
