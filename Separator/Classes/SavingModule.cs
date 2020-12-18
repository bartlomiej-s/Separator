using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Separator
{
    public static class SavingModule
    {
        public static void Save(PictureBox pb)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "bmp files (*.bmp)|*.bmp";
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image img = pb.Image;
                Bitmap bmp = new Bitmap(pb.Width, pb.Height);
                pb.DrawToBitmap(bmp, new Rectangle(0, 0, pb.Width, pb.Height));
                bmp.Save(saveFileDialog1.FileName, ImageFormat.Bmp);
            }
        }
    }
}
