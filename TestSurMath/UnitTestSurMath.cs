using ZXY;

namespace TestSurMath
{
	[TestClass]
	public class UnitTestSurMath
	{
		[TestMethod]
		public void TestDMSToDMS()
		{
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

			{
				var dms = SurMath.DMSToDMS(10.423011);
				Assert.AreEqual(10, dms.d);
				Assert.AreEqual(42, dms.m);
				Assert.AreEqual(30.11, dms.s, 1e-5);

				dms = SurMath.DMSToDMS(10.423011223344);
				Assert.AreEqual(10, dms.d);
				Assert.AreEqual(42, dms.m);
				Assert.AreEqual(30.11223344, dms.s, 1e-10);


				dms = SurMath.DMSToDMS(1.4000);
				Assert.AreEqual(1, dms.d);
				Assert.AreEqual(40, dms.m);
				Assert.AreEqual(0, dms.s, 1e-5);
			}
			{
				var dms = SurMath.DMSToDMS(-10.423011);
				Assert.AreEqual(-10, dms.d);
				Assert.AreEqual(-42, dms.m);
				Assert.AreEqual(-30.11, dms.s, 1e-5);

				dms = SurMath.DMSToDMS(-10.423011223344);
				Assert.AreEqual(-10, dms.d);
				Assert.AreEqual(-42, dms.m);
				Assert.AreEqual(-30.11223344, dms.s, 1e-10);


				dms = SurMath.DMSToDMS(-1.4000);
				Assert.AreEqual(-1, dms.d);
				Assert.AreEqual(-40, dms.m);
				Assert.AreEqual(0, dms.s, 1e-5);
			}
		}

		[TestMethod]
		public void TestDmsToRadian()
		{
			double rad = SurMath.DMSToRadian(101.03201);
			Assert.AreEqual(1.763752656690170, rad, 1e-14);
			rad = SurMath.DMSToRadian(-101.03201);
			Assert.AreEqual(-1.763752656690170, rad, 1e-14);

			rad = SurMath.DMSToRadian(1.4001);
			Assert.AreEqual(0.0290936690033833000, rad, 1e-14);

			rad = SurMath.DMSToRadian(1.4000);
			Assert.AreEqual(0.0290888208665722, rad, 1e-14);
			rad = SurMath.DMSToRadian(-1.4000);
			Assert.AreEqual(-0.0290888208665722, rad, 1e-14);
		}


		[TestMethod]
		public void TestRadianToDMS()
		{
			{
				var dms = SurMath.Radian2DMS(0.0290888208665722);
				Assert.AreEqual(1, dms.d);
				Assert.AreEqual(40, dms.m);
				Assert.AreEqual(0, dms.s, 1e-5);
			}

			{
				var dms = SurMath.Radian2DMS(0.186896207362775000);
				Assert.AreEqual(10, dms.d);
				Assert.AreEqual(42, dms.m);
				Assert.AreEqual(30.11, dms.s, 1e-5);
			}

			{
				var dms = SurMath.Radian2DMS(-0.0290888208665722);
				Assert.AreEqual(-1, dms.d);
				Assert.AreEqual(-40, dms.m);
				Assert.AreEqual(0, dms.s, 1e-5);
			}

			{
				var dms = SurMath.Radian2DMS(-0.186896207362775000);
				Assert.AreEqual(-10, dms.d);
				Assert.AreEqual(-42, dms.m);
				Assert.AreEqual(-30.11, dms.s, 1e-5);
			}
		}


		[TestMethod]
		public void TestDMStoString()
		{
			var str = SurMath.DmsToString(101.03201);
			Assert.AreEqual("101¡ã03¡ä20.1¡å", str);

			str = SurMath.DmsToString(-101.03201);
			Assert.AreEqual("-101¡ã03¡ä20.1¡å", str);

			str = SurMath.DmsToString(0.4001);
			Assert.AreEqual("0¡ã40¡ä01¡å", str);

			str = SurMath.DmsToString(-0.4001);
			Assert.AreEqual("-0¡ã40¡ä01¡å", str);

			str = SurMath.DmsToString(1.4001);
			Assert.AreEqual("1¡ã40¡ä01¡å", str);

			str = SurMath.DmsToString(1.4000);
			Assert.AreEqual("1¡ã40¡ä00¡å", str);

			str = SurMath.DmsToString(-1.4000);
			Assert.AreEqual("-1¡ã40¡ä00¡å", str);
		}


		[TestMethod]
		public void TestRadtoString()
		{
			string dms = SurMath.RadianToString(0.0234165007975906);
			Assert.AreEqual<string>("1¡ã20¡ä30¡å", dms);

			dms = SurMath.RadianToString(-0.0234165007975906);
			Assert.AreEqual<string>("-1¡ã20¡ä30¡å", dms);

			dms = SurMath.RadianToString(0.02908882086657220);
			Assert.AreEqual<string>("1¡ã40¡ä00¡å", dms);

			dms = SurMath.RadianToString(-0.02908882086657220);
			Assert.AreEqual<string>("-1¡ã40¡ä00¡å", dms);

			dms = SurMath.RadianToString(0.02908930568025330);
			Assert.AreEqual<string>("1¡ã40¡ä00.1¡å", dms);

			dms = SurMath.RadianToString(-0.02908930568025330);
			Assert.AreEqual<string>("-1¡ã40¡ä00.1¡å", dms);

			dms = SurMath.RadianToString(0.69580806988502100); //39 52 0.71672
			Assert.AreEqual<string>("39¡ã52¡ä00.71672¡å", dms);

			dms = SurMath.RadianToString(-0.69580806988502100); //-39 52 0.71672
			Assert.AreEqual<string>("-39¡ã52¡ä00.71672¡å", dms);
		}


		[TestMethod]
		public void TestAzimuth()
		{
			var a = SurMath.Azimuth(0, 0, 1, 1);
			Assert.AreEqual(45, SurMath.RadianToDMS(a.a), 1e-10);

			double xA = 50342.464;
			double yA = 3423.232;
			double xB = 50289.874;
			double yB = 3528.978;
			var az = SurMath.Azimuth(xA, yA, xB, yB);
			Assert.AreEqual<string>("116¡ã26¡ä32.10298¡å", SurMath.RadianToString(az.a));

			xA = 50289.874;
			yA = 3528.978;
			xB = 50342.464;
			yB = 3423.232;
			az = SurMath.Azimuth(xA, yA, xB, yB);
			Assert.AreEqual<string>("296¡ã26¡ä32.10298¡å", SurMath.RadianToString(az.a));

			xA = 50342.464;
			yA = 3423.232;
			xB = 50389.874;
			yB = 3528.978;
			az = SurMath.Azimuth(xA, yA, xB, yB);
			Assert.AreEqual<string>("65¡ã51¡ä05.29596¡å", SurMath.RadianToString(az.a));

			xA = 50389.874;
			yA = 3528.978;
			xB = 50342.464;
			yB = 3423.232;
			az = SurMath.Azimuth(xA, yA, xB, yB);
			Assert.AreEqual<string>("245¡ã51¡ä05.29596¡å", SurMath.RadianToString(az.a));
		}
	}
}