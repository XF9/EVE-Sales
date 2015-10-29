using System;
using System.Globalization;

namespace EVE_Salestats
{
    class Settings
    {
        public static NumberFormatInfo numberFormat;

        static Settings()
        {
            Settings.numberFormat = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            Settings.numberFormat.NumberGroupSeparator = ".";
            Settings.numberFormat.NumberDecimalSeparator = ",";
        }
    }
}