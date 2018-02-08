using Obmen_wpf.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obmen_wpf.Model
{
    class CopyEspp : ICopyFiles
    {   
        public void Start(string key, string value)
        {
            // Pension
            string pensionFrom = Settings.Default.pensionFrom;
            string pensionTo = Settings.Default.pensionTo + "\\";
            // ESPP
            string esppFrom = Settings.Default.esppFrom;
            string esppTo = Settings.Default.esppTo + "\\";
            // Pension
            Operations.CopyFile(pensionFrom, key + pensionTo, false);
            // ESPP
            Operations.CopyFile(key + esppFrom, esppTo, false);            
        }
    }
}
