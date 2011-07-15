

using System;
using System.Collections.Generic;
using System.Text;



namespace Businfo.Globe
{
    class CoordTrans
    {


        const double _EPSONAN = 1.0e-12;
const 	double EPSILON	=	1.0e-12;
const  double PI = 3.14159265358979323846;

const   double WGSF	=	1/298.257223563;
const   double WGSe2	=	WGSF*(2-WGSF);
const 	 double WGSa	=	6378137.0000000000;

const   double BJ54F	=	1/298.3;
const   double BJ54e2	=	BJ54F*(2-BJ54F);
const double BJ54a = 6378245.0000000000;

  struct COORDINATECARTESIAN 
{
      public double dX;
      public double dY;
      public double dZ;
} ;

 struct COORDINATEGEODETIC 
{
     public double dLatitude;
     public double dLongtitude;
     public double dHeight;
} ;

 struct COORDINATEPLANE 
{
     public double dX;
     public double dY;
     public double dh;
} ;


 public struct SEVENPARAMETER
{
     public double dx;
     public double dy;
     public double dz;
     public double ex;
     public double ey;
     public double ez;
     public double m;
} ;

 struct GUASS
{
     public double x;
     public double y;
     public double h;
} ;




      // CoordTrans();
      public CoordTrans(double dx,double dy,double dz,double ex,double ey,double ez,double m,double Noofblet,int belt)
      {
          m_Para7.dx = dx;
          m_Para7.dy = dy;
          m_Para7.dz = dz;
          m_Para7.ex = ex;
          m_Para7.ey = ey;
          m_Para7.ez = ez;
          m_Para7.m = m;
          m_rNoofBelt = Noofblet;//分度数
          m_nBelt = belt;//带号
      }
      //~CoordTrans();
 public static SEVENPARAMETER m_Para7;
 public static int m_nBelt;
 public static double m_rNoofBelt;


	  	  
//Attribute

     private static   void BLHtoWGSXYZ(COORDINATEGEODETIC gdt,ref COORDINATECARTESIAN  car)
    {
        double N;
        N = WGSa / Math.Sqrt(1 - WGSe2 * Math.Sin(gdt.dLatitude) * Math.Sin(gdt.dLatitude)); // WGSe2 = e的平方
        car.dX = (N + gdt.dHeight) * Math.Cos(gdt.dLatitude) * Math.Cos(gdt.dLongtitude);
        car.dY = (N + gdt.dHeight) * Math.Cos(gdt.dLatitude) * Math.Sin(gdt.dLongtitude);
        car.dZ = (N * (1 - WGSe2) + gdt.dHeight) * Math.Sin(gdt.dLatitude);
    }
     private static void WGSXYZtoBJxyz(COORDINATECARTESIAN XYZ_84, SEVENPARAMETER m_para, ref COORDINATECARTESIAN xyz_54)
    {
        xyz_54.dX = XYZ_84.dX + m_Para7.dx - XYZ_84.dZ * ((m_Para7.ey / 3600.0) * PI / 180.0) + XYZ_84.dY * ((m_Para7.ez / 3600.0) * PI / 180.0) + XYZ_84.dX * m_Para7.m * 0.000001;
        xyz_54.dY = XYZ_84.dY + m_Para7.dy + XYZ_84.dZ * ((m_Para7.ex / 3600.0) * PI / 180.0) - XYZ_84.dX * ((m_Para7.ez / 3600.0) * PI / 180.0) + XYZ_84.dY * m_Para7.m * 0.000001;
        xyz_54.dZ = XYZ_84.dZ + m_Para7.dz - XYZ_84.dY * ((m_Para7.ex / 3600.0) * PI / 180.0) + XYZ_84.dX * ((m_Para7.ey / 3600.0) * PI / 180.0) + XYZ_84.dZ * m_Para7.m * 0.000001;	
	
    }
      private static void BJxyztoblh(COORDINATECARTESIAN xyz_54, ref COORDINATEGEODETIC blh_54)
    {
        COORDINATEGEODETIC Geoold;

        double m;
        double N;

        m = Math.Sqrt(Math.Pow(xyz_54.dY, 2) + Math.Pow(xyz_54.dX, 2));

        blh_54.dLongtitude =Math.Atan2(xyz_54.dY, xyz_54.dX);

        blh_54.dLatitude = Math.Atan2(xyz_54.dZ, m);
        blh_54.dHeight = 0.0;


        do
        {
            N = BJ54a / Math.Sqrt(1 - BJ54e2 * Math.Sin(blh_54.dLatitude) * Math.Sin(blh_54.dLatitude));
            Geoold.dLatitude = blh_54.dLatitude;
            Geoold.dHeight = blh_54.dHeight;

            blh_54.dLatitude = Math.Atan2(xyz_54.dZ * (N + blh_54.dHeight), m * (N * (1 - BJ54e2) + blh_54.dHeight));
            blh_54.dHeight = xyz_54.dZ / Math.Sin(blh_54.dLatitude) - N * (1 - BJ54e2);

        } while (Math.Abs(blh_54.dHeight - Geoold.dHeight) > _EPSONAN && Math.Abs(blh_54.dLatitude - Geoold.dLatitude) > _EPSONAN);
    }

