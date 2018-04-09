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
    class UserControlViewModel
    {
        /// <summary>
        /// Файлы Ф130 для АСКУ
        /// </summary>
        public TypeOfOperation Asku { get; } = TypeOfOperation.ASKU;
        /// <summary>
        /// Настройки для Ф130
        /// </summary>
        public TypeOfOperation Config { get; } = TypeOfOperation.CONFIG;
        /// <summary>
        /// Обновление модуля PoastPay
        /// </summary>
        public TypeOfOperation PostPayUpdate { get; } = TypeOfOperation.POSTPAY_UPDATE;
        /// <summary>
        /// БД для postpay
        /// </summary>
        public TypeOfOperation PostPayDB { get; } = TypeOfOperation.POSTPAY_DB;
        /// <summary>
        /// Реестр Postpay
        /// </summary>
        public TypeOfOperation PostPayReg { get; } = TypeOfOperation.POSTPAY_REG;
        /// <summary>
        /// Справочники по переводам
        /// </summary>
        public TypeOfOperation Espp { get; } = TypeOfOperation.ESPP;
        /// <summary>
        /// Файлы по выплате пенсии
        /// </summary>
        public TypeOfOperation Pension { get; } = TypeOfOperation.PENSION;
        /// <summary>
        /// Реестр ФСГ
        /// </summary>
        public TypeOfOperation FsgReg { get; } = TypeOfOperation.FSG_REG;
        /// <summary>
        /// Кэш для ФСГ
        /// </summary>
        public TypeOfOperation FsgCash { get; } = TypeOfOperation.FSG_CASH;
        /// <summary>
        /// Список
        /// </summary>
        public TypeOfOperation List_T { get; } = TypeOfOperation.LIST_T;
 
        /// <summary>
        /// Открывает диалог выбора каталога и сохраняет его в настройках программы
        /// </summary>
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
                        case TypeOfOperation.LIST_T:
                            Properties.Settings.Default.listTo = dialog.SelectedPath;
                            break;
                        default:
                            break;

                    }
                });
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
    FSG_CASH,
    LIST_T
}