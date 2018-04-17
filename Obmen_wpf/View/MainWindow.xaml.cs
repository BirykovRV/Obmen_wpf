using Obmen_wpf.Model;
using Obmen_wpf.ViewModel;
using System;
using System.Reflection;
using System.Deployment.Application;
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
            Version ver = ApplicationDeployment.CurrentDeployment.CurrentVersion;
            Title = "Офлайн Транспорт    v. " + ver;
        }        
    }
}
