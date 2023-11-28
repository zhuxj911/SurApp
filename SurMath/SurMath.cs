using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ZXY;

/// <summary>
/// 常用的测量函数库
/// </summary>
public static class SurMath
{
	public const double TWOPI = 2 * Math.PI;
	public const double TORADIAN = Math.PI / 180.0;
	public const double TODEGREE = 180.0 / Math.PI;
	public const double TOSECOND = 180 * 3600 / Math.PI;

	public static (int d, int m, double s) DMSToDMS(double dmsAngle)
	{
		dmsAngle *= 10000;
		int sAngle = (int)dmsAngle;
		int d = sAngle / 10000;
		int m = (sAngle - d * 10000) / 100;
		double s = dmsAngle - d * 10000 - m * 100;

		return (d, m, s);
	}

	public static double DMSToRadian(double dmsAngle)
	{
		var dms = DMSToDMS(dmsAngle);

		return (dms.d + dms.m / 60.0 + dms.s / 3600.0) * TORADIAN;
	}

	public static (int d, int m, double s) Radian2DMS(double radAngle)
	{
		double angle = radAngle * TOSECOND; //转换为秒
		int iAngle = (int)angle;
		int d = iAngle / 3600;
		iAngle = iAngle - d * 3600;
		int m = iAngle / 60;
		double s = angle - d * 3600 - m * 60; //注意：此处用没有精度损失的angle
		return (d, m, s);
	}

	public static double RadianToDMS(double radAngle)
	{
		var dms = Radian2DMS(radAngle);
		return dms.d + dms.m / 100.0 + dms.s / 10000.0;
	}


	//-10.423011 -> -10°42′30.110000000000582″
	public static string DMS2String(int d, int m, double s)
	{
		//0 0  -30    0 -30 30 
		int f = (d + m / 60.0 + s / 3600.0) >= 0 ? 1 : -1;
		string ff = (f == 1) ? "" : "-";

		if(Math.Abs(s) < 1e-10)
			return $"{ff}{f * d}°{f * m:00}′00″";
		else
			return $"{ff}{f * d}°{f * m:00}′{f * s:00.#####}″";
	}

	public static string DmsToString(double dmsAngle)
	{
		var dms = DMSToDMS(dmsAngle);
		return DMS2String(dms.d, dms.m, dms.s);
	}

	public static string RadianToString(double radAngle)
	{
		var dms = Radian2DMS(radAngle);
		return DMS2String(dms.d, dms.m, dms.s);
	}

	public static (double a, double d) Azimuth(double xA, double yA, double xB, double yB)
	{
		(double a, double d) ad;
		double dx = xB - xA;
		double dy = yB - yA;
		ad.a = Math.Atan2(dy, dx) + ((dy < 0) ? 1 : 0) * TWOPI;
		ad.d = Math.Sqrt(dx * dx + dy * dy);
		return ad;
	}
}
