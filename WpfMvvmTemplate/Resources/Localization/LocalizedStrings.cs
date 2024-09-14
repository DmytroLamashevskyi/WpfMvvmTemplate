using System.ComponentModel;
using System.Globalization;
using System.Resources;
using WpfMvvmTemplate.Services;

namespace WpfMvvmTemplate.Resources.Localization
{
    public class LocalizedStrings : INotifyPropertyChanged
    {
        private static ResourceManager _resourceManager = new ResourceManager("WpfMvvmTemplate.Resources.Localization.Strings", typeof(LocalizedStrings).Assembly);

        public event PropertyChangedEventHandler PropertyChanged;

        public string this[string key]
        {
            get { return _resourceManager.GetString(key, CultureInfo.CurrentUICulture); }
        }

        public LocalizedStrings()
        {
            LocalizationService.Instance.PropertyChanged += (s, e) =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
            };
        }
    }
}
