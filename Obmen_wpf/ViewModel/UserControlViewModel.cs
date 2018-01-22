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

        public TypeOfOperation Asku { get; } = TypeOfOperation.ASKU;
        public TypeOfOperation Config { get; } = TypeOfOperation.CONFIG;
        public TypeOfOperation PostPayUpdate { get; } = TypeOfOperation.POSTPAY_UPDATE;
        public TypeOfOperation PostPayDB { get; } = TypeOfOperation.POSTPAY_DB;
        public TypeOfOperation PostPayReg { get; } = TypeOfOperation.POSTPAY_REG;
        public TypeOfOperation Espp { get; } = TypeOfOperation.ESPP;
        public TypeOfOperation Pension { get; } = TypeOfOperation.PENSION;
        public TypeOfOperation FsgReg { get; } = TypeOfOperation.FSG_REG;
        public TypeOfOperation FsgCash { get; } = TypeOfOperation.FSG_CASH;

        public ICommand OnOpenDialog
        {
            get
            {
                return new Command(o =>
                {
                    FolderBrowserDialog dialog = new FolderBrowserDialog();
                    dialog.ShowDialog();

                    switch (o)
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
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string sender = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(sender));
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