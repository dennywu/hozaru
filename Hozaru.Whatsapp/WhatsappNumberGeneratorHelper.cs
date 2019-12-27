using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Whatsapp
{
    public static class WhatsappNumberGeneratorHelper
    {
        public static string GenerateWhatsappUrl(string whatsappNumber)
        {
            whatsappNumber = whatsappNumber.Replace(" ", string.Empty).Trim();
            var whatsappNumberFormated = whatsappNumber;
            if (whatsappNumber.StartsWith("0"))
            {
                var removedZeroWhatsappNumber = whatsappNumber.Remove(0, 1);
                whatsappNumberFormated = string.Format("62{0}", removedZeroWhatsappNumber);
            }
            else if (whatsappNumber.StartsWith("+"))
            {
                var removedPlusWhatsappNumber = whatsappNumber.Remove(0, 1);
                whatsappNumberFormated = string.Format("{0}", removedPlusWhatsappNumber);
            }

            return string.Format("https://wa.me/{0}", whatsappNumberFormated);
        }
    }
}
