using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Application.Services.Dto
{
    [Serializable]
    public class PagedResultOutput<T> : PagedResultDto<T>, IOutputDto
    {
        /// <summary>
        /// Creates a new <see cref="PagedResultOutput{T}"/> object.
        /// </summary>
        public PagedResultOutput()
        {

        }

        /// <summary>
        /// Creates a new <see cref="PagedResultOutput{T}"/> object.
        /// </summary>
        /// <param name="totalCount">Total count of Items</param>
        /// <param name="items">List of items in current page</param>
        public PagedResultOutput(int totalCount, IReadOnlyList<T> items)
            : base(totalCount, items)
        {

        }
    }
}
