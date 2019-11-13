using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Hozaru.ApplicationServices.Orders.Dtos
{
    public class ConfirmationOrderInputDto
    {
        public Guid Id { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public IFormFile PaymentReceipt { get; set; }

        public Image generateImage()
        {
            if (PaymentReceipt == null || PaymentReceipt.Length == 0)
                throw new Exception("Please upload payment receipt");

            var imageStream = PaymentReceipt.OpenReadStream();
            var image = Image.FromStream(imageStream);
            return image;
        }
    }
}
