using Obmen_wpf.Model;
using Obmen_wpf.ViewModel;
using System;
using System.Reflection;
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
            var ver = Assembly.GetExecutingAssembly().GetName().Version;
            Title = "Офлайн транспорт\t ver. " + ver;
        }        
    }
}
