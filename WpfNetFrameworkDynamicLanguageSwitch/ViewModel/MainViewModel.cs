using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNetFrameworkDynamicLanguageSwitch.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            SelectedLanguage = Languages.First();
            LanguageProvider.CultureChanged += LanguageProvider_LanguageChanged;
        }

        private void LanguageProvider_LanguageChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("");
        }

        public List<string> Languages { get; set; } = new List<string>() { "pl-PL", "en-US" };

        private string _selectedLanguage;
        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                _selectedLanguage = value;
                RaisePropertyChanged(nameof(SelectedLanguage));
                LanguageProvider.Instance.SetCulture(new CultureInfo(value));
            }
        }

        public string Text1
        {
            get => LanguageProvider.Instance.GetValue("Polish");
        }
        public string Text2
        {
            get => LanguageProvider.Instance.GetValue("English");
        }
    }
}
