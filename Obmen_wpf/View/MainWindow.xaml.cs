using Obmen_wpf.Model;
using System.Configuration;
using System.Diagnostics;
using System.Windows;

namespace Obmen_wpf.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Если требуется обновить настройки из прошлой версии
            if (Properties.Settings.Default.UpgradeRequired)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpgradeRequired = false;
                Properties.Settings.Default.Save();
            }
            // Проверка на наличие новой версии
            if (UpdateChecker.CheckUpdate("Obmen_wpf.exe"))
            {
                System.Windows.Forms.MessageBox.Show("Обнаружена новая версия программы. \nНажмите \"Ок\" " +
                    "и программа будет автоматически обновлена.", 
                    "Внимание!", 
                    System.Windows.Forms.MessageBoxButtons.OK, 
                    System.Windows.Forms.MessageBoxIcon.Asterisk);
                
                Process.Start("Updater.exe");
            }                
        }
    }
}
