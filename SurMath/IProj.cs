namespace ZXY;

public interface IProj
{
    /// <summary>
    /// 根据经纬度计算XY坐标（Y带加常数）
    /// </summary>
    /// <param name="B">纬度，单位：弧度</param>
    /// <param name="L">经度，单位：弧度</param>
    /// <param name="L0">中央子午线经度，单位：弧度</param>
    /// <param name="YKM">Y坐标加常数，单位：km， 一般为500km</param>
    /// <param name="N0">Y坐标前的带号</param>
    /// <returns>X:North坐标，单位：m,  Y: East坐标，单位：m, gamma:子午线收敛角，单位：弧度， m:长度比</returns>
    (double X, double Y, double gamma, double m) BLtoXY(double B, double L, double L0=0.0, double YKM=0.0, double N0=0.0);


    /// <summary>
    /// 根据XY坐标（Y带加常数）计算经纬度
    /// </summary>
    /// <param name="X">X:North坐标，单位：m</param>
    /// <param name="Y">Y: East坐标，单位：m</param>
    /// <param name="L0">中央子午线经度，单位：弧度</param>
    /// <param name="YKM">Y坐标加常数，单位：km， 一般为500km</param>
    /// <param name="N0">Y坐标前的带号</param>
    /// <returns>B:纬度，单位：弧度,  L: 经度，单位：弧度, gamma:子午线收敛角，单位：弧度， m:长度比</returns>
    (double B, double L, double gamma, double m) XYtoBL(double X, double Y, double L0=0.0, double YKM = 0.0, double N0 = 0.0);
}