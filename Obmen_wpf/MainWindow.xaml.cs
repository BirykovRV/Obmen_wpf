using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Obmen_wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Operation operation;
        private RemovableDisk disk; 

        public MainWindow()
        {
            InitializeComponent();
            operation = new Operation();
            disk = new RemovableDisk();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            disk.FindDisk();
            //string from = "D:\\TestFrom";
            string to = "C:\\TestTo\\";
            //string fromArchive = "D:\\TestFrom";
            //string toArchive = "C:\\TestTo\\Archive\\";
            //operation.CopyFile(from, to, false);
            //operation.CopyFile(fromArchive, toArchive, true);
            foreach (var item in disk.RemovableDrives)
            {
                operation.CopyFile($"{item.Key}\\PostPay\\DB", to, true);
                //operation.CopyFile(fromArchive, toArchive, true);
            }
            disk.RemovableDrives.Clear();
        }
    }
}
