using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Separator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeValues();
        }

        private void InitializeValues()
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 5;

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;

            decimal inc = 0.000001m;
            numericUpDown1.Increment = inc;
            numericUpDown2.Increment = inc;
            numericUpDown3.Increment = inc;
            numericUpDown4.Increment = inc;
            numericUpDown5.Increment = inc;
            numericUpDown6.Increment = inc;
            numericUpDown7.Increment = inc;
            numericUpDown8.Increment = inc;
            numericUpDown9.Increment = 0.01m;

            pictureBox1.Image = DrawingHelper.DrawFilledRectangle(pictureBox1.Width, pictureBox1.Height);
            pictureBox2.Image = DrawingHelper.DrawFilledRectangle(pictureBox2.Width, pictureBox2.Height);
            pictureBox3.Image = DrawingHelper.DrawFilledRectangle(pictureBox3.Width, pictureBox3.Height);
            pictureBox4.Image = DrawingHelper.DrawFilledRectangle(pictureBox4.Width, pictureBox4.Height);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = GreyscaleConverter.ToGreyscale(new Bitmap(pictureBox1.Image));
        }

        private void button2_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Multiselect = false;
            openFileDialog1.Title = "Open image file";
            openFileDialog1.InitialDirectory = @"Desktop";


            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    string filename = openFileDialog1.FileName;
                    FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    Byte[] bindata = new byte[Convert.ToInt32(fs.Length)];
                    fs.Read(bindata, 0, Convert.ToInt32(fs.Length));
                    MemoryStream stream = new MemoryStream(bindata);
                    stream.Position = 0;
                    pictureBox1.Image = Image.FromStream(stream);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    {
                        label11.Text = "Y";
                        label12.Text = "Cb";
                        label13.Text = "Cr";

                        Bitmap bitmap1, bitmap2, bitmap3;
                        (bitmap1, bitmap2, bitmap3) = SeparationModule.SeparateYCbCr(new Bitmap(pictureBox1.Image));

                        pictureBox2.Image = bitmap1;
                        pictureBox3.Image = bitmap2;
                        pictureBox4.Image = bitmap3;
                    }
                    break;
                case 1:
                    {
                        label11.Text = "H";
                        label12.Text = "S";
                        label13.Text = "V";

                        Bitmap bitmap1, bitmap2, bitmap3;
                        (bitmap1, bitmap2, bitmap3) = SeparationModule.SeparateHSV(new Bitmap(pictureBox1.Image));

                        pictureBox2.Image = bitmap1;
                        pictureBox3.Image = bitmap2;
                        pictureBox4.Image = bitmap3;
                    }
                    break;
                case 2:
                    {
                        label11.Text = "L";
                        label12.Text = "a";
                        label13.Text = "b";

                        Bitmap bitmap1, bitmap2, bitmap3;
                        (bitmap1, bitmap2, bitmap3) = SeparationModule.SeparateLab(new Bitmap(pictureBox1.Image), (double)numericUpDown1.Value, (double)numericUpDown2.Value,
                            (double)numericUpDown3.Value, (double)numericUpDown4.Value, (double)numericUpDown5.Value, (double)numericUpDown6.Value,
                            (double)numericUpDown7.Value, (double)numericUpDown8.Value, (double)numericUpDown9.Value);

                        pictureBox2.Image = bitmap1;
                        pictureBox3.Image = bitmap2;
                        pictureBox4.Image = bitmap3;
                    }
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SavingModule.Save(pictureBox2);
            SavingModule.Save(pictureBox3);
            SavingModule.Save(pictureBox4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = DrawingHelper.CreateSampleImage(pictureBox1.Width, pictureBox1.Height);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox c = sender as ComboBox;

            switch (c.SelectedIndex)
            {
                case 0:
                    {
                        groupBox1.Enabled = false;
                    }
                    break;
                case 1:
                    {
                        groupBox1.Enabled = false;
                    }
                    break;
                case 2:
                    {
                        groupBox1.Enabled = true;
                    }
                    break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox c = sender as ComboBox;

            switch (c.SelectedIndex)
            {
                case 0:
                    {
                        numericUpDown1.Value = (decimal)0.6400;
                        numericUpDown2.Value = (decimal)0.3300;
                        numericUpDown3.Value = (decimal)0.3000;
                        numericUpDown4.Value = (decimal)0.6000;
                        numericUpDown5.Value = (decimal)0.1500;
                        numericUpDown6.Value = (decimal)0.0600;
                        numericUpDown7.Value = (decimal)0.3127;
                        numericUpDown8.Value = (decimal)0.3290;
                        numericUpDown9.Value = (decimal)2.20;
                    }
                    break;
                case 1:
                    {
                        numericUpDown1.Value = (decimal)0.6400;
                        numericUpDown2.Value = (decimal)0.3300;
                        numericUpDown3.Value = (decimal)0.2100;
                        numericUpDown4.Value = (decimal)0.7100;
                        numericUpDown5.Value = (decimal)0.1500;
                        numericUpDown6.Value = (decimal)0.0600;
                        numericUpDown7.Value = (decimal)0.3127;
                        numericUpDown8.Value = (decimal)0.3290;
                        numericUpDown9.Value = (decimal)2.20;
                    }
                    break;
                case 2:
                    {
                        numericUpDown1.Value = (decimal)0.6250;
                        numericUpDown2.Value = (decimal)0.3400;
                        numericUpDown3.Value = (decimal)0.2800;
                        numericUpDown4.Value = (decimal)0.5950;
                        numericUpDown5.Value = (decimal)0.1550;
                        numericUpDown6.Value = (decimal)0.0700;
                        numericUpDown7.Value = (decimal)0.3127;
                        numericUpDown8.Value = (decimal)0.3290;
                        numericUpDown9.Value = (decimal)1.80;
                    }
                    break;
                case 3:
                    {
                        numericUpDown1.Value = (decimal)0.7350;
                        numericUpDown2.Value = (decimal)0.2650;
                        numericUpDown3.Value = (decimal)0.2740;
                        numericUpDown4.Value = (decimal)0.7170;
                        numericUpDown5.Value = (decimal)0.1670;
                        numericUpDown6.Value = (decimal)0.0090;
                        numericUpDown7.Value = (decimal)0.3333;
                        numericUpDown8.Value = (decimal)0.3333;
                        numericUpDown9.Value = (decimal)2.20;
                    }
                    break;
                case 4:
                    {
                        numericUpDown1.Value = (decimal)0.7347;
                        numericUpDown2.Value = (decimal)0.2653;
                        numericUpDown3.Value = (decimal)0.1152;
                        numericUpDown4.Value = (decimal)0.8264;
                        numericUpDown5.Value = (decimal)0.1566;
                        numericUpDown6.Value = (decimal)0.0177;
                        numericUpDown7.Value = (decimal)0.3457;
                        numericUpDown8.Value = (decimal)0.3585;
                        numericUpDown9.Value = (decimal)1.20;
                    }
                    break;
                case 5:
                    {
                        numericUpDown1.Value = (decimal)0.6400;
                        numericUpDown2.Value = (decimal)0.3300;
                        numericUpDown3.Value = (decimal)0.2900;
                        numericUpDown4.Value = (decimal)0.6000;
                        numericUpDown5.Value = (decimal)0.1500;
                        numericUpDown6.Value = (decimal)0.0600;
                        numericUpDown7.Value = (decimal)0.3127;
                        numericUpDown8.Value = (decimal)0.3290;
                        numericUpDown9.Value = (decimal)1.95;
                    }
                    break;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox c = sender as ComboBox;

            switch (c.SelectedIndex)
            {
                case 0:
                    {
                        numericUpDown7.Value = (decimal)0.44757;
                        numericUpDown8.Value = (decimal)0.40744;
                    }
                    break;
                case 1:
                    {
                        numericUpDown7.Value = (decimal)0.34840;
                        numericUpDown8.Value = (decimal)0.35160;
                    }
                    break;
                case 2:
                    {
                        numericUpDown7.Value = (decimal)0.31006;
                        numericUpDown8.Value = (decimal)0.31615;
                    }
                    break;
                case 3:
                    {
                        numericUpDown7.Value = (decimal)0.34567;
                        numericUpDown8.Value = (decimal)0.35850;
                    }
                    break;
                case 4:
                    {
                        numericUpDown7.Value = (decimal)0.33242;
                        numericUpDown8.Value = (decimal)0.34743;
                    }
                    break;
                case 5:
                    {
                        numericUpDown7.Value = (decimal)0.31273;
                        numericUpDown8.Value = (decimal)0.32902;
                    }
                    break;
                case 6:
                    {
                        numericUpDown7.Value = (decimal)0.29902;
                        numericUpDown8.Value = (decimal)0.31485;
                    }
                    break;
                case 7:
                    {
                        numericUpDown7.Value = (decimal)0.28480;
                        numericUpDown8.Value = (decimal)0.29320;
                    }
                    break;
                case 8:
                    {
                        numericUpDown7.Value = (decimal)0.33333;
                        numericUpDown8.Value = (decimal)0.33333;
                    }
                    break;
                case 9:
                    {
                        numericUpDown7.Value = (decimal)0.37207;
                        numericUpDown8.Value = (decimal)0.37512;
                    }
                    break;
                case 10:
                    {
                        numericUpDown7.Value = (decimal)0.31285;
                        numericUpDown8.Value = (decimal)0.32918;
                    }
                    break;
                case 11:
                    {
                        numericUpDown7.Value = (decimal)0.38054;
                        numericUpDown8.Value = (decimal)0.37691;
                    }
                    break;
            }
        }
    }
}
