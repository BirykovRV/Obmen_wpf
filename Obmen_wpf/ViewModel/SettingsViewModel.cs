﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Obmen_wpf.Model;
using Obmen_wpf.Properties;
using Obmen_wpf.View;

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
                    MessageBox.Show("Настройки успешно сохранены!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
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
                    Settings.Default.pensionFrom = Environment.ExpandEnvironmentVariables("C:\\Users\\%USERNAME%\\Desktop\\Пенсия");
                    Settings.Default.listTo = Environment.ExpandEnvironmentVariables("C:\\Users\\%USERNAME%\\Desktop");                    
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
