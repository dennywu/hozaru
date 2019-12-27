using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Hozaru.Core.Configurations;
using Hozaru.Core.Images;
using Hozaru.Domain;
using Hozaru.Identity.MultiTenancy;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;

namespace Hozaru.ApplicationServices.ImagesGenerator
{
    public class ImageGenerator : HozaruApplicationService, IImageGenerator
    {
        public string SaveBrandImage(Image image, PngFormat imageFormat, Tenant tenant)
        {
            var pathFileStorageDirectory = AppSettingConfigurationHelper.GetSection("PathFileStorageDirectory").Value;
            if (pathFileStorageDirectory == null || pathFileStorageDirectory.Equals(string.Empty))
                throw new Exception("File configuration must have App Setting with key PathFileStorageDirectory");

            var pathDirectoryProduct = Path.Combine(pathFileStorageDirectory, "Images", "Tenants", tenant.TenancyName);

            var directoryProductInfo = new DirectoryInfo(pathDirectoryProduct);

            if (!Directory.Exists(pathDirectoryProduct))
                Directory.CreateDirectory(pathDirectoryProduct);

            var resizedImage = ImageResizer.FixedSize(image, 100, 100);
            var filePath = Path.Combine(directoryProductInfo.FullName, string.Format("{0}{1}", "brand", imageFormat.GetFileExtension()));

            var encoder = imageFormat.GetEncoder();
            resizedImage.Save(filePath, encoder);
            resizedImage.Dispose();
            return Path.Combine("Images", "Tenants", tenant.TenancyName, "brand" + imageFormat.GetFileExtension());
        }

        public string SavePaymentReceipt(Image image, string fileName, IImageFormat imageFormat)
        {
            var pathFileStorageDirectory = AppSettingConfigurationHelper.GetSection("PathFileStorageDirectory").Value;
            if (pathFileStorageDirectory == null || pathFileStorageDirectory.Equals(string.Empty))
                throw new Exception("File configuration must have App Setting with key PathFileStorageDirectory");

            var pathDirectoryProduct = Path.Combine(pathFileStorageDirectory, "Images", "PaymentReceipts");

            var directoryProductInfo = new DirectoryInfo(pathDirectoryProduct);

            if (!Directory.Exists(pathDirectoryProduct))
                Directory.CreateDirectory(pathDirectoryProduct);

            Image resizedImage = ImageResizer.FixedSize(image, 500, 500);

            var filePath = Path.Combine(directoryProductInfo.FullName, string.Format("{0}{1}", fileName, imageFormat.GetFileExtension()));

            var encoder = imageFormat.GetEncoder();
            resizedImage.Save(filePath, encoder);
            resizedImage.Dispose();
            return Path.Combine("Images", "PaymentReceipts", fileName + imageFormat.GetFileExtension());
        }

        public string SaveProductImage(Image image, string fileName, Product product, IImageFormat imageFormat)
        {
            var pathFileStorageDirectory = AppSettingConfigurationHelper.GetSection("PathFileStorageDirectory").Value;
            if (pathFileStorageDirectory == null || pathFileStorageDirectory.Equals(string.Empty))
                throw new Exception("File configuration must have App Setting with key PathFileStorageDirectory");

            var pathDirectoryProduct = Path.Combine(pathFileStorageDirectory, "Images", "Products", product.Id.ToString());

            var directoryProductInfo = new DirectoryInfo(pathDirectoryProduct);

            if (!Directory.Exists(pathDirectoryProduct))
                Directory.CreateDirectory(pathDirectoryProduct);

            var resizedImage = ImageResizer.FixedSize(image, 600, 600);
            var filePath = Path.Combine(directoryProductInfo.FullName, string.Format("{0}{1}", fileName, imageFormat.GetFileExtension()));

            var encoder = imageFormat.GetEncoder();
            resizedImage.Save(filePath, encoder);
            resizedImage.Dispose();
            return Path.Combine("Images", "Products", product.Id.ToString(), fileName + imageFormat.GetFileExtension());
        }
    }
}
