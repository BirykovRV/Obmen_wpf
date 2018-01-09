using System.Xml.Linq;

namespace Obmen_wpf.Model
{
    class GetSettings
    {
        public void CreateXml()
        {
            XDocument xdoc = new XDocument(new XElement("Config",
                new XElement("F130",
                    new XAttribute("name", "f130"),
                    new XElement("from", "\\F130"),
                    new XElement("to", "D:\\TEST\\")),

                new XElement("PostPay",
                    new XAttribute("name", "postpay"),
                    new XElement("from", "\\PostPay"),
                    new XElement("to", "D:\\TEST\\"))));

            xdoc.Save("D:\\TEST\\config.xml");
        }
    }
}
