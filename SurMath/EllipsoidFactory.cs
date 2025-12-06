namespace ZXY;


public static class EllipsoidFactory
{
    public static List<Ellipsoid> EllipsoidList { get; } = [];
    
    //用于数据文件的读写，根据参考椭球带号查找相应椭球
    public static Dictionary<string, Ellipsoid> Ellipsoids { get; } = [];

    static EllipsoidFactory()
    {
        EllipsoidList.Add(
            new Ellipsoid(6378137, 298.257222101) { Id = EllipsoidType.CGCS2000, Name = "CGCS2000大地坐标系"});
        EllipsoidList.Add(
            new Ellipsoid(6378245, 298.3) { Id = EllipsoidType.BJ54, Name = "北京54坐标系" });
        EllipsoidList.Add(
            new Ellipsoid(6378140, 298.257) { Id = EllipsoidType.XA80, Name = "西安80坐标系" });
        EllipsoidList.Add(
            new Ellipsoid(6378137, 298.257223560){Id = EllipsoidType.WGS84, Name = "WGS84大地坐标系"});
        EllipsoidList.Add(
            new Ellipsoid(6378137, 298.257222101){ Id = EllipsoidType.CS00, Name = "自定义坐标系" });

        //用于数据文件的读写，根据参考椭球带号查找相应椭球
        foreach (var it in EllipsoidList)
        {
            Ellipsoids.Add(it.Id.ToString(), it);
        }
    }
}