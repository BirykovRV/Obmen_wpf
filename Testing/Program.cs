using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Obmen_wpf.Model;
using Obmen_wpf;
using System.Xml.Linq;
using System.Reflection;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument xdoc = XDocument.Load("Testing.exe.config");
            XElement root = xdoc.Element("configuration");

            foreach (var item in root.Elements("settings").ToList())
            {
                if (item.Attribute("name").Value == "version")
                {
                    item.Element("value").Value = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                    var version = new Version(item.Value);
                    Console.WriteLine(version);
                }
            }

            Console.ReadKey();
        }
    }
}
