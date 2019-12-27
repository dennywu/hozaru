using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hozaru.ApplicationServices.Freights.Dtos
{
    public class GetFreightByServiceInputDto
    {
        public ExpeditionService ExpeditionService { get; private set; }
        public Districts Origin { get; private set; }
        public Districts Destination { get; private set; }
        public decimal Weight { get; private set; }

        public GetFreightByServiceInputDto(ExpeditionService expeditionService, Districts origin, Districts destination, decimal weight)
        {
            this.ExpeditionService = expeditionService;
            this.Origin = origin;
            this.Destination = destination;
            this.Weight = weight;
        }
    }
}
