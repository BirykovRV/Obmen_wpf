using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Obmen_wpf.Model
{
    class OperationType : INotifyPropertyChanged
    {
        private string name;
        private UserControl control;

        public UserControl Control
        {
            get { return control; }
            set
            {
                control = value;
                OnPropertyChanged("Control");
            }
        }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string sender = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(sender));
        }
    }
}
