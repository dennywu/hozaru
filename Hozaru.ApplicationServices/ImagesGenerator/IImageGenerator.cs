using Hozaru.Core.Application.Services;
using Hozaru.Domain;
using Hozaru.Identity.MultiTenancy;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Png;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.ApplicationServices.ImagesGenerator
{
    public interface IImageGenerator : IApplicationService
    {
        string SavePaymentReceipt(Image image, string fileName, IImageFormat imageFormat);
        string SaveProductImage(Image image, string fileName, Product product, IImageFormat imageFormat);
        string SaveBrandImage(Image image, PngFormat imageFormat, Tenant tenant);
    }
}
