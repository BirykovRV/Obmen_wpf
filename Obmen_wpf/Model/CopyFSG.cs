﻿using Obmen_wpf.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obmen_wpf.Model
{
    class CopyFSG : ICopyFiles
    {
        public void Start(string key, string value)
        {
            // FSG Reg
            string fsgRegFrom = Settings.Default.fsgRegFrom;
            string fsgRegTo = Settings.Default.fsgRegTo + "\\";
            // FSG Cash
            string cashFsgFrom = Settings.Default.cashFsgFrom;
            string cashFsgTo = Settings.Default.cashFsgTo + "\\";
            // FSG Reg
            Operations.CopyFile(fsgRegFrom, key + fsgRegTo, false);
            // FSG Cash
            Operations.CopyFile(key + cashFsgFrom, cashFsgTo, false);
        }
    }
}
