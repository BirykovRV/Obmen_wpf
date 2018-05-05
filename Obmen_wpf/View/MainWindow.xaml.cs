using Obmen_wpf.Model;
using Obmen_wpf.ViewModel;
using System;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Xml.Linq;

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
            if (UpdateChecker.CheckUpdate("Obmen_wpf.exe"))
            {
                System.Windows.Forms.MessageBox.Show("Обнаружена новая версия программы. \nНажмите \"Ок\" и программа будет автоматически обновлена.", "Внимание!", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);
                Process.Start("Updater.exe");
            }
        }        
    }
}
