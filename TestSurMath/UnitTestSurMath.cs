using ZXY;

namespace TestSurMath
{
	[TestClass]
	public class UnitTestSurMath
	{
		[TestMethod]
		public void TestDMSToDMS()
		{
			var dms = SurMath.DMSToDMS(123.3253012312);
			Assert.AreEqual(123, dms.d);
			Assert.AreEqual(32, dms.m);
			Assert.AreEqual(53.012312, dms.s, 1e-6);

			dms = SurMath.DMSToDMS(1.4);
			Assert.AreEqual(1, dms.d);
			Assert.AreEqual(40, dms.m);
			Assert.AreEqual(0, dms.s, 1e-6);
		}

		[TestMethod]
		public void TestAzimuth()
		{
			var a = SurMath.Azimuth(0, 0, 1, 1);
			Assert.AreEqual(45, SurMath.RadianToDMS(a.a), 1e-10);
		}
	}
}