using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SignalRChat
{
	public abstract class BaseViewModel : INotifyPropertyChanged
	{
		protected string _errorMessage;
		protected bool _displaySpinner;
		protected bool _displayErrorMessage;

		public bool DisplaySpinner
		{
			get { return _displaySpinner; }
			set { SetProperty(ref _displaySpinner, value); }
		}

		public string ErrorMessage
		{
			get { return _errorMessage; }
			set { SetProperty(ref _errorMessage, value); }
		}

		public bool DisplayErrorMessage
		{
			get { return _displayErrorMessage; }
			set { SetProperty(ref _displayErrorMessage, value); }
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
		{
			if (Object.Equals(storage, value))
				return false;

			storage = value;
			OnPropertyChanged(propertyName);
			return true;
		}

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}

