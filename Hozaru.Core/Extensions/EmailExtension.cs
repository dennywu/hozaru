using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Hozaru
{
    public static class EmailExtension
    {
        static bool invalid = false;

        public static bool IsValidEmail(this string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            strIn = Regex.Replace(strIn, @"(@)(.+)$", DomainMapper,
                                  RegexOptions.None);


            if (invalid)
                return false;

            return Regex.IsMatch(strIn,
                  @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                  RegexOptions.IgnoreCase);
        }

        private static string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
    }
}
