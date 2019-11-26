using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Hozaru.Core.Configurations;
using Hozaru.Core.Images;
using Hozaru.Domain;

namespace Hozaru.ApplicationServices.ImagesGenerator
{
    public class ImageGenerator : HozaruApplicationService, IImageGenerator
    {
        public string SavePaymentReceipt(Image image, string fileName, ImageFormat imageFormat)
        {
            var pathFileStorageDirectory = AppSettingConfigurationHelper.GetSection("PathFileStorageDirectory").Value;
            if (pathFileStorageDirectory == null || pathFileStorageDirectory.Equals(string.Empty))
                throw new Exception("File configuration must have App Setting with key PathFileStorageDirectory");

            var pathDirectoryProduct = Path.Combine(pathFileStorageDirectory, "Images", "PaymentReceipts");

            var directoryProductInfo = new DirectoryInfo(pathDirectoryProduct);

            if (!Directory.Exists(pathDirectoryProduct))
                Directory.CreateDirectory(pathDirectoryProduct);

            Bitmap bitmap = ImageResizer.FixedSize(image, 500, 500);
            var filePath = Path.Combine(directoryProductInfo.FullName, string.Format("{0}{1}", fileName, imageFormat.ToStringImageFormat()));
            bitmap.Save(filePath, ImageFormat.Jpeg);
            bitmap.Dispose();
            return Path.Combine("Images", "PaymentReceipts", fileName + imageFormat.ToStringImageFormat());
        }

        public string SaveProductImage(Image image, string fileName, Product product, ImageFormat imageFormat)
        {
            var pathFileStorageDirectory = AppSettingConfigurationHelper.GetSection("PathFileStorageDirectory").Value;
            if (pathFileStorageDirectory == null || pathFileStorageDirectory.Equals(string.Empty))
                throw new Exception("File configuration must have App Setting with key PathFileStorageDirectory");

            var pathDirectoryProduct = Path.Combine(pathFileStorageDirectory, "Images", "Products", product.Id.ToString());

            var directoryProductInfo = new DirectoryInfo(pathDirectoryProduct);

            if (!Directory.Exists(pathDirectoryProduct))
                Directory.CreateDirectory(pathDirectoryProduct);

            Bitmap bitmap = ImageResizer.FixedSize(image, 600, 600);
            var filePath = Path.Combine(directoryProductInfo.FullName, string.Format("{0}{1}", fileName, imageFormat.ToStringImageFormat()));
            bitmap.Save(filePath, imageFormat);
            bitmap.Dispose();
            return Path.Combine("Images", "Products", product.Id.ToString(), fileName + imageFormat.ToStringImageFormat());
        }
    }
}