      private static void BJblhtoGuass(COORDINATEGEODETIC geo_54, int Belt, double NoofBlet, ref GUASS xy_guass)
    {
        double l;
        double a0;
        double a3;
        double a4;
        double a5;
        double a6;
        double N;
        double B;
        double L;

        B = geo_54.dLatitude;
        L = geo_54.dLongtitude;

        if (Belt == 3)//3度带
            l = L - (NoofBlet * PI / 180.0);
        else
            return;

        N = 6399698.902 - (21562.267 - (108.973 - 0.612 * Math.Pow(Math.Cos(B), 2)) * Math.Pow(Math.Cos(B), 2)) * Math.Pow(Math.Cos(B), 2);
        a0 = 32140.404 - (135.3302 - (0.7092 - 0.0040 * Math.Pow(Math.Cos(B), 2)) * Math.Pow(Math.Cos(B), 2)) * Math.Pow(Math.Cos(B), 2);
        a4 = (0.25 + 0.00252 * Math.Pow(Math.Cos(B), 2)) * Math.Pow(Math.Cos(B), 2) - 0.04166;
        a6 = (0.166 * Math.Pow(Math.Cos(B), 2) - 0.084) * Math.Pow(Math.Cos(B), 2);
        a3 = (0.3333333 + 0.001123 * Math.Pow(Math.Cos(B), 2)) * Math.Pow(Math.Cos(B), 2) - 0.1666667;
        a5 = 0.0083 - (0.1667 - (0.1968 + 0.0040 * Math.Pow(Math.Cos(B), 2)) * Math.Pow(Math.Cos(B), 2)) * Math.Pow(Math.Cos(B), 2);

        xy_guass.x = 6367558.4969 * B - (a0 - (0.5 + (a4 + a6 * Math.Pow(l, 2)) * Math.Pow(l, 2)) * Math.Pow(l, 2) * N) * Math.Sin(B) * Math.Cos(B);

        xy_guass.y = (1 + (a3 + a5 * Math.Pow(l, 2)) * Math.Pow(l, 2)) * l * N * Math.Cos(B);

        xy_guass.y = 500000 + xy_guass.y;

        xy_guass.h = geo_54.dHeight;
    }

