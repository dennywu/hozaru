using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hozaru.ApplicationServices.Orders.Dtos
{
    public class CreateOrderItemInputDto
    {
        [Display(Name = "Produk")]
        [Required(ErrorMessageResourceType = typeof(MessagesDataAnnotation), ErrorMessageResourceName = "Required")]
        public Guid ProductId { get; set; }

        [Range(1, 1000, ErrorMessage = "Kuantitas Produk tidak boleh lebih kecil dari 1 dan tidak besar dari 1.000")]
        public int Quantity { get; set; }

        public string Note { get; set; }
    }
}
