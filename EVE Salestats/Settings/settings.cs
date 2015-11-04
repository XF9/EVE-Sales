using System;
using System.Globalization;

using EVE_SaleTools.Entities;

namespace EVE_SaleTools
{
    /// <summary>
    /// Class to store all temporary settings
    /// </summary>
    class Settings
    {
        public static NumberFormatInfo numberFormat;

        public static AccountInfo accountInformation;

        public static Character[] characterList;

        static Settings()
        {
            Settings.numberFormat = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            Settings.numberFormat.NumberGroupSeparator = ".";
            Settings.numberFormat.NumberDecimalSeparator = ",";
        }
    }
}