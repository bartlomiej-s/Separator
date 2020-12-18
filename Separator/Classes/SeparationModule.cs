using System;
using System.Drawing;

namespace Separator
{
    public static class SeparationModule
    {
        public static (Bitmap, Bitmap, Bitmap) SeparateYCbCr(Bitmap bitmap)
        {
            Color color;
            Bitmap bitmap1 = new Bitmap(bitmap.Width, bitmap.Height);
            Bitmap bitmap2 = new Bitmap(bitmap.Width, bitmap.Height);
            Bitmap bitmap3 = new Bitmap(bitmap.Width, bitmap.Height);
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    color = bitmap.GetPixel(i, j);

                    int Y = (int)Math.Round(16 + 65.738 * color.R / 256 + 129.057 * color.G / 256 + 25.064 * color.B / 256);
                    int Cb = (int)Math.Round(128 - 37.945 * color.R / 256 - 74.494 * color.G / 256 + 112.439 * color.B / 256);
                    int Cr = (int)Math.Round(128 + 112.439 * color.R / 256 - 94.154 * color.G / 256 - 18.285 * color.B / 256);

                    bitmap1.SetPixel(i, j, Color.FromArgb(Y, Y, Y));
                    bitmap2.SetPixel(i, j, Color.FromArgb(127, 255 - Cb, Cb));
                    bitmap3.SetPixel(i, j, Color.FromArgb(Cr, 255 - Cr, 127));
                }
            }
            return (bitmap1, bitmap2, bitmap3);
        }

        public static (Bitmap, Bitmap, Bitmap) SeparateHSV(Bitmap bitmap)
        {
            Color color;
            Bitmap bitmap1 = new Bitmap(bitmap.Width, bitmap.Height);
            Bitmap bitmap2 = new Bitmap(bitmap.Width, bitmap.Height);
            Bitmap bitmap3 = new Bitmap(bitmap.Width, bitmap.Height);
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    color = bitmap.GetPixel(i, j);

                    double Rpom = (double)color.R / 255;
                    double Gpom = (double)color.G / 255;
                    double Bpom = (double)color.B / 255;
                    double Cmax = Math.Max(Math.Max(Rpom, Gpom), Bpom);
                    double Cmin = Math.Min(Math.Min(Rpom, Gpom), Bpom);
                    double delta = Cmax - Cmin;

                    double H = 0;
                    if (delta == 0) H = 0;
                    else if (Cmax == Rpom)
                    {
                        double pom = (Gpom - Bpom) / delta;
                        if (pom > 0) H = 60 * (pom % 6);
                        else H = 60 * (pom % 6 + 6);
                    }
                    else if (Cmax == Gpom) H = 60 * (((Bpom - Rpom) / delta) + 2);
                    else if (Cmax == Bpom) H = 60 * (((Rpom - Gpom) / delta) + 4);

                    double S;
                    if (Cmax == 0) S = 0;
                    else S = delta / Cmax;

                    double V = Cmax;

                    int Hpom = (int)Math.Round((H / 360) * 255);
                    int Spom = (int)Math.Round(S * 255);
                    int Vpom = (int)Math.Round(V * 255);

                    bitmap1.SetPixel(i, j, Color.FromArgb(Hpom, Hpom, Hpom));
                    bitmap2.SetPixel(i, j, Color.FromArgb(Spom, Spom, Spom));
                    bitmap3.SetPixel(i, j, Color.FromArgb(Vpom, Vpom, Vpom));
                }
            }
            return (bitmap1, bitmap2, bitmap3);
        }

        public static (Bitmap, Bitmap, Bitmap) SeparateLab(Bitmap bitmap, double xr, double yr, double xg, double yg, double xb, double yb, double xw, double yw, double gamma)
        {
            Color color;
            Bitmap bitmap1 = new Bitmap(bitmap.Width, bitmap.Height);
            Bitmap bitmap2 = new Bitmap(bitmap.Width, bitmap.Height);
            Bitmap bitmap3 = new Bitmap(bitmap.Width, bitmap.Height);
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    color = bitmap.GetPixel(i, j);

                    double R = Math.Pow(color.R / 255.0, gamma);
                    double G = Math.Pow(color.G / 255.0, gamma);
                    double B = Math.Pow(color.B / 255.0, gamma);

                    R *= 100;
                    G *= 100;
                    B *= 100;

                    double Yw = 1;
                    double Xw = (Yw / yw) * xw;
                    double Zw = (Yw / yw) * (1 - xw - yw);

                    double zr = (1 - xr - yr);
                    double zg = (1 - xg - yg);
                    double zb = (1 - xb - yb);

                    double[][] Mpom = new double[3][];
                    Mpom[0] = new double[3] { xr, xg, xb };
                    Mpom[1] = new double[3] { yr, yg, yb };
                    Mpom[2] = new double[3] { zr, zg, zb };

                    double[][] Minv = MatrixInverse(Mpom);

                    double Sr = (Xw * Minv[0][0]) + (Yw * Minv[0][1]) + (Zw * Minv[0][2]);
                    double Sg = (Xw * Minv[1][0]) + (Yw * Minv[1][1]) + (Zw * Minv[1][2]);
                    double Sb = (Xw * Minv[2][0]) + (Yw * Minv[2][1]) + (Zw * Minv[2][2]);

                    double[,] M = new double[,] { { Sr * xr, Sg * xg,  Sb * xb},
                                                { Sr * yr, Sg * yg,  Sb * yb},
                                                { Sr * zr, Sg * zg,  Sb * zb}};

                    double X = (R * M[0, 0]) + (G * M[0, 1]) + (B * M[0, 2]);
                    double Y = (R * M[1, 0]) + (G * M[1, 1]) + (B * M[1, 2]);
                    double Z = (R * M[2, 0]) + (G * M[2, 1]) + (B * M[2, 2]);

                    Yw = 100;
                    Xw = (Yw / yw) * xw;
                    Zw = (Yw / yw) * (1 - xw - yw);

                    int L = (int)Math.Round(((116 * f(Y / Yw) - 16) / 100) * 255);
                    int a = (int)Math.Round(((500 * (f(X / Xw) - f(Y / Yw))) / 127) * 75);
                    int b = (int)Math.Round((((200 * (f(Y / Yw) - f(Z / Zw)))) / 127) * 75);

                    bitmap1.SetPixel(i, j, Color.FromArgb(L, L, L));
                    bitmap2.SetPixel(i, j, Color.FromArgb(127 + a, 127 - a, 127));
                    bitmap3.SetPixel(i, j, Color.FromArgb(127 + b, 127, 127 - b));
                }
            }
            return (bitmap1, bitmap2, bitmap3);
        }

        private static double[][] MatrixInverse(double[][] m)
        {
            double det = m[0][0] * (m[1][1] * m[2][2] - m[2][1] * m[1][2]) -
             m[0][1] * (m[1][0] * m[2][2] - m[1][2] * m[2][0]) +
             m[0][2] * (m[1][0] * m[2][1] - m[1][1] * m[2][0]);

            double invdet = 1 / det;

            double[][] minv = new double[3][];
            for (int i = 0; i < 3; i++)
            {
                minv[i] = new double[3];
            }
            minv[0][0] = (m[1][1] * m[2][2] - m[2][1] * m[1][2]) * invdet;
            minv[0][1] = (m[0][2] * m[2][1] - m[0][1] * m[2][2]) * invdet;
            minv[0][2] = (m[0][1] * m[1][2] - m[0][2] * m[1][1]) * invdet;
            minv[1][0] = (m[1][2] * m[2][0] - m[1][0] * m[2][2]) * invdet;
            minv[1][1] = (m[0][0] * m[2][2] - m[0][2] * m[2][0]) * invdet;
            minv[1][2] = (m[1][0] * m[0][2] - m[0][0] * m[1][2]) * invdet;
            minv[2][0] = (m[1][0] * m[2][1] - m[2][0] * m[1][1]) * invdet;
            minv[2][1] = (m[2][0] * m[0][1] - m[0][0] * m[2][1]) * invdet;
            minv[2][2] = (m[0][0] * m[1][1] - m[1][0] * m[0][1]) * invdet;

            return minv;
        }

        private static double f(double t)
        {
            double delta = 6.0 / 29;
            if (t > Math.Pow(delta, 3))
            {
                return Math.Pow(t, 1.0 / 3);
            }
            else
            {
                return ((t / (3 * Math.Pow(delta, 2))) + 4.0 / 29);
            }
        }
    }
}
