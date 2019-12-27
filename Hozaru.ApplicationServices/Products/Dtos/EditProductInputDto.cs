using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hozaru.ApplicationServices.Products.Dtos
{
    public class EditProductInputDto
    {
        [Display(Name = "Produk Id")]
        [Required(ErrorMessageResourceType = typeof(MessagesDataAnnotation), ErrorMessageResourceName = "Required")]
        public Guid Id { get; set; }

        [Display(Name = "Nama Produk")]
        [MinLength(5, ErrorMessage = "Nama Produk harus lebih dari 5 karakter.")]
        [MaxLength(128, ErrorMessage = "Nama Produk tidak boleh lebih dari 128 karakter.")]
        [Required(ErrorMessageResourceType = typeof(MessagesDataAnnotation), ErrorMessageResourceName = "Required")]
        [RegularExpression(@"^[^\\/:*;\.\)\(]+$", ErrorMessage = "Karakter :, ., ;, *, / and '\' tidak diizinkan untuk digunakan")]
        public string Name { get; set; }

        [Display(Name = "Deskripsi Produk")]
        [MinLength(20, ErrorMessage = "Deskripsi Produk harus lebih dari 20 karakter.")]
        [MaxLength(4000, ErrorMessage = "Nama Produk tidak boleh lebih dari 4000 karakter.")]
        [Required(ErrorMessageResourceType = typeof(MessagesDataAnnotation), ErrorMessageResourceName = "Required")]
        public string Description { get; set; }

        [Range(typeof(decimal), "1", "100000000", ErrorMessage = "Harga Produk tidak boleh lebih kecil dari Rp 1 dan tidak besar dari Rp 100.000.000")]
        public decimal Price { get; set; }

        [Range(typeof(decimal), "1", "100000000", ErrorMessage = "Berat Produk tidak boleh lebih kecil dari 1 Gram dan tidak besar dari 100.000.000 Gram")]
        public decimal Weight { get; set; }

        [MaxLength(32, ErrorMessage = "SKU Produk tidak boleh lebih dari 32 karakter.")]
        public string SKU { get; set; }

        public IList<ProductImageInputDto> Images { get; set; }
        public IList<int> DeletedImagesByPriority { get; set; }

        public EditProductInputDto()
        {
            Images = new List<ProductImageInputDto>();
            DeletedImagesByPriority = new List<int>();
            SKU = string.Empty;
        }
    }
}
