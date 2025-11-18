using SurApp.ViewModels;
using ZXY;

namespace SurApp.Models;

public class GeoPoint : ViewModelBase, IPoint
{
	private string name = "";
	public string Name
	{
		get => name;
		set
		{
			name = value;
			RaisePropertyChanged();
		}
	}


	private double x = 0.0;
	public double X
	{
		get => x;
		set
		{
			if(value >= 0)
			{
				x = value;
				RaisePropertyChanged();
			}
		}
	}

	private double y = 0.0;
	public double Y
	{
		get => y;
		set
		{
			if(value >= 0)
			{
				y = value;
				RaisePropertyChanged();
			}
		}
	}

	private double _dmsB = 0.0;
	public double DmsB
	{
		get => _dmsB;
		set
		{
			_dmsB = value;
			RaisePropertyChanged();
		}
	}

	private double _dmsL = 0.0;
	public double DmsL
	{
		get => _dmsL;
		set
		{
			_dmsL = value;
			RaisePropertyChanged();
		}
	}

	private double _Gamma = 0.0;
	public double Gamma
	{
		get => _Gamma;
		set
		{
			_Gamma = value;
			RaisePropertyChanged();
			RaisePropertyChanged("GammaDMSString");
		}
	}

	public string GammaDmsString => ZXY.SurMath.RadianToDmsString(Gamma);

	private double _m = 0.0;
	public double M
	{
		get => _m;
		set
		{
			_m = value;
			RaisePropertyChanged();
		}
	}

	public override string ToString() => $"{Name}, {X}, {Y}, {DmsB}, {DmsL}";
}
