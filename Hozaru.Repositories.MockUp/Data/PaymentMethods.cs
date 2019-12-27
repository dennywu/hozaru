using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hozaru.Repositories.MockUp.Data
{
    public static class PaymentMethods
    {
        public static IQueryable<PaymentMethod> GetAll()
        {
            IList<PaymentMethod> paymentMethods = new List<PaymentMethod>();
            var bankBca = new Bank("BCA_MANUAL", "Bank BCA (Dicek Manual)", "BCA", @"D:\HozaruDevelopment\FileStorage\BankImages\bca.png", true);
            var bankMandiri = new Bank("MANDIRI_MANUAL", "Bank Mandiri (Dicek Manual)", "MANDIRI", @"D:\HozaruDevelopment\FileStorage\BankImages\mandiri.png", true);

            var bcaManual = new PaymentMethod(bankBca, "BATAM", "DENY", "0000000001");
            var mandiriManual = new PaymentMethod(bankMandiri, "BATAM", "DENY", "00000000200");

            paymentMethods.Add(bcaManual);
            paymentMethods.Add(mandiriManual);
            return paymentMethods.AsQueryable();
        }
    }
}
