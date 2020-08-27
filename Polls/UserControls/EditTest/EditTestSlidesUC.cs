using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Polls.Models;

namespace Polls.UserControls.EditTest
{
    public partial class EditTestSlidesUC : UserControl
    {
        private List<SlideItemUC> slideItems = new List<SlideItemUC>();
        private Test test;
        private EditTestUC superOwner;

        public EditTestSlidesUC(Test test)
        {
            this.test = test;
            InitializeComponent();

            if (slideItems.Count.Equals(0)) // test is got from Api
            {
                loadSlides();
            }

            refresh();
        }

        private void loadSlides()
        {
            foreach (Slide slide in test.slides)
            {
                SlideItemUC slideItem = new SlideItemUC(this, slide.question);
                slideItems.Add(slideItem);

                flowLayoutPanel1.Controls.Add(slideItem);
            }
        }

        private void refresh()
        {
            SlideItemUC slideItem;

            for (int i = 0; i < slideItems.Count; ++i)
            {
                slideItem = slideItems[i];
                slideItem.SetNumber(i + 1);
                test.slides[i].slideNumber = i;
                slideItem.setDeletable(true);

                if (test.GetSlide(i).question.Length > 17)
                {
                    slideItem.SetTitle(string.Concat(test.GetSlide(i).question.Substring(0, 17), "..."));
                }
                else
                {
                    slideItem.SetTitle(test.GetSlide(i).question);
                }
            }
            if (slideItems.Count.Equals(1))
                slideItems[0].setDeletable(false);
            flowLayoutPanel1.Focus();

            if (slideItems.Count.Equals(50))
                pictureBox3.Visible = false;
            else
                pictureBox3.Visible = true;
        }

        public void SetSuperOwner(EditTestUC superOwner)
        {
            this.superOwner = superOwner;
        }

        public void RemoveSlide(int slideNumber)
        {
            int next;
            if (test.slides[slideNumber].answers.Count.Equals(0))
                next = -1;
            else
                next = test.slides[slideNumber].answers[0].nextSlideNumber; // if forked is deleted some slides may be not reachable
            if (next > slideNumber)
                --next;
            test.slides.RemoveAt(slideNumber);
            slideItems.RemoveAt(slideNumber);
            flowLayoutPanel1.Controls.RemoveAt(slideNumber);

            foreach (Slide slide in test.slides)
            {
                //// this does refresh()
                //
                //if (slide.slideNumber > slideNumber)
                //{
                //    --slide.slideNumber;
                //}
                ////
                foreach (Answer answer in slide.answers)
                {
                    if (answer.nextSlideNumber.Equals(slideNumber))
                        answer.nextSlideNumber = next;
                    else if (answer.nextSlideNumber > slideNumber)
                        --answer.nextSlideNumber;
                }
            }

            refresh();
        }

        public void EditSlide(int slideNumber)  // open new UC
        {
            superOwner.ChangeToEditSlide(slideNumber);
        }

        public void pictureBox3_Click(object sender, EventArgs e)  // add new slide
        {
            //                      |
            //                      |  Because number anyway will be updated in refresh() - so "mb delete"
            //                     \ /
            //                      v
            Slide slide = new Slide(0, string.Concat("Вопрос №", (slideItems.Count + 1).ToString()));
            test.slides.Add(slide);

            SlideItemUC slideItem = new SlideItemUC(this, slide.question);
            slideItems.Add(slideItem);

            flowLayoutPanel1.Controls.Add(slideItem);

            if (!test.slides.Count.Equals(1))
            {
                int newNumber = test.slides.Count - 1;

                foreach (Answer answer in test.slides[newNumber - 1].answers)
                {
                    if (answer.nextSlideNumber.Equals(-1))
                    {
                        answer.nextSlideNumber = newNumber;
                    }
                }
            }
            
            refresh();
        }

        private void pictureBox1_Click(object sender, EventArgs e)  // swaitch to general setttings
        {
            superOwner.SwitchToGeneral();
        }

        private void EditTestSlidesUC_VisibleChanged(object sender, EventArgs e)
        {
            refresh();
        }
    }
}
