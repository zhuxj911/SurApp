using System.IO;
using ZXY;

namespace SurApp.Models;

public class CoordinateSystem {
    public Ellipsoid CurrentEllipsoid { get; set; } = EllipsoidFactory.EllipsoidList[0];

    public double DmsL0 { get; set; }

    public double YKM { get; set; }

    public int NY { get; set; }

    public List<GPoint> PointList { get; } = [
//#if DEBUG
//    new GPoint(){Name = "GP01",DmsB=21.58470845,DmsL=2.25314880},
//    new GPoint(){Name = "GP02",DmsB=30.30, DmsL=2.20}
//#endif
	];

    private static Dictionary<string, Ellipsoid> Ellipsoids => EllipsoidFactory.Ellipsoids;
    public static async Task<CoordinateSystem> ReadDataTextFile(string fileName) {
        CoordinateSystem cs = new();

        using var sr = new StreamReader(fileName);
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
                        // "BJ54" "XA80" "WGS84" "CGCS2000" "CS00"
                        cs.CurrentEllipsoid = Ellipsoids[its[0]];
                        if (its is ["CS00", _, _])
                        {
                            cs.CurrentEllipsoid.a = double.TryParse(its[1], out var va) ? va : 0.0;
                            cs.CurrentEllipsoid.f = double.TryParse(its[2], out var vf) ? vf : 1.0;
                        }
                        break;
                    case "L0":
                        cs.DmsL0 = double.TryParse(items[1], out var vL0) ? vL0 : 0.0;
                        break;
                    case "YKM":
                        cs.YKM = double.TryParse(items[1], out var vYKM) ? vYKM : 0.0;
                        break;
                    case "N":
                        cs.NY = int.TryParse(items[1], out var vN) ? vN : 0;
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

            cs.PointList.Add(pnt);
        }
        return cs;
    }

    public async Task WriteFile(string fileName) {
        using var sw = new StreamWriter(fileName);
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
}
