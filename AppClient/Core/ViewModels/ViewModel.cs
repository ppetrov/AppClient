using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppClient.Core.ViewModels
{
	/// <summary>
	/// Defines the base class for all view models
	/// </summary>
	public abstract class ViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
		{
			if (!EqualityComparer<T>.Default.Equals(field, value))
			{
				field = value;
				this.OnPropertyChanged(propertyName);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Get the formatted value of an empty indicator('-') when the value is empty
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string GetDisplayValue(string value)
		{
			return string.IsNullOrWhiteSpace(value) ? @"-" : value;
		}
	}
}