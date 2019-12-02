using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Application.Services.Dto
{
    public interface ILimitedResultRequest
    {
        /// <summary>
        /// Max expected result count.
        /// </summary>
        int MaxResultCount { get; set; }
    }
}
