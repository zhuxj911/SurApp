using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SurApp.Models;
using ZXY;

namespace SurApp.ViewModels;

public partial class AzimuthWindowVM : ViewModelBase
{

    public AzimuthWindowVM()
    {
        //设置为预编译状态下的数据
#if DEBUG
        StartPoint = new() { Name = "D01", X = 3805820.521, Y = 333150.649 };
        EndPoint = new() { Name = "D02", X = 3805813.062, Y = 333067.961 };
#else
	    StartPoint = new();
		EndPoint = new();
#endif
        StartPoint.PropertyChanged += OnPointPropertyChanged; //是否可修改为发消息
        EndPoint.PropertyChanged += OnPointPropertyChanged;
    }

    private void OnPointPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        CalculateCommand.NotifyCanExecuteChanged();
    }

    [ObservableProperty]
    private GeoPoint startPoint;
    

    [ObservableProperty]
    private GeoPoint endPoint;
    

    [ObservableProperty]
    private string azName = "起点->方向的坐标方位角:";
   
    [ObservableProperty]
    private string azValue = "";
       
    [ObservableProperty]
    private double distance;
    

    [RelayCommand]
    private void Switch()
    {
        (StartPoint, EndPoint) = (EndPoint, StartPoint);
    }
    
    [RelayCommand(CanExecute = nameof(CanCalculate))]
    private void Calculate()
    {
        var ad = StartPoint.Azimuth(EndPoint);

        AzValue = SurMath.RadianToDmsString(ad.a);
        Distance = ad.d;

        AzName = $"{StartPoint.Name}->{EndPoint.Name}的坐标方位角";
    }

    // 控制计算按钮是否可用   
    private bool CanCalculate => Math.Abs(StartPoint.X - EndPoint.X) >= 0.1 || Math.Abs(StartPoint.Y - EndPoint.Y) >= 0.1;
}


