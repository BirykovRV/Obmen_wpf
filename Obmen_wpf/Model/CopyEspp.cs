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
        // Pension
        string pensionFrom = Settings.Default.pensionFrom;
        string pensionTo = Settings.Default.pensionTo + "\\";
        // ESPP
        string esppFrom = Settings.Default.esppFrom;
        string esppTo = Settings.Default.esppTo + "\\";

        public void Start(string key)
        {
            // Pension
            Operations.CopyFileAsync(pensionFrom, key + pensionTo, false);
            // ESPP
            Operations.CopyFileAsync(key + esppFrom, esppTo, false);
        }
    }
}
