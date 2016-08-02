using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace SampleFive.CustomTools
{
    public class AccountingUtil
    {
        public static string ConvertMoneyToRialsWithComma(string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            return String.Format(new CultureInfo("fa-IR"), "{0:C0}", Convert.ToInt64(str)).Replace("ريال", "") + " ریال";
        }
        public static string ConvertMoneyToRialsWithComma(long str)
        {
            return String.Format(new CultureInfo("fa-IR"), "{0:C0}", str).Replace("ريال", "") + " ریال";
        }

        public static string ConvertMoneyToRialsWithComma(double str)
        {
            return String.Format(new CultureInfo("fa-IR"), "{0:C0}", str).Replace("ريال", "") + " ریال";
        }

        public static string RemoveSpecialCharacterMoney(string str)
        {
            return Regex.Replace(str, @"[^0-9\.]", string.Empty);
        }

        public static string RemoveSpecialCharacterMoney(double str)
        {
            return Regex.Replace(str.ToString(CultureInfo.CurrentCulture), @"[^0-9\.]", string.Empty);
        }

        public static double[] ConvertStringsToDoubles(string[] values)
        {
            if (!values.Any()) return null;
            return values.Select(value => Convert.ToDouble(RemoveSpecialCharacterMoney(value))).ToArray();
        }
        public static double ConvertStringToDouble(string str)
        {
            return string.IsNullOrEmpty(str) ? 0 : Convert.ToDouble(RemoveSpecialCharacterMoney(str));
        }
    }
}
