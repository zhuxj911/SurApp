using System.Windows.Input;
using SurApp.Commands;
using SurApp.Models;
using ZXY;

namespace SurApp.ViewModels;

internal class AzimuthWindowVM : ViewModelBase
{

    public AzimuthWindowVM()
    {
        //设置为预编译状态下的数据
#if DEBUG
        startPoint = new() { Name = "D01", X = 3805820.521, Y = 333150.649 };
        endPoint = new() { Name = "D02", X = 3805813.062, Y = 333067.961 };
#else
	    startPoint = new();
		endPoint = new();
#endif
    }

    private GeoPoint startPoint;
    public GeoPoint StartPoint
    {
        get => startPoint;
        set
        {
            if(startPoint != value)
            {
                startPoint = value;
                RaisePropertyChanged();
            }
            
        }
    }

    private GeoPoint endPoint;
    public GeoPoint EndPoint
    {
        get => endPoint;
        set
        {
            if(endPoint != value)
            {
                endPoint = value;
                RaisePropertyChanged();
            }
        }
    }

    private string azName = "起点->方向的坐标方位角:";
    public string AzName
    {
        get => azName;
        set
        {
            if(azName != value)
            {
                azName = value;
                RaisePropertyChanged("AzName");
            }
        }
    }

    private string azValue = "";
    public string AzValue
    {
        get => azValue;
        set
        {
            if(azValue != value)
            {
                azValue = value;
                RaisePropertyChanged();
            }
        }
    }   

    private double distance;
    public double Distance
    {
        get => distance; 
        set 
        { 
            if(distance != value)
            {
                distance = value; RaisePropertyChanged();
            }
        }
    }

    private void Switch()
    {
        (StartPoint, EndPoint) = (EndPoint, StartPoint);
    }
    public ICommand SwitchCommand => new Commands.RelayCommand( (_) => Switch() );

    private void Calculate()
    {
        var ad = StartPoint.Azimuth(EndPoint);

        AzValue = SurMath.RadianToDmsString(ad.a);
        Distance = ad.d;

        AzName = $"{StartPoint.Name}->{EndPoint.Name}的坐标方位角";
    }

    // 控制计算按钮是否可用   
    private bool CanCalculate => Math.Abs(StartPoint.X - EndPoint.X) >= 0.1 || Math.Abs(StartPoint.Y - EndPoint.Y) >= 0.1;
    public ICommand CalculateCommand => new RelayCommand( (_) => Calculate(), (_) => CanCalculate);
}


