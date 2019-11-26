using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Text;

namespace Hozaru.ApplicationServices.Orders.Dtos
{
    public class ConfirmationOrderInputDto
    {
        [Display(Name ="Order Id")]
        [Required(ErrorMessageResourceType = typeof(MessagesDataAnnotation), ErrorMessageResourceName = "Required")]
        public Guid Id { get; set; }

        [Display(Name = "Nama Bank")]
        [Required(ErrorMessageResourceType = typeof(MessagesDataAnnotation), ErrorMessageResourceName = "Required")]
        public string BankName { get; set; }

        [Display(Name = "Nama Rekening")]
        [Required(ErrorMessageResourceType = typeof(MessagesDataAnnotation), ErrorMessageResourceName = "Required")]
        public string AccountName { get; set; }

        [Display(Name = "Nomor Rekening")]
        [Required(ErrorMessageResourceType = typeof(MessagesDataAnnotation), ErrorMessageResourceName = "Required")]
        public string AccountNumber { get; set; }

        [Display(Name = "Bukti Pembayaran")]
        [Required(ErrorMessageResourceType = typeof(MessagesDataAnnotation), ErrorMessageResourceName = "Required")]
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
