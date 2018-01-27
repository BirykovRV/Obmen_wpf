using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Obmen_wpf.Model;
using Obmen_wpf.Properties;
using Obmen_wpf.View;
using Microsoft.Win32;
using System.Windows;

namespace Obmen_wpf.ViewModel
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Выбраная операция
        /// </summary>
        private OperationType selectedOperation;
        /// <summary>
        /// Коллекция контролов для каждой операции
        /// </summary>
        public ObservableCollection<OperationType> Controls { get; set; }

        /// <summary>
        /// Создается новая коллеция контролов
        /// </summary>
        public SettingsViewModel()
        {
            Controls = new ObservableCollection<OperationType>
            {
                new OperationType { Name = "F130", Control = new F130UserControl()},
                new OperationType { Name = "PostPay", Control = new PostPayUserControl()},
                new OperationType { Name = "ESPP", Control = new EsppUserControl()},
                new OperationType { Name = "FSG", Control = new FsgUserControl()},
                new OperationType { Name = "Инфо. пункт", Control = new InfoPointUserControl()}
            };
        }
        /// <summary>
        /// Выбранный элемент в списке
        /// </summary>
        public OperationType SelectedOperation
        {
            get { return selectedOperation; }
            set
            {
                selectedOperation = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Сохраняет настройки программы
        /// </summary>
        public ICommand OnSave
        {
            get
            {
                return new Command(o =>
                {
                    Settings.Default.Save();
                    //System.Windows.Forms.Application.Restart();
                    //Application.Current.Shutdown();
                }, o => Settings.IsMyPropertyChanged);
            }
        }
        /// <summary>
        /// Возвращает все значения настроек по умолчанию
        /// </summary>
        public ICommand OnDefault
        {
            get
            {
                return new Command(o =>
                {
                    Settings.Default.Reset();
                });
            }
        }        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string sender = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(sender));
        }
    }
}
