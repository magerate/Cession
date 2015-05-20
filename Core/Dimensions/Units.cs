using System;

namespace Cession.Dimensions
{

    public class PrecisionAttribute:Attribute
    {
        public double Precission{ get; private set; }

        public PrecisionAttribute (double precission)
        {
            this.Precission = precission;
        }
    }

    public class FormatSymbolAttribute:Attribute
    {
        public string FormatSymbol{ get; private set; }

        public FormatSymbolAttribute (string formatSymbol)
        {
            this.FormatSymbol = formatSymbol;
        }
    }


    public enum Units
    {
        [Precision (Length.PixelsPerInch),FormatSymbol ("fi")]
        FeetInches = 0,

        [Precision (Length.PixelsPerInch),FormatSymbol ("fif")]
        FeetInchesFraction4,

        [Precision (Length.PixelsPerCentimeter),FormatSymbol ("m")]
        Meter,

        [Precision (Length.PixelsPerCentimeter),FormatSymbol ("cm")]
        Centimeter,

        [Precision (Length.PixelsPerCentimeter),FormatSymbol ("mm")]
        Milimeter,
    }
}

