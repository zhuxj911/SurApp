using System.Collections.ObjectModel;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using SurApp.Models;
using ZXY;

namespace SurApp.ViewModels;

public partial class ProjViewModel : ViewModelBase {
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Title))]
    private string fileName = "untitle";

    public string Title => $"测量螺丝刀(Version©)-{FileName}";

    public List<Ellipsoid> EllipsoidList { get; } = EllipsoidFactory.EllipsoidList;

    private static Dictionary<string, Ellipsoid> Ellipsoids => EllipsoidFactory.Ellipsoids;

    [ObservableProperty]
    private Ellipsoid currentEllipsoid = EllipsoidFactory.EllipsoidList[0];

    [ObservableProperty]
    private double _dmsL0;

    [ObservableProperty]
    private double _YKM;

    [ObservableProperty]
    private int _NY;

    public ObservableCollection<GPoint> PointList { get; } = [
#if DEBUG
      new GPoint(){Name = "Gp01",DmsB=21.58470845,DmsL=2.25314880},
      new GPoint(){Name = "Gp02",DmsB=30.30, DmsL=2.20},
#endif
	];

    public bool IsValidated() => PointList.Count > 0;

    #region Commands

    [RelayCommand]
    private void New() {
        CurrentEllipsoid = Ellipsoids["CGCS2000"];
        DmsL0 = 0;
        YKM = 0;
        NY = 0;
        PointList.Clear();
    }


    [RelayCommand]
    private void Open() {
        var dlg = new OpenFileDialog {
            DefaultExt = ".txt",
            Filter = "高斯投影数据文件|*.txt|All File(*.*)|*.*"
        };
        if (dlg.ShowDialog() != true) return;
        FileName = dlg.FileName;

        using var sr = new StreamReader(FileName);
        PointList.Clear();
        while (true) {
            var buffer = sr.ReadLine();
            if (buffer == null) break;

            buffer = buffer.Trim();
            if (buffer == string.Empty) continue;

            if (buffer[0] == '#') continue;

            string[]? items = null;
            if (buffer.Contains(':')) {
                items = buffer.Split([':']);
                var cap = items[0].Trim();
                switch (cap) {
                    case "CS":
                        var its = items[1].Trim().Split([',']);
                        if (its is ["CS00", _, _]) //if (its.Length == 3 && its[0] == "CS00")
                        {
                            CurrentEllipsoid = Ellipsoids["CS00"];
                            CurrentEllipsoid.a = double.TryParse(its[1], out var va) ? va : 0.0;
                            CurrentEllipsoid.f = double.TryParse(its[2], out var vf) ? vf : 1.0;
                        } else {// "BJ54" "XA80" "WGS84" "CGCS2000"
                            CurrentEllipsoid = Ellipsoids[its[0]];
                        }
                        break;
                    case "L0":
                        DmsL0 = double.TryParse(items[1], out var vL0) ? vL0 : 0.0;
                        break;
                    case "YKM":
                        YKM = double.TryParse(items[1], out var vYKM) ? vYKM : 0.0;
                        break;
                    case "N":
                        this.NY = int.TryParse(items[1], out var vN) ? vN : 0;
                        break;
                }
                continue;
            }

            items = buffer.Split([',']);
            if (items.Length < 3) continue; //少于三项数据，不是点的坐标数据，忽略
            var pnt = new GPoint {
                Name = items[0].Trim(),
                X = double.TryParse(items[1], out var vx) ? vx : 0.0,
                Y = double.TryParse(items[2], out var vy) ? vy : 0.0
            };

            if (items.Length >= 5) {
                //默认为 D.MMSS
                pnt.DmsB = double.TryParse(items[3], out var vB) ? vB : 0.0;
                pnt.DmsL = double.TryParse(items[4], out var vL) ? vL : 0.0;
            }

            this.PointList.Add(pnt);
        }
    }

    private void WriteFile() {
        using var sw = new StreamWriter(FileName);
        sw.WriteLine("#数据文件中的 # : , 均应为英文字符");
        sw.WriteLine("#以#开头的行视为注释行");
        sw.WriteLine("#可以忽略0个空格的行");
        sw.WriteLine("#可以忽略有多个空格的行");
        sw.WriteLine();

        sw.WriteLine("#CS 指定坐标系 BJ54 XA80 CGCS2000 WGS84 CS00");
        sw.WriteLine("#CS: BJ54");
        sw.WriteLine("#CS: XA80");
        sw.WriteLine("#CS: WGS84");
        sw.WriteLine("#CS: CGCS2000");
        sw.WriteLine("#CS: CS00, 6378137, 298.257222101");
        sw.WriteLine(CurrentEllipsoid.Id == EllipsoidType.CS00
            ? $"CS: {CurrentEllipsoid.Id}, {CurrentEllipsoid.a}, {CurrentEllipsoid.f}"
            : $"CS: {CurrentEllipsoid.Id}");

        sw.WriteLine();

        sw.WriteLine($"L0: {DmsL0}");
        sw.WriteLine($"YKM: {YKM}");
        sw.WriteLine($"N: {NY}");

        sw.WriteLine();
        sw.WriteLine("#点名, X, Y, B, L");
        foreach (var pnt in PointList) {
            sw.WriteLine(pnt);
        }

        sw.Close();
    }

    [RelayCommand]
    private void Save() {
        if (FileName == "untitle")
            SaveAs();
        else
            WriteFile();
    }


    [RelayCommand]
    private void SaveAs() {
        var dlg = new SaveFileDialog {
            DefaultExt = ".txt",
            Filter = "高斯投影数据文件|*.txt|All File(*.*)|*.*"
        };
        if (dlg.ShowDialog() != true) return;
        FileName = dlg.FileName;

        WriteFile();
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