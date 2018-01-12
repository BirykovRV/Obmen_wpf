using Obmen_wpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Obmen_wpf.ViewModel
{
    class ViewModelBase 
    {
        public Command OnClick
        {
            get
            {
                return new Command( o =>
                {     
                    if (RemovableDisk.FindDisk())
                    {
                        foreach (var item in RemovableDisk.RemovableDrives)
                        {
                            Console.WriteLine(item.Key + " " + item.Value);
                        }
                        RemovableDisk.RemovableDrives.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Нет USB");
                    }
                });
            }
        }
    }
}
