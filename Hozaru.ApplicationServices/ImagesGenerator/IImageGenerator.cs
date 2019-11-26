using Hozaru.Core.Application.Services;
using Hozaru.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace Hozaru.ApplicationServices.ImagesGenerator
{
    public interface IImageGenerator : IApplicationService
    {
        string SavePaymentReceipt(Image image, string fileName, ImageFormat imageFormat);
        string SaveProductImage(Image image, string fileName, Product product, ImageFormat imageFormat);
    }
}
