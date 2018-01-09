using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using System.Windows.Input;

namespace Obmen_wpf.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private int count;
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
                RaisePropertyChanged(() => Count);
            }
        }

        public ICommand ClickAdd
        {
            get
            {
                return new DelegateCommand(() =>
                {                    
                    Count++;                    
                }, () => Count < 10);
            }
        }

        public ICommand Reset
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Count = 0;
                }, () => Count > 0);
            }
        }
    }
}
