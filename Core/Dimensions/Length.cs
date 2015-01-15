namespace Cession.Dimensions
{
	using System;

	public struct Length:IEquatable<Length>
	{
		public const double LogicUnitPerMM = 1.0;
		public const double LogicUnitPerCM = 10 * LogicUnitPerMM;
		public const double LogicUnitPerDM = 100 * LogicUnitPerMM;
		public const double LogicUnitPerMeter = 1000 * LogicUnitPerMM;

		public const int InchesPerFoot = 12;
		public const double LogicUnitPerInch = 25.4 * LogicUnitPerMM;
		public const double LogicUnitPerFoot = InchesPerFoot * LogicUnitPerInch;
		public const double LogicUnitPerInchDiv4 = LogicUnitPerInch / 4;


		private double value;
		private double precision;

		public static double DefaultPrecision{ get; set; }
		public static string DefaultFormatSymbol{ get; set; }

		static Length()
		{
			DefaultPrecision = LogicUnitPerInch;
			DefaultFormatSymbol = "fi";
		}

		public Length (double value):this(value,DefaultPrecision)
		{

		}

		public Length(double value,double precision){
			if (precision <= 0)
				throw new ArgumentOutOfRangeException ("precission");

			this.value = value;
			this.precision = precision;
		}

		public double Value
		{
			get{return value;}
			set{this.value = value;}
		}

		public double RoundedValue
		{
			get{return Length.Round(value,precision);}
		}

		public static double Round(double length,double precision)
		{
			return Math.Round(length / precision) * precision;
		}

		public override string ToString ()
		{
			return ToString (DefaultFormatSymbol);
		}

		public string ToString(string format)
		{  
			if (String.IsNullOrEmpty(format)) format = "fi";
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
				return string.Format("{0}mm",Milimeters.ToString());
			case "m":
				return string.Format ("{0}m", Meters.ToString ());
			default:
				throw new FormatException(String.Format("The '{0}' format string is not supported.", format));
			}      
		}

		public static bool TryParse(string s,out Length length)
		{
			length = new Length ();
//			return new Length(GetConverter(Unit).ConvertFrom(s));
			return false;
		}

		public static Length operator + (Length left,Length right)
		{
			left.value = left.RoundedValue + right.RoundedValue;
			left.precision = Math.Min (left.precision, right.precision);
			return left;
		}

		public static Length operator + (Length left,double right)
		{
			left.value = left.RoundedValue + Length.Round(right,left.precision);
			return left;
		}

		public static Length operator + (double left,Length right)
		{
			right.value = Length.Round(left,right.precision) + right.RoundedValue;
			return right;
		}

		public static Length operator - (Length left,Length right)
		{
			left.value = left.RoundedValue - right.RoundedValue;
			left.precision = Math.Min (left.precision, right.precision);
			return left;
		}

		public static Length operator - (double left,Length right)
		{
			right.value = Length.Round(left,right.precision) - right.RoundedValue;
			return right;
		}

		public static Length operator - (Length left,double right)
		{
			left.value = left.RoundedValue - Length.Round(right,left.precision);
			return left;
		}

		public static bool operator == (Length left,Length right)
		{
			return left.Equals(right);
		}

		public static bool operator != (Length left,Length right)
		{
			return !left.Equals(right);
		}

		public static bool operator > (Length left,Length right)
		{
			return left.RoundedValue > right.RoundedValue;
		}

		public static bool operator < (Length left,Length right)
		{
			return left.RoundedValue < right.RoundedValue;
		}

		public static bool operator >= (Length left,Length right)
		{
			return left.RoundedValue >= right.RoundedValue;
		}

		public static bool operator <= (Length left,Length right)
		{
			return left.RoundedValue <= right.RoundedValue;
		}

		public bool Equals(Length length)
		{
			return RoundedValue == length.RoundedValue;
		}

		public override bool Equals (object obj)
		{
			if(null == obj || !(obj is Length))
				return false;
			return Equals((Length)obj);
		}

		public override int GetHashCode ()
		{
			return RoundedValue.GetHashCode();
		}

		public static Length FromFeetInches(int feet,int inches)
		{
			return new Length(feet * Length.LogicUnitPerFoot + inches * Length.LogicUnitPerInch,
				Length.LogicUnitPerInch);
		}

//		public static Length FromFeetInchesRemainder(int feet,int inches,double remainder)
//		{
//			return new Length(feet * Length.LogicUnitPerFoot + 
//								inches * Length.LogicUnitPerInch + 
//			                  remainder);
//		}

		public static Length FromFeet(int feet)
		{
			return new Length(feet * Length.LogicUnitPerFoot,Length.LogicUnitPerInch);
		}

		public static Length FromInches(int inches)
		{
			return new Length(inches * Length.LogicUnitPerInch,Length.LogicUnitPerInch);
		}

		public static Length FromMeter(double metres)
		{
			return new Length(metres * Length.LogicUnitPerMeter,Length.LogicUnitPerCM);
		}

		public static Length FromCentimeters(double centimeters)
		{
			return new Length(centimeters * Length.LogicUnitPerCM,Length.LogicUnitPerCM);
		}

//		public static Length FromMDmCm(int meters,int decimeters,int centimeters)
//		{
//			return Length.FromCentimeters(meters * 100 + decimeters * 10 + centimeters);
//		}

		public double Feet
		{
			get{return RoundedValue / Length.LogicUnitPerFoot;}
		}

		public double Inches
		{
			get{return RoundedValue / Length.LogicUnitPerInch;}
		}

		public double Meters
		{
			get{return RoundedValue / Length.LogicUnitPerMeter;}
		}

		public double Decimeters
		{
			get{return RoundedValue / Length.LogicUnitPerDM;}
		}

		public double Centimeters
		{
			get{return RoundedValue / Length.LogicUnitPerCM;}
		}

		public double Milimeters
		{
			get{return RoundedValue / Length.LogicUnitPerMM;}
		}

		private string ToStringImperial ()
		{
			int feet = (int)Feet;

			double n = Math.Ceiling(Math.Log10 (LogicUnitPerInch / precision));
			string formatSymbol = "F" + n.ToString ();
			double inches = (Inches % InchesPerFoot);
				
			if(feet == 0)
				return string.Format("{0}\"",inches.ToString(formatSymbol));
			return string.Format("{0}'{1}\"",feet.ToString(),inches.ToString(formatSymbol));
		}

		private string ToStringImperialFraction(){
			int feet = (int)Feet;
			int inches = (int)(Inches % InchesPerFoot);
			int denominator = (int)(Length.LogicUnitPerInch / precision);
			int remainder = (int)(RoundedValue / precision) % denominator;

			if (remainder == 0) {
				if(feet == 0)
					return string.Format("{0}\"",inches.ToString());
				return string.Format("{0}'{1}\"",feet.ToString(),inches.ToString());
			}

			if(feet == 0)
				return string.Format("{0}\" {1}/{2}",inches.ToString(),
					remainder.ToString(),
					denominator.ToString());
			else
				return string.Format("{0}'{1}\" {2}/{3}",feet.ToString(),
					inches.ToString(),
					remainder.ToString(),
					denominator.ToString());
		}
	}
}

