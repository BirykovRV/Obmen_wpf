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

            GetSettings set = new GetSettings();
            set.CreateXml();

            //try
            //{
            //    if (RemovableDisk.FindDisk())
            //    {                    
            //        foreach (var item in RemovableDisk.RemovableDrives)
            //        {
            //            string to = $"D:\\TEST\\{item.Value}\\";
            //            string from = $"{item.Key}\\F130";
            //            operation.CopyFile(from, to, false);
            //        }
            //        RemovableDisk.RemovableDrives.Clear();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Не найдены USB накопители");
            //    }                
            //}
            //catch (Exception ex)
            //{
            //    log.Debug(ex.ToString());
            //}              
        }
    }
}
