using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Obmen_wpf.Model;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Ftp ftp = new Ftp("ftp://10.87.6.143/", "support", "trd19afo");

            ftp.Download("test/", "D:\\TEST\\");
        }        
    }
}
