using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Application.Services.Dto
{
    public interface IEntityDto<TPrimaryKey> : IDto
    {
        /// <summary>
        /// Id of the entity.
        /// </summary>
        TPrimaryKey Id { get; set; }
    }
}
