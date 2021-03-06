﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hozaru.ApplicationServices.Freights.Dtos
{
    public class GetFreightInputDto
    {
        [Display(Name = "Kota Penerima")]
        [Required(ErrorMessageResourceType = typeof(MessagesDataAnnotation), ErrorMessageResourceName = "Required")]
        public Guid CityId { get; set; }


        [Display(Name = "Kecamatan Penerima")]
        [Required(ErrorMessageResourceType = typeof(MessagesDataAnnotation), ErrorMessageResourceName = "Required")]
        public Guid DistrictId { get; set; }

        public IList<FreightShoppingCartItem> Items { get; set; }
    }
}
