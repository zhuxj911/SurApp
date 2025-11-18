using System.Collections.Generic;

namespace ZXY;

/// <summary>
/// 参考椭球类型
/// </summary>
public enum EllipsoidType
{
    CS00 =0,
    BJ54=1,
    XA80=2,
    WGS84=3,
    CGCS2000=4
}

public static class EllipsoidFactory
{
    public static List<Ellipsoid> EllipsoidList { get; } = [];
    
    //用于数据文件的读写，根据参考椭球带号查找相应椭球
    public static Dictionary<string, Ellipsoid> Ellipsoids { get; } = new Dictionary<string, Ellipsoid>();

    static EllipsoidFactory()
    {
        EllipsoidList.Add(
            new Ellipsoid(6378137, 298.257222101) { Id = "CGCS2000", Name = "CGCS2000大地坐标系"});
        EllipsoidList.Add(
            new Ellipsoid(6378245, 298.3) { Id = "BJ54", Name = "北京54坐标系" });
        EllipsoidList.Add(
            new Ellipsoid(6378140, 298.257) { Id = "XA80", Name = "西安80坐标系" });
        EllipsoidList.Add(
            new Ellipsoid(6378137, 298.257223560){Id = "WGS84", Name = "WGS84大地坐标系"});
        EllipsoidList.Add(
            new Ellipsoid(6378137, 298.257222101){ Id = "CS00", Name = "自定义坐标系" });

        //用于数据文件的读写，根据参考椭球带号查找相应椭球
        foreach (var it in EllipsoidList)
        {
            Ellipsoids.Add(it.Id, it);
        }
    }
}