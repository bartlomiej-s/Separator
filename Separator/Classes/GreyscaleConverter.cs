using System;
using System.Drawing;

namespace Separator
{
    public static class GreyscaleConverter
    {
        public static Bitmap ToGreyscale(Bitmap bitmap)
        {
            Color color;
            Bitmap bitmap1 = new Bitmap(bitmap.Width, bitmap.Height);
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    color = bitmap.GetPixel(i, j);

                    int Y = (int)Math.Round(0.299 * color.R + 0.587 * color.G + 0.114 * color.B);

                    bitmap1.SetPixel(i, j, Color.FromArgb(Y, Y, Y));
                }
            }
            return bitmap1;
        }
    }
}
