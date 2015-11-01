using System;
using System.Globalization;

using EVE_Salestats.Entities;

namespace EVE_Salestats
{
    class Settings
    {
        public static NumberFormatInfo numberFormat;

        public static AccountInfo accountInformation;

        static Settings()
        {
            Settings.numberFormat = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            Settings.numberFormat.NumberGroupSeparator = ".";
            Settings.numberFormat.NumberDecimalSeparator = ",";
        }
    }
}