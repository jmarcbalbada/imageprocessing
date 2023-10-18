using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessing
{
    public partial class Form1 : Form
    {
        Bitmap loadPicture, result;
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color pixel;
            result = new Bitmap(loadPicture.Width, loadPicture.Height);
            for (int x = 0; x < loadPicture.Width; x++)
            {
                for (int y = 0; y < loadPicture.Height; y++)
                {
                    pixel = loadPicture.GetPixel(x, y);
                    result.SetPixel(x, y, pixel);
                    pictureBox2.Image = result;
                }
            }
        }

        private void greyscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color pixel;
            result = new Bitmap(loadPicture.Width, loadPicture.Height);
            for (int x = 0; x < loadPicture.Width; x++)
            {
                for (int y = 0; y < loadPicture.Height; y++)
                {
                    pixel = loadPicture.GetPixel(x, y);
                    int gray = ((pixel.R + pixel.G + pixel.B) / 3);
                    pixel = Color.FromArgb(gray, gray, gray);
                    result.SetPixel(x, y, pixel);
                    pictureBox2.Image = result;
                }
            }
        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color pixel;
            result = new Bitmap(loadPicture.Width, loadPicture.Height);
            for (int x = 0; x < loadPicture.Width; x++)
            {
                for (int y = 0; y < loadPicture.Height; y++)
                {
                    pixel = loadPicture.GetPixel(x, loadPicture.Height - y - 1);
                    result.SetPixel(x, y, pixel);
                    pictureBox2.Image = result;
                }
            }
        }

        private void mirrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // mirror flip X
            Color pixel;
            result = new Bitmap(loadPicture.Width, loadPicture.Height);
            for (int x = 0; x < loadPicture.Width; x++)
            {
                for (int y = 0; y < loadPicture.Height; y++)
                {
                    pixel = loadPicture.GetPixel(loadPicture.Width - x - 1, y);
                    result.SetPixel(x, y, pixel);
                    pictureBox2.Image = result;
                }
            }
        }

        private void colorInversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color pixel;
            result = new Bitmap(loadPicture.Width, loadPicture.Height);
            for (int x = 0; x < loadPicture.Width; x++)
            {
                for (int y = 0; y < loadPicture.Height; y++)
                {
                    pixel = loadPicture.GetPixel(x, y);
                    int fixedByte = 255;
                    pixel = Color.FromArgb(fixedByte - pixel.R, fixedByte - pixel.G, fixedByte - pixel.B);
                    result.SetPixel(x, y, pixel);
                    pictureBox2.Image = result;
                }
            }
        }

        private void blackAndWhiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color pixel;
            result = new Bitmap(loadPicture.Width, loadPicture.Height);

            for (int x = 0; x < loadPicture.Width; x++)
            {
                for (int y = 0; y < loadPicture.Height; y++)
                {
                    pixel = loadPicture.GetPixel(x, y);

                    // Calculate the grayscale value by averaging the RGB components
                    int grayValue = (int)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);

                    // Create a new black and white color (all channels set to the grayscale value)
                    Color bwColor = Color.FromArgb(grayValue, grayValue, grayValue);

                    // Set the pixel in the result image to the black and white color
                    result.SetPixel(x, y, bwColor);
                }
            }

            pictureBox2.Image = result;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1 != null && result != null) // Check if they are not null.
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        result.Save(saveFileDialog1.FileName + ".jpg");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving the file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                // Handle the case where the objects are null (e.g., display an error message or create new instances).
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Check if a file was selected
                try
                {
                    // Dispose of the old image if one exists
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                    }

                    // Load and display the new image
                    loadPicture = new Bitmap(openFileDialog1.FileName);
                    pictureBox1.Image = loadPicture;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading the image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}
