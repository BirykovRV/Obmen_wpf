using Microsoft.Win32;
using Obmen_wpf.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Obmen_wpf.ViewModel
{
    class UserControlViewModel : INotifyPropertyChanged
    {

        public ICommand OnOpenDialog
        {
            get
            {
                return new Command(o =>
                {
                    switch (o.ToString())
                    {
                        case "asku":
                            DoOperation(TypeOfOperation.ASKU);
                            break;
                        case "config":
                            DoOperation(TypeOfOperation.CONFIG);
                            break;
                        case "post_update":
                            DoOperation(TypeOfOperation.POSTPAY_UPDATE);
                            break;
                        case "post_db":
                            DoOperation(TypeOfOperation.POSTPAY_DB);
                            break;
                        case "post_reg":
                            DoOperation(TypeOfOperation.POSTPAY_REG);
                            break;
                        case "espp":
                            DoOperation(TypeOfOperation.ESPP);
                            break;
                        case "pension":
                            DoOperation(TypeOfOperation.PENSION);
                            break;
                        case "fsg_reg":
                            DoOperation(TypeOfOperation.FSG_REG);
                            break;
                        case "fsg_cash":
                            DoOperation(TypeOfOperation.FSG_CASH);
                            break;

                    }
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string sender = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(sender));
        }

        private void DoOperation(TypeOfOperation type)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            switch (type)
            {
                case TypeOfOperation.ASKU:
                    Properties.Settings.Default.f130From = dialog.SelectedPath;
                    break;
                case TypeOfOperation.CONFIG:
                    Properties.Settings.Default.configTo = dialog.SelectedPath;
                    break;
                case TypeOfOperation.POSTPAY_UPDATE:
                    Properties.Settings.Default.postPayUpdateTo = dialog.SelectedPath;
                    break;
                case TypeOfOperation.POSTPAY_DB:
                    Properties.Settings.Default.postPayDBTo = dialog.SelectedPath;
                    break;
                case TypeOfOperation.POSTPAY_REG:
                    Properties.Settings.Default.postPayRegFrom = dialog.SelectedPath;
                    break;
                case TypeOfOperation.ESPP:
                    Properties.Settings.Default.esppTo = dialog.SelectedPath;
                    break;
                case TypeOfOperation.PENSION:
                    Properties.Settings.Default.pensionFrom = dialog.SelectedPath;
                    break;
                case TypeOfOperation.FSG_REG:
                    Properties.Settings.Default.fsgRegFrom = dialog.SelectedPath;
                    break;
                case TypeOfOperation.FSG_CASH:
                    Properties.Settings.Default.cashFsgTo = dialog.SelectedPath;
                    break;
                default:
                    break;
            }
        }
    }
}

enum TypeOfOperation
{
    ASKU,
    CONFIG,
    POSTPAY_UPDATE,
    POSTPAY_DB,
    POSTPAY_REG,
    ESPP,
    PENSION,
    FSG_REG,
    FSG_CASH
}