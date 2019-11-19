using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Hozaru.Core.Images
{
    public static class ImageResizer
    {
        public static Bitmap Resize(System.Drawing.Image image, int defaultWidth = 500, int defaultHeight = 500)
        {
            int width = defaultWidth;
            int height = defaultHeight;
            double actualHeight = image.Height;
            double actualWidth = image.Width;

            if (width == 0 || height == 0)
                return (Bitmap)image;

            Bitmap imgResized = new Bitmap(width, height);
            imgResized.SetResolution(72, 72);

            using (Graphics graphic = Graphics.FromImage(imgResized))
            {
                graphic.Clear(Color.White);
                graphic.CompositingQuality = CompositingQuality.HighQuality;
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = SmoothingMode.HighQuality;

                double factor = 1;
                if (width > 0)
                    factor = width / actualWidth;
                else if (height > 0)
                    factor = height / actualHeight;

                var sourceRectangle = new Rectangle(0, 0, (int)actualWidth, (int)actualHeight);
                var destinationRectangle = new Rectangle(0, 0, (int)(factor * actualWidth), (int)(factor * actualHeight));

                if (width > destinationRectangle.X)
                {
                    destinationRectangle.X = (width - destinationRectangle.Width) / 2;
                }
                if (height > destinationRectangle.Y)
                {
                    destinationRectangle.Y = (height - destinationRectangle.Height) / 2;
                }
                graphic.DrawImage(image, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);
            }
            return imgResized;
        }

        public static Bitmap FixedSize(Image imgPhoto, int width, int height)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)width / (float)sourceWidth);
            nPercentH = ((float)height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((width -
                              (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((height -
                              (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(width, height);
            bmPhoto.SetResolution(72,72);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.White);
            grPhoto.InterpolationMode =
                    InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }
    }
}
