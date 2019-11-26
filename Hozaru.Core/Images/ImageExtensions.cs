using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;
using System.Linq;

namespace Hozaru.Core.Images
{
    public static class ImageExtensions
    {
        public static string ToStringImageFormat(this ImageFormat format)
        {
            try
            {
                return ImageCodecInfo.GetImageEncoders()
                        .First(x => x.FormatID == format.Guid)
                        .FilenameExtension
                        .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .First()
                        .Trim('*')
                        .ToLower();
            }
            catch (Exception)
            {
                return "." + format.ToString().ToLower();
            }
        }
    }
}
