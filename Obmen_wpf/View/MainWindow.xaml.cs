using Obmen_wpf.Model;
using Obmen_wpf.ViewModel;
using System;
using System.Deployment.Application;
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

            XDocument xdoc = XDocument.Load("Obmen_wpf.exe.config");
            XElement root = xdoc.Element("userSettings");

            foreach (var item in root.Elements("settings").ToList())
            {
                if (item.Attribute("name").Value == "version")
                {
                    item.Element("value").Value = Assembly.GetExecutingAssembly().GetName().Version.ToString();                                       
                }
            }

        }
    }
}
