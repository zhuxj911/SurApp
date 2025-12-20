using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using SurApp.Models;
using SurApp.Services;
using ZXY;

namespace SurApp.ViewModels;

public partial class ProjViewModel : ViewModelBase {
    public void SetCoordinateSystem(CoordinateSystem coordinateSystem) {
        CurrentEllipsoid = coordinateSystem.CurrentEllipsoid;
        DmsL0 = coordinateSystem.DmsL0;
        YKM= coordinateSystem.YKM;
        NY= coordinateSystem.NY;

        foreach (var item in coordinateSystem.PointList) {
            PointList.Add(new GPointViewModel(item));
        }
    }

    public CoordinateSystem GetCoordinateSystem() {
        var coordinateSystem = new CoordinateSystem() {
            CurrentEllipsoid = this.CurrentEllipsoid,
            DmsL0 = this.DmsL0,
            YKM = this.YKM,
            NY = this.NY,
        };
     
        foreach (var item in this.PointList) {
            coordinateSystem.PointList.Append( item.GetGPoint() );
        }
        return coordinateSystem;
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Title))]
    private string fileName = "untitle";

    public string Title => $"测量螺丝刀(Version©)-{FileName}";

    public List<Ellipsoid> EllipsoidList { get; } = EllipsoidFactory.EllipsoidList;


    [ObservableProperty]
    private Ellipsoid currentEllipsoid = EllipsoidFactory.EllipsoidList[0];

    [ObservableProperty]
    private double _dmsL0;

    [ObservableProperty]
    private double _YKM;

    [ObservableProperty]
    private int _NY;

    [ObservableProperty]
    private ObservableCollection<GPointViewModel> _pointList = [
#if DEBUG
      new GPointViewModel(new GPoint(){Name = "GP01",DmsB=21.58470845,DmsL=2.25314880}),
      new GPointViewModel(new GPoint(){Name = "GP02",DmsB=30.30, DmsL=2.20}),
#endif
	];

    #region Commands

    [RelayCommand]
    private void New() {
        CurrentEllipsoid = EllipsoidFactory.EllipsoidList[0];
        DmsL0 = 0;
        YKM = 0;
        NY = 0;
        PointList.Clear();
    }


    [RelayCommand]
    private async Task Open() {
        var dlg = new OpenFileDialog {
            DefaultExt = ".txt",
            Filter = "高斯投影数据文件|*.txt|All File(*.*)|*.*"
        };
        if (dlg.ShowDialog() != true) return;
        FileName = dlg.FileName;

        SetCoordinateSystem(await CoordinateSystem.ReadDataTextFile(FileName) );
    }

    [RelayCommand]
    private async Task OpenJson() {
        var dlg = new OpenFileDialog {
            DefaultExt = ".json",
            Filter = "Json文件|*.json|All File(*.*)|*.*"
        };
        if (dlg.ShowDialog() != true) return;
        FileName = dlg.FileName;

        SetCoordinateSystem(await CoordinateSystemService.LoadFromFileAsync(FileName));
    }


    [RelayCommand]
    private async Task Save() {
        if (FileName == "untitle")
            await SaveAs();
        else
            await GetCoordinateSystem().WriteFile(FileName); 
    }

    [RelayCommand]
    private async Task SaveAs() {
        var dlg = new SaveFileDialog {
            DefaultExt = ".txt",
            Filter = "高斯投影数据文件|*.txt|All File(*.*)|*.*"
        };
        if (dlg.ShowDialog() != true) return;
        FileName = dlg.FileName;

        await GetCoordinateSystem().WriteFile(FileName);
    }

    [RelayCommand]
    private async Task SaveJson() {
        var dlg = new SaveFileDialog {
            DefaultExt = ".json",
            Filter = "Json文件|*.json|All File(*.*)|*.*"
        };
        if (dlg.ShowDialog() != true) return;
        FileName = dlg.FileName;

        await CoordinateSystemService.SaveToFileAsync(GetCoordinateSystem(), FileName);
    }

    [RelayCommand]
    private void BLtoXY() {
        IProj proj = new GaussProj(CurrentEllipsoid);
        double L0 = SurMath.DmsToRadian(this.DmsL0);
        foreach (var pnt in PointList) {
            var B = SurMath.DmsToRadian(pnt.DmsB);
            var L = SurMath.DmsToRadian(pnt.DmsL);
            var (X, Y, gamma, m) = proj.BLtoXY(B, L, L0, YKM, NY);
            pnt.X = X;
            pnt.Y = Y;
            pnt.Gamma = gamma;
            pnt.M = m;
        }
    }

    [RelayCommand]
    private void XYtoBL() {
        var proj = new GaussProj(CurrentEllipsoid);
        var L0 = SurMath.DmsToRadian(this.DmsL0);
        foreach (var pnt in PointList) {
            var (B, L, gamma, m) = proj.XYtoBL(pnt.X, pnt.Y, L0, YKM, NY);
            pnt.DmsB = SurMath.RadianToDms(B);
            pnt.DmsL = SurMath.RadianToDms(L);
            pnt.Gamma = gamma;
            pnt.M = m;
        }
    }


    [RelayCommand]
    private void ClearXY() {
        foreach (var pnt in PointList) {
            pnt.X = 0;
            pnt.Y = 0;
            pnt.Gamma = 0;
            pnt.M = 0;
        }
    }


    [RelayCommand]
    private void ClearBL() {
        foreach (var pnt in PointList) {
            pnt.DmsB = 0;
            pnt.DmsL = 0;
            pnt.Gamma = 0;
            pnt.M = 0;
        }
    }


    [RelayCommand]
    private void CalculateAzimuth() {
        var win = new AzimuthWindow();
        win.ShowDialog();
    }
    #endregion
}