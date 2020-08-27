using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Polls.UserControls.PassingTest
{
    public partial class ImageViewUC : OwnedUserControl
    {
        public ImageViewUC(MainForm owner, string imageBase64) : base(owner)
        {
            InitializeComponent();

            putImage(imageBase64);
        }

        private void putImage(string imageString)
        {
            Image image = Image.FromStream(new MemoryStream(Convert.FromBase64String(imageString)));
            double ratio = image.Width * 1.0 / image.Height;
            if (ratio < 1)
            {
                pictureBox2.Height = (int)(pictureBox2.Width * image.Height * 1.0 / image.Width);
            }
            else
            {
                pictureBox2.Width = (int)(pictureBox2.Height * image.Width * 1.0 / image.Height);
            }
            pictureBox2.Image = image;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Owner.popUC();
        }
    }
}
