using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;

namespace CaptchaGenerator
{
    public static class ImageGenerator
    {
        private static Random _random;
        static ImageGenerator()
        {
            _random = new Random();
        }

        public static Bitmap Generate(GenerateImageOptions options)
        {
            options.ValidateOptions();

            var bitmap = new Bitmap(options.Width, options.Height, PixelFormat.Format32bppArgb);
            //using (var bitmap = new Bitmap(options.Width, options.Height, PixelFormat.Format32bppArgb))
            //{
            using (var g = Graphics.FromImage(bitmap))
            {
                using (var path = new GraphicsPath())
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    var rect = new Rectangle(0, 0, options.Width, options.Height);
                    using (var hatchBrush = new HatchBrush(HatchStyle.SmallConfetti, options.BackColor, options.BackColor))
                    {
                        g.FillRectangle(hatchBrush, rect);

                        SizeF size;
                        Font font;

                        float fontSize = rect.Height + 1;
                        do
                        {
                            fontSize--;
                            font = new Font(FontFamily.GenericSansSerif, fontSize, FontStyle.Bold);
                            size = g.MeasureString(options.Text, font);
                        } while (size.Width > rect.Width);

                        var format = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };

                        //path.AddString(this.text, font.FontFamily, (int) font.Style, 
                        //    font.Size, rect, format);
                        path.AddString(options.Text, font.FontFamily, (int)font.Style, 75, rect, format);
                        font.Dispose();
                    }

                    float v = 4F;
                    PointF[] points = {
                            new PointF(_random.Next(rect.Width) / v, _random.Next(rect.Height) / v),
                            new PointF(rect.Width - _random.Next(rect.Width) / v, _random.Next(rect.Height) / v),
                            new PointF(_random.Next(rect.Width) / v, rect.Height - _random.Next(rect.Height) / v),
                            new PointF(rect.Width - _random.Next(rect.Width) / v, rect.Height - _random.Next(rect.Height) / v)
                        };

                    using (var matrix = new Matrix())
                    {
                        matrix.Translate(0F, 0F);
                        path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);

                        using (var hatchBrush = new HatchBrush(HatchStyle.Percent10, Color.White, options.ForeColor))
                        {
                            g.FillPath(hatchBrush, path);
                            int m = Math.Max(rect.Width, rect.Height);
                            for (int i = 0; i < (int)(rect.Width * rect.Height / 30F); i++)
                            {
                                int x = _random.Next(rect.Width);
                                int y = _random.Next(rect.Height);
                                int w = _random.Next(m / 50);
                                int h = _random.Next(m / 50);
                                g.FillEllipse(hatchBrush, x, y, w, h);
                            }
                        }
                    }
                    //}
                }

                return bitmap;
            }
        }

        private static void ValidateOptions(this GenerateImageOptions options)
        {
            if (options.Width <= 0)
                throw new ArgumentOutOfRangeException("width", options.Width,
                    "Argument out of range, must be greater than zero.");
            if (options.Height <= 0)
                throw new ArgumentOutOfRangeException("height", options.Height,
                    "Argument out of range, must be greater than zero.");
        }
    }
}








//public void Dispose()
//{
//    GC.SuppressFinalize(this);
//    this.Dispose(true);
//}
//protected virtual void Dispose(bool disposing)
//{
//    if (disposing)
//        this.image.Dispose();
//}