      private static void GaussToBJblh(GUASS xy_guass, int Belt, double NoofBelt, ref COORDINATEGEODETIC geo_54)
    {
        	int ProjNo; //带号
	double longitude0, X0,Y0, xval,yval;
	double e1,e2,f,a, ee, NN, T,C, M, D,R,u,fai;
	a = 6378245.0; f = 1.0/298.3; //54年北京坐标系参数
	//a=6378140.0; f=1/298.257; //80年西安坐标系参数
	
	if(Belt == 3)//3度带
	   ProjNo = (int)(xy_guass.x/1000000) ; //查找带号
	else
		 return;	

	longitude0 = m_rNoofBelt*PI/180.0;//中央经线

	X0 = ProjNo*1000000+500000;
	Y0 = 0;
	xval = xy_guass.x-X0; yval = xy_guass.y-Y0; //带内大地坐标
	e2 = 2*f-f*f;
	e1 = (1.0-Math.Sqrt(1-e2))/(1.0+Math.Sqrt(1-e2));
	ee = e2/(1-e2);
	M = yval;
	u = M/(a*(1-e2/4-3*e2*e2/64-5*e2*e2*e2/256));
	fai = u+(3*e1/2-27*e1*e1*e1/32)*Math.Sin(2*u)+(21*e1*e1/16-55*e1*e1*e1*e1/32)*Math.Sin(4*u)
		+(151*e1*e1*e1/96)*Math.Sin(6*u)+(1097*e1*e1*e1*e1/512)*Math.Sin(8*u);
	C = ee*Math.Cos(fai)*Math.Cos(fai);
	T = Math.Tan(fai)*Math.Tan(fai);
	NN = a/Math.Sqrt(1.0-e2*Math.Sin(fai)*Math.Sin(fai));
	R = a*(1-e2)/Math.Sqrt((1-e2*Math.Sin(fai)*Math.Sin(fai))*(1-e2*Math.Sin(fai)*Math.Sin(fai))*(1-e2*Math.Sin(fai)*Math.Sin(fai)));
	D = xval/NN;
	//计算经度(Longitude) 纬度(Latitude)
	geo_54.dLongtitude = longitude0+(D-(1+2*T+C)*D*D*D/6+(5-2*C+28*T-3*C*C+8*ee+24*T*T)*D*D*D*D*D/120)/Math.Cos(fai);
	geo_54.dLatitude = fai -(NN*Math.Tan(fai)/R)*(D*D/2-(5+3*T+10*C-4*C*C-9*ee)*D*D*D*D/24+(61+90*T+298*C+45*T*T-256*ee-3*C*C)*D*D*D*D*D*D/720);
	geo_54.dHeight = xy_guass.h;
	//转换为十进制度
// 	output[0] = longitude1 / iPI;
// 	output[1] = latitude1 / iPI;
    }
      private static void blhtoBJxyz(COORDINATEGEODETIC blh_54, ref COORDINATECARTESIAN xyz_54)
    {
        	double	N;
	N	=	BJ54a / Math.Sqrt(1 - BJ54e2 * Math.Sin(blh_54.dLatitude) * Math.Sin(blh_54.dLatitude)); // WGSe2 = e的平方
	xyz_54.dX	=	(N + blh_54.dHeight) * Math.Cos(blh_54.dLatitude) * Math.Cos(blh_54.dLongtitude);
	xyz_54.dY	=	(N + blh_54.dHeight) * Math.Cos(blh_54.dLatitude) * Math.Sin(blh_54.dLongtitude);
	xyz_54.dZ   = 	(N * (1 - BJ54e2) + blh_54.dHeight) * Math.Sin(blh_54.dLatitude);
    }
      private static void BJxyztoWGSXYZ(COORDINATECARTESIAN xyz_54, SEVENPARAMETER m_para, ref COORDINATECARTESIAN XYZ_84)
    {
        	XYZ_84.dX = xyz_54.dX + m_Para7.dx - xyz_54.dZ*((m_Para7.ey/3600.0)*PI/180.0) + xyz_54.dY*((m_Para7.ez/3600.0)*PI/180.0) + xyz_54.dX*m_Para7.m*0.000001;
	XYZ_84.dY = xyz_54.dY + m_Para7.dy + xyz_54.dZ*((m_Para7.ex/3600.0)*PI/180.0) - xyz_54.dX*((m_Para7.ez/3600.0)*PI/180.0) + xyz_54.dY*m_Para7.m*0.000001;
	XYZ_84.dZ = xyz_54.dZ + m_Para7.dz - xyz_54.dY*((m_Para7.ex/3600.0)*PI/180.0) + xyz_54.dX*((m_Para7.ey/3600.0)*PI/180.0) + xyz_54.dZ*m_Para7.m*0.000001;	
    }
      private static void WGSXYZtoBLH(COORDINATECARTESIAN XYZ_84, ref COORDINATEGEODETIC gdt)
    {
          double  m;
	   double  N;
	   COORDINATEGEODETIC Geoold;

       m = Math.Sqrt(Math.Pow(XYZ_84.dY, 2) + Math.Pow(XYZ_84.dX, 2));
       gdt.dLongtitude = Math.Atan2(XYZ_84.dY, XYZ_84.dX);
       gdt.dLatitude = Math.Atan2(XYZ_84.dZ, m);
	   gdt.dHeight = 0.0;
	   
	   do {
		   N	= WGSa / Math.Sqrt (1-WGSe2*Math.Sin(gdt.dLatitude)*Math.Sin(gdt.dLatitude));
		   Geoold.dLatitude	= gdt.dLatitude;
		   Geoold.dHeight	= gdt.dHeight;
           gdt.dLatitude = Math.Atan2(XYZ_84.dZ * (N + gdt.dHeight), m * (N * (1 - WGSe2) + gdt.dHeight));
           gdt.dHeight = XYZ_84.dZ / Math.Sin(gdt.dLatitude) - N * (1 - WGSe2);
       } while (Math.Abs(gdt.dHeight - Geoold.dHeight) > _EPSONAN && Math.Abs(gdt.dLatitude - Geoold.dLatitude) > _EPSONAN);
    }

