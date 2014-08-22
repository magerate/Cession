
namespace Cession.Dimensions
{
	using System;

	public class PrecisionAttribute:Attribute{
		public double Precission{get;private set;}
		public PrecisionAttribute(double precission){
			this.Precission = precission;
		}
	}

	public class FormatSymbolAttribute:Attribute{
		public string FormatSymbol{ get; private set;}

		public FormatSymbolAttribute(string formatSymbol){
			this.FormatSymbol = formatSymbol;
		}
	}


	public enum Units
	{
		[Precision(Length.LogicUnitPerFoot),FormatSymbol("fi")]
		FeetInches = 0,

		[Precision(Length.LogicUnitPerInchDiv4),FormatSymbol("fif")]
		FeetInchesFraction4,

		[Precision(Length.LogicUnitPerCM),FormatSymbol("m")]
		Meter,

		[Precision(Length.LogicUnitPerCM),FormatSymbol("cm")]
		Centimeter,

		[Precision(Length.LogicUnitPerMM),FormatSymbol("mm")]
		Milimeter,
	}
}

