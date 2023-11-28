using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZXY;

public class Point
{
	private string name = "";
	private string code = "";
	private double x = 0.0;
	private double y = 0.0;
	private double z = 0.0;

	public Point(string name, double x, double y, double z)
	{
		this.name = name;
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public Point(string name, double x, double y)
	{
		this.name = name;
		this.x = x;
		this.y = y;
	}


	public Point(double x, double y)
	{
		this.x = x;
		this.y = y;
	}

	public string Name
	{
		get { return name; }
		set { name = value; }
	}

	public string Code
	{
		get { return code; }
		set { code = value; }
	}

	public double X
	{
		get => x;
		set => x = value;
	}

	public double Y
	{
		get { return y; }
		set { y = value; }
	}

	public double Z
	{
		get => z;
		set
		{
			z = value;
		}
	}

	public override string ToString()
	{
		return $"name={name}, code={code},x={x}, y={y}, z={z}";
	}

	public double Azimuth(Point other)
	{
		return SurMath.Azimuth(this.X, this.Y, other.X, other.Y).a;
	}

	// distance of this  and other 
	public double Distance(Point other)
	{
		return SurMath.Azimuth(this.X, this.Y, other.X, other.Y).d;
	}

	/// <summary> 
	/// 静态的 整体的角度 类名 Point 与 实例的 个体的角度 this
	/// </summary>
	/// <param name="p1"></param>
	/// <param name="p2"></param>
	/// <returns></returns>
	public static double Distance(Point p1, Point p2)
	{
		//static 函数中不能使用this			
		return p1.Distance(p2);
	}


	public static double Azimuth(Point p1, Point p2)
	{
		//static 函数中不能使用this			
		return p1.Azimuth(p2);
	}
}
