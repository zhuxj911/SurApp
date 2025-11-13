namespace ZXY;

public class UtmProj : IProj
{
    private GaussProj gaussProj;
    private double k = 0.9996;

    public UtmProj(Ellipsoid ellipsoid)
    {
        gaussProj = new GaussProj(ellipsoid);
    }

    private (double x, double y, double gamma, double m) BlToXy(double B, double l)
    {
        var r =gaussProj.BLtoXY(B, l);
        double x = k * r.X;
        double y = k * r.Y;
        double gamma = k * r.gamma;
        double m = k * r.m;

        return (x, y, gamma, m);
    }

    public (double X, double Y, double gamma, double m) BLtoXY(double B, double L, double L0 = 0.0, double YKM = 0.0, double N0 = 0.0)
    {
        double l = L - L0;
        var r = BlToXy(B, l);
        return (r.x, r.y + YKM * 1000 + N0 * 1e6, r.gamma, r.m);
    }

    private (double B, double l, double gamma, double m) XyToBl(double x, double y)
    {
        x = x / k;
        y = y / k;

        var r = gaussProj.XYtoBL(x, y);

        double gamma = k * r.gamma;
        double m = k * r.m;
        return (r.B, r.L, gamma, m);
    }

    public (double B, double L, double gamma, double m) XYtoBL(double X, double Y, double L0 = 0.0, double YKM = 0.0, double N0 = 0.0)
    {
        double y = Y - N0 * 1e6 - YKM * 1000;
        var r = XyToBl(X, y);
        double L = r.l + L0;
        return (r.B, L, r.gamma, r.m);
    }
}
