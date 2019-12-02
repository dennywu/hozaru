﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Application.Services.Dto
{
    public interface IPagedResultRequest : ILimitedResultRequest
    {
        int SkipCount { get; set; }
    }
}
