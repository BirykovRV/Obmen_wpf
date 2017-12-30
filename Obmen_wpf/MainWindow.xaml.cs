using System;
using System.Windows;
using NLog;

namespace Obmen_wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Operations operation;

        private static Logger log = LogManager.GetCurrentClassLogger();

        public MainWindow()
        {
            InitializeComponent();
            operation = new Operations();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RemovableDisk.FindDisk();
                string to = "D:\\TEST\\";
                foreach (var item in RemovableDisk.RemovableDrives)
                {
                    operation.CopyFile($"{item.Key}\\F130", to, false);
                }
                RemovableDisk.RemovableDrives.Clear();

            }
            catch (Exception ex)
            {
                log.Debug(ex.ToString());
            }              
        }
    }
}
