using Hozaru.Core.Application.Services.Dto;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.Freights.Dtos
{
    public class FreightDto
    {
        public Guid ExpeditionServiceId { get; set; }
        public string ExpeditionFullName { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public decimal TotalWeight { get; set; }
        public EstimatedTimeDelivery EstimatedTimeDelivery { get; set; }
        public string ExpeditionServiceGroupName { get; set; }
    }
}
