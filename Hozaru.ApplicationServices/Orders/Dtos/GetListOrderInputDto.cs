using Hozaru.Core.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hozaru.ApplicationServices.Orders.Dtos
{
    public class GetListOrderInputDto : IInputDto, IPagedResultRequest
    {
        public OrderStatusInputDto Status { get; set; }
        public int SkipCount { get; set; }
        public int MaxResultCount { get; set; }

        public GetListOrderInputDto()
        {
            this.Status = OrderStatusInputDto.ALL;
        }
    }
}
