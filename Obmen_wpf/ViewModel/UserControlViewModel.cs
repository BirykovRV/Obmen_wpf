using Microsoft.Win32;
using Obmen_wpf.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Obmen_wpf.ViewModel
{
    class UserControlViewModel : INotifyPropertyChanged
    {

        public ICommand OnOpenDialog
        {
            get
            {
                return new Command(o =>
                {
                    switch (o.ToString())
                    {
                        case "asku":
                            OpenFileDialog dialog = new OpenFileDialog();
                            dialog.ShowDialog();
                            break;
                        case "config":
                            MessageBox.Show("config");
                            break;

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
