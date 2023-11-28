using System.Windows.Input;
using ZXY;

namespace SurApp.ViewModels;

internal class AzimuthWindowVM : NotifyPropertyObject
{

	public AzimuthWindowVM()
	{
		//设置为预编译状态下的数据
#if DEBUG
		startPoint = new Point("D01", 3805820.521, 333150.649, 0);
		endPoint = new Point("D02", 3805813.062, 333067.961, 0);
#else
	startPoint = new Point("", 0.0, 0.0, 0.0);
	endPoint = new Point("", 0.0, 0.0, 0.0);
#endif
	}

	private Point startPoint;
	public Point StartPoint
	{
		get => startPoint;
		set
		{
			startPoint = value;
			RaisePropertyChanged();
		}
	}

	private Point endPoint;
	public Point EndPoint
	{
		get => endPoint;
		set
		{
			endPoint = value;
			RaisePropertyChanged();
		}
	}

	private string azName = "起点->方向的坐标方位角:";
	public string AzName
	{
		get { return azName; }
		set
		{
			azName = value;
			RaisePropertyChanged("AzName");
		}
	}

	private string az = "";
	public string Az
	{
		get { return az; }
		set { az = value; RaisePropertyChanged(); }
	}

	private double dist;
	public double Dist
	{
		get { return dist; }
		set { dist = value; RaisePropertyChanged(); }
	}

	public void Calculate(object? parameter)
	{
		var ad = SurMath.Azimuth(StartPoint.X, StartPoint.Y, EndPoint.X, EndPoint.Y);

		Az = SurMath.RadianToString(ad.a);
		Dist = ad.d;

		AzName = $"{StartPoint.Name}->{EndPoint.Name}的坐标方位角";
	}

	public bool canCalculate(object? parameter)
	{
		return true;
	}

	public void Switch(object? parameter)
	{
		var tmp = StartPoint;
		StartPoint = EndPoint;
		EndPoint = tmp;
	}


	public ICommand CalculateCommand => new Commands.MyCommand(
		Calculate, canCalculate);


	public ICommand SwitchCommand => new Commands.MyCommand(
		Switch, canCalculate);
}


