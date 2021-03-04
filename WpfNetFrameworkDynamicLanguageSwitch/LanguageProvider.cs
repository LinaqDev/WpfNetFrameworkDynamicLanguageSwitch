using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfNetFrameworkDynamicLanguageSwitch
{
    public abstract class LanguageProvider
    {
        private static LanguageProvider instance = new ResStringLanguageProvider();
        public static LanguageProvider Instance
        {
            get => instance;
            set
            {
                if (instance == value)
                    return;
                if (value != null)
                {
                    value.references.Clear();
                    value.references.AddRange(instance.references);
                }
                instance = value;
            }
        }
        public static event EventHandler CultureChanged = delegate { };

        protected readonly List<TExtension> references = new List<TExtension>();
        public void TrackInstance(TExtension item)
        {
            if (!references.Contains(item))
                references.Add(item);
        }
        public void StopTrackingInstance(TExtension item) => references.Remove(item);
        public void SetCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            UpdateToCulture(culture);
            foreach (var reference in references)
                reference.CultureChanged();
            CultureChanged.Invoke(this, new EventArgs());
        }
        public virtual void UpdateToCulture(CultureInfo newCulture) { }
        public abstract string GetValue(string key);
    }

    public class ResStringLanguageProvider : LanguageProvider
    {
        public override string GetValue(string key)
        {
            var result = Localization.Language.ResourceManager.GetString(key);
            if (result == null)
            {
                //Log.Warning($"Translation not found for key: {key}");
                return key;
            } 
            return result;
        }
    }
}
