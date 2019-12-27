using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hozaru.ApplicationServices.PaymentMethods.Dtos
{
    public class EditPaymentMethodInputDto
    {
        [Display(Name = "Metode Pembayaran")]
        [Required(ErrorMessageResourceType = typeof(MessagesDataAnnotation), ErrorMessageResourceName = "Required")]
        public Guid Id { get; set; }

        [Display(Name = "Kantor Cabang")]
        [Required(ErrorMessageResourceType = typeof(MessagesDataAnnotation), ErrorMessageResourceName = "Required")]
        public string BankBranch { get; set; }

        [Display(Name = "Nama Rekening")]
        [Required(ErrorMessageResourceType = typeof(MessagesDataAnnotation), ErrorMessageResourceName = "Required")]
        public string AccountName { get; set; }

        [Display(Name = "Nomor Rekening")]
        [Required(ErrorMessageResourceType = typeof(MessagesDataAnnotation), ErrorMessageResourceName = "Required")]
        public string AccountNumber { get; set; }

        [Display(Name = "Aktif")]
        [Required(ErrorMessageResourceType = typeof(MessagesDataAnnotation), ErrorMessageResourceName = "Required")]
        public bool IsDisabled { get; set; }
    }
}
