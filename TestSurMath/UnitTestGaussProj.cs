using ZXY;

namespace UnitTestGaussProj;

public class UnitTestGaussProj
{
    [Fact]
    public void TestBLToXY()
    {
        {           
            IProj proj = new GaussProj(EllipsoidFactory.Ellipsoids["BJ54"]);

            double B = SurMath.DmsToRadian(21.58470845);
            double l = SurMath.DmsToRadian(2.25314880);
            var r = proj.BLtoXY(B, l);
            //B = 21 ◦ 58 ′ 47.0845 ′′ ,L = 113 ◦ 25 ′ 31.4880 ′′ ，
            //x = 2433586.692,y = 250547.403
            Assert.Equal(2433586.692, r.X, 1e-3);
            Assert.Equal(250547.403, r.Y, 1e-3);
        }

        {
            //B = 21 ◦ 58 ′ 47.0845 ′′ ,L = 113 ◦ 25 ′ 31.4880 ′′ ，
            //x = 2433586.692,y = 250547.403            
            IProj proj = new GaussProj(EllipsoidFactory.Ellipsoids["BJ54"]);

            double B = SurMath.DmsToRadian(21.58470845);
            double L = SurMath.DmsToRadian(113.25314880);
            double L0 = SurMath.DmsToRadian(111);
            var r = proj.BLtoXY(B, L, L0, 0, 0);

            Assert.Equal(2433586.692, r.X, 1e-3);
            Assert.Equal(250547.403, r.Y, 1e-3);
        }
    }

    [Fact]
    public void TestXYToBL()
    {
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
}
