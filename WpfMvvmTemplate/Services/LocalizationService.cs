using System.ComponentModel;
using System.Globalization;

namespace WpfMvvmTemplate.Services
{
    public class LocalizationService : INotifyPropertyChanged
    {
        private static LocalizationService _instance;
        public static LocalizationService Instance => _instance ??= new LocalizationService();

        public event PropertyChangedEventHandler PropertyChanged;

        public void ChangeCulture(string cultureName)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
            OnPropertyChanged(string.Empty); 
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
