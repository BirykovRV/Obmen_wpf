using System.Windows;

namespace Obmen_wpf.Properties {
    
    
    // Этот класс позволяет обрабатывать определенные события в классе параметров:
    //  Событие SettingChanging возникает перед изменением значения параметра.
    //  Событие PropertyChanged возникает после изменения значения параметра.
    //  Событие SettingsLoaded возникает после загрузки значений параметров.
    //  Событие SettingsSaving возникает перед сохранением значений параметров.
    internal sealed partial class Settings
    {

        public static bool IsMyPropertyChanged { get; private set; }

        public Settings() {
            // Для добавления обработчиков событий для сохранения и изменения параметров раскомментируйте приведенные ниже строки:

            this.SettingChanging += this.SettingChangingEventHandler;

            this.SettingsSaving += this.SettingsSavingEventHandler;

        }

        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) {
            // Добавьте здесь код для обработки события SettingChangingEvent.     
            if (sender != null)
            {
                IsMyPropertyChanged = true;
            }
            else
            {
                IsMyPropertyChanged = false;
            }
        }
        
        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            // Добавьте здесь код для обработки события SettingsSaving.
            MessageBox.Show("Настройки успешно сохранены!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            IsMyPropertyChanged = false;
        }
    }
}
