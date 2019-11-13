using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using Hozaru.Core.Configurations;
using Hozaru.Core.Images;
using Hozaru.Domain;

namespace Hozaru.ApplicationServices.ImagesGenerator
{
    public class ImageGenerator : HozaruApplicationService, IImageGenerator
    {
        public string SavePaymentReceipt(Image image, string fileName)
        {
            string pathDirectoryProduct;
            var pathFileStorageDirectory = AppSettingConfigurationHelper.GetSection("PathFileStorageDirectory").Value;
            if (pathFileStorageDirectory == null || pathFileStorageDirectory.Equals(string.Empty))
                throw new Exception("File configuration must have App Setting with key PathFileStorageDirectory");

            pathDirectoryProduct = string.Format(@"{0}\Images\PaymentReceipts", pathFileStorageDirectory);

            var directoryProductInfo = new DirectoryInfo(pathDirectoryProduct);

            if (!Directory.Exists(pathDirectoryProduct))
                Directory.CreateDirectory(pathDirectoryProduct);

            Bitmap bitmap = ImageResizer.FixedSize(image, 500, 500);
            var filePath = Path.Combine(directoryProductInfo.FullName, string.Format("{0}.png", fileName));
            bitmap.Save(filePath);
            return filePath;
        }
    }
}
