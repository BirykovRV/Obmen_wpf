using Obmen_wpf.Properties;

namespace Obmen_wpf.Model
{
    class CopyEspp : ICopyFiles
    {   
        public void Start(string key, string value, bool isInfoPoint)
        {
            // Pension
            string pensionFrom = Settings.Default.pensionFrom;
            string pensionTo = key + Settings.Default.pensionTo + "\\";
            // ESPP
            string esppFrom = key + Settings.Default.esppFrom;
            string esppTo = Settings.Default.esppTo + "\\";

            string serverPathTo = $"{value}/Пенсия/";
            string serverPathFrom = "ToOPS/ESPP/";

            if (isInfoPoint)
            {
                ServerFtpModel server = new ServerFtpModel();
                server.StartUpload(pensionTo, serverPathTo);
                server.StartDownload(serverPathFrom, esppFrom);
            }
            else
            {
                // Pension
                Operations.CopyFile(pensionFrom, pensionTo, false);
                //Delete old files
               // Operations.DeleteOldObj(pensionFrom);
                // ESPP
                Operations.CopyFile(esppFrom, esppTo, false);
            }                        
        }
    }
}
