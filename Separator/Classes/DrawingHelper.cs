using System;
using System.Drawing;

namespace Separator
{
    public static class DrawingHelper
    {
        public static Bitmap DrawFilledRectangle(int x, int y)
        {
            Bitmap bmp = new Bitmap(x, y);
            using (Graphics graph = Graphics.FromImage(bmp))
            {
                Rectangle ImageSize = new Rectangle(0, 0, x, y);
                graph.FillRectangle(Brushes.White, ImageSize);
            }
            return bmp;
        }

        public static Bitmap CreateSampleImage(int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);

            int sizeblack = 50;
            int r = 100;

            Color[][] image = new Color[bitmap.Width][];
            for (int i = 0; i < bitmap.Width; i++)
            {
                image[i] = new Color[bitmap.Height];
                for (int j = 0; j < bitmap.Height; j++)
                {
                    image[i][j] = Color.White;
                }
            }

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < sizeblack; j++)
                {
                    image[i][j] = Color.Black;
                }
            }
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = bitmap.Height - 1; j > bitmap.Height - 1 - sizeblack; j--)
                {
                    image[i][j] = Color.Black;
                }
            }
            for (int i = 0; i < sizeblack; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    image[i][j] = Color.Black;
                }
            }
            for (int i = bitmap.Width - 1; i > bitmap.Width - 1 - sizeblack; i--)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    image[i][j] = Color.Black;
                }
            }


            int x = width / 2;
            int y = height / 2;
            for (int i = -r; i <= r; i++)
                for (int j = -r; j <= r; j++)
                    if (i * i + j * j <= r * r)
                    {
                        double deg = Math.Atan2(i, j) * 180 / Math.PI;
                        if (deg < 0) deg += 360;
                        double dist = Math.Sqrt(Math.Pow(i, 2) + Math.Pow(j, 2)) / r;
                        image[x + i][y + j] = DrawingHelper.ColorFromHSV(deg, dist, 1);
                    }

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    bitmap.SetPixel(i, j, image[i][j]);
                }
            }

            return bitmap;
        }

        private static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }
    }
}
