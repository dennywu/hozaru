using Hozaru.Core.Application.Services;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Hozaru.ApplicationServices.ImagesGenerator
{
    public interface IImageGenerator : IApplicationService
    {
        string SavePaymentReceipt(Image image, string fileName);
    }
}
