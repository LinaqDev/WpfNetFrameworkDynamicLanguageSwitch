using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNetFrameworkDynamicLanguageSwitch.ViewModel
{
	public class BaseViewModel : INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;
		protected void RaisePropertyChanged(string propertyname)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
		}

		public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;
		public static void RaiseStaticPropertyChanged(string propName)
		{
			EventHandler<PropertyChangedEventArgs> handler = StaticPropertyChanged;
			if (handler != null)
				handler(null, new PropertyChangedEventArgs(propName));

		}

	}
}
