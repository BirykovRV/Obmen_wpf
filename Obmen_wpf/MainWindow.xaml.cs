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
        private Operation operation;
        private RemovableDisk disk;

        private static Logger log = LogManager.GetCurrentClassLogger();

        public MainWindow()
        {
            InitializeComponent();
            operation = new Operation();
            disk = new RemovableDisk();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                disk.FindDisk();
                string to = "C:\\TestTo\\";
                foreach (var item in disk.RemovableDrives)
                {
                    operation.CopyFile($"{item.Key}\\F130", to, false);
                    //operation.CopyFile(fromArchive, toArchive, true);

                }
                disk.RemovableDrives.Clear();
            }
            catch (Exception ex)
            {
                log.Debug(ex.ToString());
            }            
            
        }
    }
}
