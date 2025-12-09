using CommunityToolkit.Mvvm.ComponentModel;
using SurApp.ViewModels;
using ZXY;

namespace SurApp.Models;

public partial class GPoint : ViewModelBase, IPoint
{
	[ObservableProperty]
	private string name = "";
	
	[ObservableProperty]
	private double x = 0.0;
	 
	[ObservableProperty]
	private double y = 0.0;
	 
	[ObservableProperty]
	private double _dmsB = 0.0;
	 
	[ObservableProperty]
	private double _dmsL = 0.0;
	 
	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(GammaDmsString))]
	private double _Gamma = 0.0;
	
	public string GammaDmsString => ZXY.SurMath.RadianToDmsString(Gamma);

	[ObservableProperty]
	private double _m = 0.0;
	
	public override string ToString() => $"{Name}, {X}, {Y}, {DmsB}, {DmsL}";
}
