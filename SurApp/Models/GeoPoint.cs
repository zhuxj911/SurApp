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
		get
		{
			return y;
		}
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
	public double dmsB
	{
		get
		{
			return _dmsB;
		}
		set
		{
			_dmsB = value;
			RaisePropertyChanged();
		}
	}

	private double _dmsL = 0.0;
	public double dmsL
	{
		get
		{
			return _dmsL;
		}
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

	public string GammaDMSString => ZXY.SurMath.RadianToDmsString(Gamma);

	private double _m = 0.0;
	public double m
	{
		get => _m;
		set
		{
			_m = value;
			RaisePropertyChanged();
		}
	}

	public override string ToString()
	{
		return $"{Name}, {X}, {Y}, {dmsB}, {dmsL}";
	}
}
