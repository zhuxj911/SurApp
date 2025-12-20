using ZXY;

namespace SurApp.Models;

public class GPoint : IPoint {
    public string Name { get; set; } = "";
    public double X { get; set; }
    public double Y { get; set; }

    public double DmsB { get; set; }
    public double DmsL { get; set; }
	
    public double Gamma { get; set; }
    public String GammaString => ZXY.SurMath.RadianToDmsString(Gamma);
	
    public double M { get; set; }

    public override string ToString() => $"{Name}, {X}, {Y}, {DmsB}, {DmsL}, {Gamma}, {M}";
}
