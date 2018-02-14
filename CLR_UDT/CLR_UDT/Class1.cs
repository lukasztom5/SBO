using System;
using System.Data;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text;
using System.Globalization;
using Microsoft.SqlServer.Types;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.Native,
     IsByteOrdered = true, ValidationMethodName = "SprawdzPunkt")]
public struct Punkt : INullable
{
    private bool is_Null;
    private double _x;
    private double _y;

    public bool IsNull
    {
        get
        { return (is_Null); }
    }
    public static Punkt Null
    {
        get
        {
            Punkt pt = new Punkt();
            pt.is_Null = true;
            return pt;
        }
    }

    public override string ToString()
    {
        if (this.IsNull)
            return "NULL";
        else
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(_x);
            builder.Append(",");
            builder.Append(_y);
            return builder.ToString();
        }
    }
    [SqlMethod(OnNullCall = false)]
    public static Punkt Parse(SqlString s)
    {
        if (s.IsNull)
            return Null;
        CultureInfo ci = new CultureInfo("en-US");
        Punkt pt = new Punkt();
        string[] xy = s.Value.Split(",".ToCharArray());
        pt.X = Double.Parse(xy[0], ci);
        pt.Y = Double.Parse(xy[1], ci);

        if (!pt.SprawdzPunkt())
            throw new ArgumentException("Invalid XY coordinate values.");
        return pt;
    }
    // współrzędne X i Y są ustawiane jako właściwościtypu.
    public Double X
    {
        get
        { return this._x; }
        set
        {
            Double temp = _x;
            _x = value;
            if (!SprawdzPunkt())
            {
                _x = temp;
                throw new ArgumentException("Zła współrzędna X.");
            }
        }
    }
    public Double Y
    {
        get
        { return this._y; }
        set
        {
            Double temp = _y;
            _y = value;
            if (!SprawdzPunkt())
            {
                _y = temp;
                throw new ArgumentException("Zła współrzędna X.");
            }
        }
    }
    private bool SprawdzPunkt()
    {
     { return true; }
    }

    // Zbior Punktow od 0,0.
    [SqlMethod(OnNullCall = false)]
    public Double ZbiorPunktowDla(int n)
    { return ZbiorPunktow(0, 0, n); }

    // Zbior punktow od wskazanego punktu
    [SqlMethod(OnNullCall = false)]
    public Double ZbiorPunktowOd(Punkt pFrom, int n)
    { return ZbiorPunktow(pFrom.X, pFrom.Y, n); }

    // Zbior punktow
    [SqlMethod(OnNullCall = false)]
    public Double ZbiorPunktow(Double iX, Double iY, int n)
    {
       Double r2=0;
        for (int i = 1; i <= n + 1; i++)
        {
            r2 += Math.Pow(iX - _x, 2.0) + Math.Pow(iY - _y, 2.0);
        }

            return Math.Sqrt(r2);
    }
    //Objetosc hipersfery o srodku 0,0
    public Double ObjetoscOd(int n)
    { return Objetosc(0, 0, n); }
    //Objetosc hipersfery o srodku we wskazanym punkcie
    public Double ObjetoscDla(Punkt pFrom, int n)
    { return Objetosc(pFrom.X, pFrom.Y, n); }


    // Objetosc hipersfery
    [SqlMethod(OnNullCall = false)]
    public Double Objetosc(Double iX, Double iY, int n)
    {
        Double r2 = 0;
        Double c = 0;
        Double V = 0;
        for (int i = 1; i <= n + 1; i++)
        {
            r2 += Math.Pow(iX - _x, 2.0) + Math.Pow(iY - _y, 2.0);
        }

        Double r = Math.Sqrt(r2);

        if (n == 0)
        {
            c=1;
            V = c * Math.Pow(r, 0.0);

        }
        else if (n == 1)
        {
            c = 2;
            V = c * Math.Pow(r, 1.0);

        }
        else if (n == 2)
        {
            c = Math.PI;
            V = c * Math.Pow(r, 2.0);

        }
        else if (n == 3)
        {
            c = (4/3)*Math.PI;
            V = c * Math.Pow(r, 3.0);

        }
        else if (n == 4)
        {
            c = 0.5 * Math.Pow(Math.PI,2.0);
            V = c * Math.Pow(r, 4.0);

        }
        else if (n == 5)
        {
            c = (8/15) * Math.Pow(Math.PI, 2.0);
            V = c * Math.Pow(r, 5.0);

        }
        else if (n == 6)
        {
            c = (1 / 6) * Math.Pow(Math.PI, 3.0);
            V = c * Math.Pow(r, 6.0);

        }
        else if (n == 7)
        {
            c = (16 / 105) * Math.Pow(Math.PI, 3.0);
            V = c * Math.Pow(r, 7.0);

        }
        else if (n == 8)
        {
            c = (1 / 24) * Math.Pow(Math.PI, 4.0);
            V = c * Math.Pow(r, 8.0);

        }
        else if (n > 8 && n%2==0)
        {
            int result = 1;
            for (int i = 1; i <= n/2; i++)
            {
                result *= i;
            }
            c = Math.Pow(Math.PI,n/2)/result;
            V = c * Math.Pow(r, n);

        }
        else if (n > 8 && n % 2 != 0)
        {
            int result = 1;
            int result1 = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            for (int i = 1; i <= result; i++)
            {
                result1 *= i;
            }

            c = Math.Pow(2, (n - 1) / 2 + 1) * Math.Pow(Math.PI, (n - 1) / 2) / result1;
            V = c * Math.Pow(r, n);

        }
        return V;
    }
    //Powierzchnie hipersfery dla srodka 0,0
    public Double PoleOd(int n)
    { return Objetosc(0, 0, n); }
    //Powierzchnia hipersfery dla srodka we wskazanym punkcie
    public Double PoleDla(Punkt pFrom, int n)
    { return Objetosc(pFrom.X, pFrom.Y, n); }
    //Powierzchnia Hipersfery
    [SqlMethod(OnNullCall = false)]
    public Double Pole(Double iX, Double iY, int n)
    {
        Double r2 = 0;
        Double c = 0;
        Double P = 0;
        for (int i = 1; i <= n + 1; i++)
        {
            r2 += Math.Pow(iX - _x, 2.0) + Math.Pow(iY - _y, 2.0);
        }

        Double r = Math.Sqrt(r2);
        if (n == 0)
        {
            c = 0;
            P = c * Math.Pow(r, n - 1);

        }
        else if (n == 1)
        {
            c = 2;
            P = c * Math.Pow(r, n - 1);

        }
        else if (n == 2)
        {
            c = 2*Math.PI;
            P = c * Math.Pow(r, n - 1);

        }
        else if (n == 3)
        {
            c = 4 * Math.PI;
            P = c * Math.Pow(r, n - 1);

        }
        else if (n == 4)
        {
            c = 2 * Math.Pow(Math.PI,2);
            P = c * Math.Pow(r, n - 1);

        }
        else if (n == 5)
        {
            c = (8/3) * Math.Pow(Math.PI, 2);
            P = c * Math.Pow(r, n - 1);

        }
        else if (n == 6)
        {
            c = Math.Pow(Math.PI, 3);
            P = c * Math.Pow(r, n - 1);

        }
        else if (n == 7)
        {
            c = (16 / 15) * Math.Pow(Math.PI, 3);
            P = c * Math.Pow(r, n - 1);

        }
        else if (n == 8)
        {
            c = (1 / 3) * Math.Pow(Math.PI, 4);
            P = c * Math.Pow(r, n - 1);

        }
        else if (n > 8)
        {
            int result = 1;
            int result1 = 1;
            Double gamma = 0.0;
            if (n % 2 == 0)
            {
                for (int i = 1; i < n; i++)
                {
                    result *= i;
                }
                gamma = result;
            }
            else if (n % 2 !=0)
            {
                for (int i = 1; i < 2*n-1; i++)
                {
                    result *= i;
                }
                for (int i = 1; i <= result; i++)
                {
                    result1 *= i;
                }

                gamma=(result1/Math.Pow(2,n))*Math.Sqrt(Math.PI);
            }
            c = (2 * Math.Pow(Math.PI, (n / 2)) / gamma);
            P = c * Math.Pow(r, n - 1);
        }

        return P;
    }
    //Odleglosc pomiedzy srodkami hipersfer
    public Double Przecina(Punkt pFrom, Punkt pFrom1, Punkt pFrom2)
    { return Przeciecie(pFrom.X, pFrom.Y, pFrom1.X,pFrom1.Y,pFrom2.X,pFrom2.Y); }

    public Double Promien11(Punkt pFrom)
    { return Promien1(pFrom.X, pFrom.Y); }

    public Double Promien22(Punkt pFrom1, Punkt pFrom2)
    { return Promien2(pFrom1.X, pFrom1.Y, pFrom2.X, pFrom2.Y); }

    public Double Styk1(Punkt pFrom, Punkt pFrom1, Punkt pFrom2)
    { return Styk(pFrom.X, pFrom.Y, pFrom1.X, pFrom1.Y, pFrom2.X, pFrom2.Y); }
    //Odleglosc, gdy hipersfery sie stykaja
    [SqlMethod(OnNullCall = false)]
    public Double Styk(Double iX, Double iY, Double iX1, Double iY1, Double iX2, Double iY2)
    {
        Double r1 = Math.Sqrt(Math.Pow(iX - _x, 2.0) + Math.Pow(iY - _y, 2.0));
        Double r2 = Math.Sqrt(Math.Pow(iX2 - iX1, 2.0) + Math.Pow(iY2 - iY1, 2.0));
        Double w = r1 + r2;
        Double s = w;
        return s;
    }
    //Promien I Hipersfery
    public Double Promien1(Double iX, Double iY)
    {
        Double r1 = Math.Sqrt(Math.Pow(iX - _x, 2.0) + Math.Pow(iY - _y, 2.0));
        Double s = r1;
        return s;
    }
    //Promien II Hipersfery
    public Double Promien2(Double iX1, Double iY1, Double iX2, Double iY2)
    {
        Double r2 = Math.Sqrt(Math.Pow(iX2 - iX1, 2.0) + Math.Pow(iY2 - iY1, 2.0));
        Double s = r2;
        return s;
    }
    //Odleglosc miedzy srodkami
    [SqlMethod(OnNullCall = false)]
    public Double Przeciecie(Double iX, Double iY, Double iX1, Double iY1, Double iX2, Double iY2)
    {
        Double r1 = Math.Sqrt(Math.Pow(iX - _x, 2.0) + Math.Pow(iY - _y, 2.0));
        Double r2 = Math.Sqrt(Math.Pow(iX2 - iX1, 2.0) + Math.Pow(iY2 - iY1, 2.0));
        Double w = r1 + r2;
        Double s = 0 ;

        if (iX==iX2 && iY==iY2)
        {
            s = 0;
        }
        else if (iX != iX2 && iY != iY2)
        {

            Double w1 = Math.Sqrt(Math.Pow(iX-iX2, 2.0) + Math.Pow(iY-iY2, 2.0));

            if (w1 > w)
            {
                s = w1;
            }
            else if (w1 == w)
            {
                s = w;
            }
            else if (w1 < w && w1 > 0)
            {
                s = w1;
            }
        }
        return s;
    }
}
