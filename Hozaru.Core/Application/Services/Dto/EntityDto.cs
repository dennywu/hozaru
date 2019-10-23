using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Application.Services.Dto
{
    [Serializable]
    public class EntityDto<TPrimary> : IEntityDto<TPrimary>
    {
        public TPrimary Id { get; set; }

        public EntityDto() { }

        public EntityDto(TPrimary id)
        {
            Id = id;
        }
    }
}
