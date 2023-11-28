using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SurApp.ViewModels;

public class NotifyPropertyObject : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;

	public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
	{
		PropertyChanged?.Invoke(this,
				new PropertyChangedEventArgs(propertyName));
	}
}