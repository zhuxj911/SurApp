using CommunityToolkit.Mvvm.ComponentModel;
using SurApp.Models;

namespace SurApp.ViewModels;

public partial class GPointViewModel : ViewModelBase
{
	public GPointViewModel(GPoint point) {
		Name = point.Name;
		X = point.X;
		Y = point.Y;
		DmsB = point.DmsB;
        DmsL = point.DmsL;
        Gamma = point.Gamma;
        M = point.M;
	}

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private double x;

    [ObservableProperty]
    private double y;

    [ObservableProperty]
    private double dmsB;

    [ObservableProperty]
    private double dmsL;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(GammaDmsString))]
    private double gamma;
 
	public string GammaDmsString => ZXY.SurMath.RadianToDmsString(Gamma);

    [ObservableProperty]
    private double m;

    public GPoint GetGPoint() {
        return new GPoint() {
            Name = this.Name,
            X = this.X,
            Y = this.Y,
            DmsB = this.DmsB,
            DmsL = this.DmsL,
            Gamma = this.Gamma,
            M = this.M,
        };
    }
}
