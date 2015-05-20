using System;

namespace Cession.Dimensions
{
    public struct Length:IEquatable<Length>
    {
        public const double MillimetersPerPixel = 25;

        public const double PixelsPerMillimeter = 1 / MillimetersPerPixel;
        public const double PixelsPerCentimeter = 10 * PixelsPerMillimeter;
        public const double PixelsPerDecimeter = 100 * PixelsPerMillimeter;
        public const double PixelsPerMeter = 1000 * PixelsPerMillimeter;

        public const int InchesPerFoot = 12;
        public const double PixelsPerInch = 25.4 * PixelsPerMillimeter;
        public const  double PixelsPerFoot = InchesPerFoot * PixelsPerInch;
        public const  double PixelsPerFootDiv4 = PixelsPerFoot / 4;

        private double _value;
        private double _precision;

        public static double DefaultPrecision{ get; set; }

        public static string DefaultFormatSymbol{ get; set; }

        static Length ()
        {
            DefaultPrecision = PixelsPerInch;
            DefaultFormatSymbol = "fi";
        }

        public Length (double value) : this (value, DefaultPrecision)
        {

        }

        public Length (double value, double precision)
        {
            if (precision <= 0)
                throw new ArgumentOutOfRangeException ("precission");

            _value = value;
            _precision = precision;
        }

        public double Value
        {
            get{ return _value; }
            set{ _value = value; }
        }

        public double RoundedValue
        {
            get{ return Length.Round (_value, _precision); }
        }

        public static double Round (double length, double precision)
        {
            return Math.Round (length / precision) * precision;
        }

        public override string ToString ()
        {
            return ToString (DefaultFormatSymbol);
        }

        public string ToString (string format)
        {  
            if (string.IsNullOrEmpty (format))
                format = "fi";
            format = format.Trim ();

            switch (format)
            {
            case "fi":
                return ToStringImperial ();
            case "fif":
                return ToStringImperialFraction ();
            case "cm":
                return string.Format ("{0}cm", Centimeters.ToString ());
            case "mm":
                return string.Format ("{0}mm", Milimeters.ToString ());
            case "m":
                return string.Format ("{0}m", Meters.ToString ());
            default:
                throw new FormatException (string.Format ("The '{0}' format string is not supported.", format));
            }      
        }

        public static string ConvertToString(double value)
        {
            var length = new Length (value);
            return length.ToString ();
        }

        public static bool TryParse (string s, out Length length)
        {
            length = new Length ();
            return false;
        }

        public static Length operator + (Length left, Length right)
        {
            left._value = left.RoundedValue + right.RoundedValue;
            left._precision = Math.Min (left._precision, right._precision);
            return left;
        }

        public static Length operator + (Length left, double right)
        {
            left._value = left.RoundedValue + Length.Round (right, left._precision);
            return left;
        }

        public static Length operator + (double left, Length right)
        {
            right._value = Length.Round (left, right._precision) + right.RoundedValue;
            return right;
        }

        public static Length operator - (Length left, Length right)
        {
            left._value = left.RoundedValue - right.RoundedValue;
            left._precision = Math.Min (left._precision, right._precision);
            return left;
        }

        public static Length operator - (double left, Length right)
        {
            right._value = Length.Round (left, right._precision) - right.RoundedValue;
            return right;
        }

        public static Length operator - (Length left, double right)
        {
            left._value = left.RoundedValue - Length.Round (right, left._precision);
            return left;
        }

        public static bool operator == (Length left, Length right)
        {
            return left.Equals (right);
        }

        public static bool operator != (Length left, Length right)
        {
            return !left.Equals (right);
        }

        public static bool operator > (Length left, Length right)
        {
            return left.RoundedValue > right.RoundedValue;
        }

        public static bool operator < (Length left, Length right)
        {
            return left.RoundedValue < right.RoundedValue;
        }

        public static bool operator >= (Length left, Length right)
        {
            return left.RoundedValue >= right.RoundedValue;
        }

        public static bool operator <= (Length left, Length right)
        {
            return left.RoundedValue <= right.RoundedValue;
        }

        public bool Equals (Length length)
        {
            return RoundedValue == length.RoundedValue;
        }

        public override bool Equals (object obj)
        {
            if (null == obj || !(obj is Length))
                return false;
            return Equals ((Length)obj);
        }

        public override int GetHashCode ()
        {
            return RoundedValue.GetHashCode ();
        }

        public static Length FromFeetInches (int feet, int inches)
        {
            return new Length (feet * PixelsPerFoot + inches * Length.PixelsPerInch,Length.PixelsPerInch);
        }


        public static Length FromFeet (int feet)
        {
            return new Length (feet * Length.PixelsPerFoot, Length.PixelsPerInch);
        }

        public static Length FromInches (int inches)
        {
            return new Length (inches * Length.PixelsPerInch, Length.PixelsPerInch);
        }

        public static Length FromMeter (double meters)
        {
            return new Length (meters * Length.PixelsPerMeter, Length.PixelsPerCentimeter);
        }

        public static Length FromCentimeters (double centimeters)
        {
            return new Length (centimeters * Length.PixelsPerCentimeter, Length.PixelsPerCentimeter);
        }

        public double Feet
        {
            get{ return RoundedValue / Length.PixelsPerFoot; }
        }

        public double Inches
        {
            get{ return RoundedValue / Length.PixelsPerInch; }
        }

        public double Meters
        {
            get{ return RoundedValue / Length.PixelsPerMeter; }
        }

        public double Decimeters
        {
            get{ return RoundedValue / Length.PixelsPerDecimeter; }
        }

        public double Centimeters
        {
            get{ return RoundedValue / Length.PixelsPerCentimeter; }
        }

        public double Milimeters
        {
            get{ return RoundedValue / Length.PixelsPerMillimeter; }
        }

        private string ToStringImperial ()
        {
            int feet = (int)Feet;

            double n = Math.Ceiling (Math.Log10 (PixelsPerInch / _precision));
            string formatSymbol = "F" + n.ToString ();
            double inches = (Inches % InchesPerFoot);

            if (feet == 0)
                return string.Format ("{0}\"", inches.ToString (formatSymbol));
            return string.Format ("{0}'{1}\"", feet.ToString (), inches.ToString (formatSymbol));
        }

        private string ToStringImperialFraction ()
        {
            int feet = (int)Feet;
            int inches = (int)(Inches % InchesPerFoot);
            int denominator = (int)(Length.PixelsPerInch / _precision);
            int remainder = (int)(RoundedValue / _precision) % denominator;

            if (remainder == 0)
            {
                if (feet == 0)
                    return string.Format ("{0}\"", inches.ToString ());
                return string.Format ("{0}'{1}\"", feet.ToString (), inches.ToString ());
            }

            if (feet == 0)
                return string.Format ("{0}\" {1}/{2}", inches.ToString (),
                    remainder.ToString (),
                    denominator.ToString ());
            else
                return string.Format ("{0}'{1}\" {2}/{3}", feet.ToString (),
                    inches.ToString (),
                    remainder.ToString (),
                    denominator.ToString ());
        }
    }
}

