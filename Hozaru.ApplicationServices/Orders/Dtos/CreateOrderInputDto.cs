using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hozaru.ApplicationServices.Orders.Dtos
{
    public class CreateOrderInputDto
    {
        [Display(Name = "Nama Penerima")]
        [Required(ErrorMessageResourceType = typeof(MessagesDataAnnotation), ErrorMessageResourceName = "Required")]
        public string Name { get; set; }

        [Display(Name = "Nomor Whatsapp")]
        [Required(ErrorMessageResourceType = typeof(MessagesDataAnnotation), ErrorMessageResourceName = "Required")]
        public string Whatsapp { get; set; }

        [Display(Name = "Email Penerima")]
        [Required(ErrorMessageResourceType = typeof(MessagesDataAnnotation), ErrorMessageResourceName = "Required")]
        public string Email { get; set; }

        [Display(Name = "Kecamatan Penerima")]
        [Required(ErrorMessageResourceType = typeof(MessagesDataAnnotation), ErrorMessageResourceName = "Required")]
        public string DistrictCode { get; set; }

        [Display(Name = "Alamat Penerima")]
        [Required(ErrorMessageResourceType = typeof(MessagesDataAnnotation), ErrorMessageResourceName = "Required")]
        public string Address { get; set; }
        public string Note { get; set; }

        [RequiredNotEmpty(ErrorMessage = "Silahkan pilih produk.")]
        public IList<CreateOrderItemInputDto> Items { get; set; }

        [Display(Name = "Ekpedisi")]
        [Required(ErrorMessageResourceType = typeof(MessagesDataAnnotation), ErrorMessageResourceName = "Required")]
        public string ExpeditionCode { get; set; }

        [Display(Name = "Tipe Pembayaran")]
        [Required(ErrorMessageResourceType = typeof(MessagesDataAnnotation), ErrorMessageResourceName = "Required")]
        public string PaymentTypeCode { get; set; }

        public CreateOrderInputDto()
        {
            this.Items = new List<CreateOrderItemInputDto>();
        }
    }
}
