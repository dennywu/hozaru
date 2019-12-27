using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Gif;

namespace Hozaru.Core.Images
{
    public static class ImageExtensions
    {
        public static string GetFileExtension(this IImageFormat imageFormat)
        {
            if (imageFormat is JpegFormat)
                return ".jpg";
            if (imageFormat is PngFormat)
                return ".png";
            if (imageFormat is GifFormat)
                return ".gif";

            throw new HozaruException("Invalid file format");
        }

        public static IImageEncoder GetEncoder(this IImageFormat imageFormat)
        {
            if (imageFormat is JpegFormat)
                return new JpegEncoder { Quality = 100 };
            if (imageFormat is PngFormat)
                return new PngEncoder();
            if (imageFormat is GifFormat)
                return new GifEncoder();

            throw new HozaruException("Invalid file format");
        }
    }
}
