using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Polls.UserControls.EditTest
{
    public partial class SlideItemUC : UserControl
    {
        EditTestSlidesUC superOwner;

        int number = 0; // order number of item in list on EditTestSlidesUC (1-based)

        public SlideItemUC(EditTestSlidesUC owner, string title)
        {
            InitializeComponent();
            superOwner = owner;
            label1.Text = title;
        }

        public void SetNumber(int newNumber)
        {
            //if (number == 0)
            //    label1.Text = string.Concat("Вопрос №", newNumber.ToString());
            //Removed to constructor of slide
            number = newNumber;
            label2.Text = string.Concat(newNumber.ToString(), " слайд");
        }

        public void SetTitle(string newTitle)
        {
            label1.Text = newTitle;
        }

        private void pictureBox2_Click(object sender, EventArgs e)  // Edit
        {
            superOwner.EditSlide(number - 1);
        }

        private void pictureBox1_Click(object sender, EventArgs e)  // Delete
        {
            superOwner.RemoveSlide(number - 1);
        }

        public void setDeletable(bool isDeletable)
        {
            pictureBox1.Visible = isDeletable;
        }
    }
}
