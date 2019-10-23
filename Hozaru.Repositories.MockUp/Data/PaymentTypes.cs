using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hozaru.Repositories.MockUp.Data
{
    public static class PaymentTypes
    {
        public static IQueryable<PaymentType> GetAll()
        {
            IList<PaymentType> paymentTypes = new List<PaymentType>();
            var bcaManual = new PaymentType("BCA_MANUAL", "Bank BCA (Dicek Manual)", "BCA", "BATAM", "DENY", "0000000001", true, @"D:\HozaruDevelopment\FileStorage\BankImages\bca.png");
            var mandiriManual = new PaymentType("MANDIRI_MANUAL", "Bank Mandiri (Dicek Manual)", "MANDIRI", "BATAM", "DENY", "00000000200", true, @"D:\HozaruDevelopment\FileStorage\BankImages\mandiri.png");

            paymentTypes.Add(bcaManual);
            paymentTypes.Add(mandiriManual);
            return paymentTypes.AsQueryable();
        }
    }
}
