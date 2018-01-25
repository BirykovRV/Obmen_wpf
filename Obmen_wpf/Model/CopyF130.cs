using Obmen_wpf.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obmen_wpf.Model
{
    class CopyF130 : ICopyFiles
    {
        // Config
        string configFrom = Settings.Default.configFrom;
        string configTo = Settings.Default.configTo + "\\";
        // ASKU
        string f130From = Settings.Default.f130From;
        string f130To = Settings.Default.f130To + "\\";

        public void Start(string key)
        {
            // Config
            Operations.CopyFileAsync(key + configFrom, configTo, false);
            // ASKU
            Operations.CopyFileAsync(f130From, key + f130To, false);
        }
    }
}
