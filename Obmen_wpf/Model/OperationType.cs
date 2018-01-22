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
        /// <summary>
        /// Название операции
        /// </summary>
        private string name;
        /// <summary>
        /// Контрол для операций
        /// </summary>
        private UserControl control;
        /// <summary>
        /// Устанавливает или получает тип контрола
        /// </summary>
        public UserControl Control
        {
            get { return control; }
            set
            {
                control = value;
                OnPropertyChanged("Control");
            }
        }
        /// <summary>
        /// Получает или устанавливает имя операции
        /// </summary>
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
