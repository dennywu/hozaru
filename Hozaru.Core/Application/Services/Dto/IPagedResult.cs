﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Application.Services.Dto
{
    public interface IPagedResult<T> : IListResult<T>, IHasTotalCount
    {

    }
}
