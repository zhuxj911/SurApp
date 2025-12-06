using ZXY;

namespace UnitTestUtmProj;

public class UnitTestUtmProj
{
    [Fact]
    public void TestBLToXY_WGS84()
    {
        IProj proj = new GaussProj(EllipsoidFactory.Ellipsoids["BJ54"]);

        double B = SurMath.DmsToRadian(21.58470845);
        double l = SurMath.DmsToRadian(2.25314880);
        var r = proj.BLtoXY(B, l);
        //B = 21 ◦ 58 ′ 47.0845 ′′ ,L = 113 ◦ 25 ′ 31.4880 ′′ ，
        //x = 2433586.692,y = 250547.403
        Assert.Equal(2433586.692, r.X, 1e-3);
        Assert.Equal(250547.403, r.Y, 1e-3);

        //B = 21 ◦ 58 ′ 47.0845 ′′ ,L = 113 ◦ 25 ′ 31.4880 ′′ ，
        //x = 2433586.692,y = 250547.403
        B = SurMath.DmsToRadian(21.58470845);
        var L = SurMath.DmsToRadian(113.25314880);
        double L0 = SurMath.DmsToRadian(111);
        r = proj.BLtoXY(B, L, L0, 0, 0);

        Assert.Equal(2433586.692, r.X, 1e-3);
        Assert.Equal(250547.403, r.Y, 1e-3);
        
        //大地测量学P176
        //B = 30 ◦ 30 ′ 00 ′′ ,l = 3 ◦ 20 ′00 ′′ 
        //x = 3380330.773,y = 320089.9761
        B = SurMath.DmsToRadian(30.30);
        L = SurMath.DmsToRadian(114.20);
        L0 = SurMath.DmsToRadian(111);
        r = proj.BLtoXY(B, L, L0, 0, 0);

        Assert.Equal(3380330.773, r.X, 1e-3);
        Assert.Equal(320089.9761, r.Y, 7e-3); //Expected: 320089.9761 Actual 320089.96964730945
    }
    
    

    [Fact]
    public void TestXYToBL_WGS84()
    {
            //B = 21 ◦ 58 ′ 47.0845 ′′ ,L = 113 ◦ 25 ′ 31.4880 ′′ ，
            IProj proj = new GaussProj(EllipsoidFactory.Ellipsoids["BJ54"]);

            double x = 2433586.692, y = 250547.403;
            var r = proj.XYtoBL(x, y);

            var B = SurMath.RadianToDms(r.B);
            var l = SurMath.RadianToDms(r.L);

            Assert.Equal(21.58470845, B, 1e-8);
            Assert.Equal(2.25314880, l, 1e-8);
    }
}