  /// <summary>
  /// 十进制经纬度到大地坐标
  /// </summary>
  /// <param name="rLatitude"></param>
  /// <param name="rLongitude"></param>
  /// <param name="rHigh"></param>
  /// <param name="rX_84"></param>
  /// <param name="rY_84"></param>
  /// <param name="rZ_84"></param>
    public void BLHto84XYZ(double rLatitude,double rLongitude,double rHigh,ref double rX_84,ref double rY_84,ref double rZ_84)
    {
        	COORDINATEGEODETIC F_84blh;
	COORDINATECARTESIAN F_84xyz = new COORDINATECARTESIAN();
    COORDINATECARTESIAN F_54xyz = new COORDINATECARTESIAN();
	COORDINATEGEODETIC   F_54blh = new COORDINATEGEODETIC() ;
	GUASS                F_guass = new GUASS();
	F_84blh.dHeight = Decimal_Radian(rHigh);
	F_84blh.dLatitude = Decimal_Radian(rLatitude);
	F_84blh.dLongtitude = Decimal_Radian(rLongitude);
	BLHtoWGSXYZ(F_84blh,ref F_84xyz);
    WGSXYZtoBJxyz(F_84xyz, m_Para7, ref  F_54xyz);
    BJxyztoblh(F_54xyz, ref F_54blh);
    BJblhtoGuass(F_54blh, m_nBelt, m_rNoofBelt, ref F_guass);
	rX_84 = F_guass.x;
	rY_84 = F_guass.y;
	rZ_84 = F_guass.h;
    }
      /// <summary>
      /// 大地坐标到十进制经纬度
      /// </summary>
      /// <param name="rX_84"></param>
      /// <param name="rY_84"></param>
      /// <param name="rZ_84"></param>
      /// <param name="rLatitude"></param>
      /// <param name="rLongitude"></param>
      /// <param name="rHigh"></param>
    public void XYZ84toBLH(double rX_84, double rY_84, double rZ_84, ref double rLatitude, ref double rLongitude, ref double rHigh)
    {
        COORDINATEGEODETIC F_84blh = new COORDINATEGEODETIC();
        COORDINATECARTESIAN F_84xyz = new COORDINATECARTESIAN() ;
        COORDINATECARTESIAN F_54xyz = new COORDINATECARTESIAN() ;
        COORDINATEGEODETIC F_54blh = new COORDINATEGEODETIC() ;
        GUASS F_guass = new GUASS() ;

        F_guass.x = rX_84;
        F_guass.y = rY_84;
        F_guass.h = rZ_84;

        GaussToBJblh(F_guass, m_nBelt, m_rNoofBelt, ref  F_54blh);
        blhtoBJxyz(F_54blh, ref  F_54xyz);
        BJxyztoWGSXYZ(F_54xyz, m_Para7, ref  F_84xyz);
        WGSXYZtoBLH(F_84xyz, ref  F_84blh);
        rLatitude = F_84blh.dLatitude * 180 / PI;
        rLongitude = F_84blh.dLongtitude * 180 / PI;
        rHigh = F_84blh.dHeight;
    }
    public static double Degree_Decimal(string degree)
      {
          int d, m, s;
          String[] d_m_s = degree.Split('.');
          d = int.Parse(d_m_s[0]);
          m = int.Parse(d_m_s[1]);
          s = int.Parse(d_m_s[2]);
          return (d + m / 60.0 + s / 3600.0);
      }
    public static double Decimal_Radian(double degree)
      {
          return degree * PI / 180.0;
      }
    public static double Radian_Decimal(double degree)
      {
          return degree * 180 / PI;
      }
    public static string Decimal_Degree(double degree)
      {
          int d, m, s;
          string strTemp;

          d = (int)Math.Floor(degree);
          m = (int)((degree - d) * 3600 / 60);
          s = (int)((degree - d) * 3600 - m * 60);
          strTemp = string.Format("%d.%d.%d", d, m, s);
          return strTemp;
      }

    }
}
