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

        private OperationType selectedOperation;
        public ObservableCollection<OperationType> Controls { get; set; }

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

        public OperationType SelectedOperation
        {
            get { return selectedOperation; }
            set
            {
                selectedOperation = value;
                OnPropertyChanged();
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

        public ICommand OnOpenDialog
        {
            get
            {
                return new Command(o =>
                {
                    if (o.ToString() == "asku")
                    {
                        MessageBox.Show("Test");
                    }
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
