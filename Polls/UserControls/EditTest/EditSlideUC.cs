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
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;

namespace Polls.UserControls.EditTest
{
    public partial class EditSlideUC : OwnedUserControl
    {
        private Test test;
        private Slide slide;

        private List<AnswerItemUC> answerList = new List<AnswerItemUC>();
        private AnswerAddItemUC answerAdd = new AnswerAddItemUC();
        private AnswerItemUC answerCustomItem = new AnswerItemUC();
        private Answer answerCustom = null;
        private AnswerItemUC answerTextItem = new AnswerItemUC();
        private Answer answerText = null;

        private bool doRefresh = false;

        private List<Slide> availableSlides = new List<Slide>();

        public EditSlideUC(Test test, Slide slide, MainForm owner) : base(owner)
        {
            InitializeComponent();
            this.test = test;
            this.slide = slide;
            Owner = owner;

            updatePhoto();

            // Setting up link for answerCustom obj
            //foreach (Answer answer in slide.answers)
            //{
            //    if (answer.isCustom)
            //    {
            //        answerCustom = answer;
            //        break;
            //    }
            //}

            //if (answerCustom.Equals(null))
            //{
            //    answerCustom = new Answer(0, true);
            //}

            // -Init answers-
            updateAvailableSlides();

            bool cust = true;

            // Init text answer
            if (slide.answerType.Equals(AnswerType.text) && (!slide.answers.Count.Equals(0)))
            {
                answerText = slide.answers[0];
            }
            else
            {
                answerText = new Answer(0);
                answerText.value = "Плейсхолдер для респондента";
            }
            answerTextItem.GetTextBox1().Text = answerText.value;
            answerTextItem.setNextSlides(availableSlides, answerText.nextSlideNumber);

            if ((!slide.answerType.Equals(AnswerType.text)) && (!slide.answers.Count.Equals(0)))
            {
                foreach (Answer answer in slide.answers)
                {
                    if (!answer.isCustom)
                    {
                        AnswerItemUC answerItem = new AnswerItemUC();
                        answerItem.GetTextBox1().Text = answer.value;
                        answerItem.setNextSlides(availableSlides, answer.nextSlideNumber);
                        answerItem.nextSlideEvent += answerNextSlideEdit;
                        answerItem.deleteItemEvent += answerDelete;
                        answerList.Add(answerItem);
                        flowLayoutPanel1.Controls.Add(answerItem);
                    }
                    else
                    {
                        cust = false;
                        answerCustom = answer;
                    }
                }
            }
            else
            {
                AnswerAdd_Click(null, null);
                AnswerAdd_Click(null, null);

                answerList[0].setActiveVariant(answerTextItem.getActiveVariantNumber());
                answerList[1].setActiveVariant(answerTextItem.getActiveVariantNumber());
            }

            if (cust)
            {
                answerCustom = new Answer(answerList.Count, true);
                //answerCustom.value = "Другое";
            }
            answerCustomItem.setNextSlides(availableSlides, answerCustom.nextSlideNumber);
            answerCustomItem.nextSlideEvent += answerNextSlideEdit;
            textBox2.Text = answerCustom.value;

            // Setting text and custom controls
            answerCustomItem.GetTextBox1().Enabled = false;
            answerCustomItem.GetTextBox1().Text = "Другое";
            answerCustomItem.setDeletable(false);
            //answerTextItem.GetTextBox1().Enabled = false;
            //answerTextItem.GetTextBox1().Text = "Плейсхолдер";
            answerTextItem.setDeletable(false);

            //if (slide.answers.Count.Equals(0))
            //    if (slide.answerType.Equals(AnswerType.text))
            //    {
            //        answerText = new Answer(0); // not reachable (cause the slide is new => type == one)
            //    }
            //    else
            //    {
            //        AnswerAdd_Click(null, null);
            //        AnswerAdd_Click(null, null);
            //    }
            //else if (slide.answerType.Equals(AnswerType.text))
            //{
            //    answerText = slide.answers[0];
            //}
            //    else
            //    {
            //        foreach (Answer answer in slide.answers)
            //        {
            //            AnswerItemUC answerItem = new AnswerItemUC();
            //            answerItem.GetTextBox1().Text = answer.value;
            //            answerItem.setNextSlides(availableSlides, answer.nextSlideNumber);
            //            answerList.Add(answerItem);
            //            flowLayoutPanel1.Controls.Add(answerItem);
            //        }
            //}


            // Setting up the dropDown for answerType
            comboBox1.Items.Clear();
            foreach (AnswerType name in Enum.GetValues(typeof(AnswerType)))
            {
                comboBox1.Items.Add(enumToString(name));
            }
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox1.SelectedItem = enumToString(slide.answerType);

            //Simplier
            answerAdd.Click += AnswerAdd_Click;

            foreach (Control control in answerAdd.Controls)
            {
                control.Click += AnswerAdd_Click;
            }

            textBox1.Text = slide.question;

            // Prepare next slides list
            doRefresh = true;
            refresh();
        }

        private void refresh()
        {
            // Default visibility
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;

            flowLayoutPanel1.Controls.Remove(answerAdd);
            flowLayoutPanel1.Controls.Remove(answerCustomItem);

            // Refreshing all answer items (in case of checkbox or radiobutton)
            if (!comboBox1.SelectedItem.Equals(enumToString(AnswerType.text)))   
            {
                // Walk through all items
                for (int i = 0; i < answerList.Count; ++i)
                {
                    //answerList[i].GetTextBox1().Text = slide.answers[i].value;
                    answerList[i].setDeletable(true);

                    //if (slide.answers[i].nextSlideNumber.Equals(-1))
                    //    answerList[i].setNextSlides(availableSlides, -1);
                    //else
                    //    answerList[i].setNextSlides(availableSlides,
                    //        availableSlides.IndexOf(test.slides[slide.answers[i].nextSlideNumber]));
                }

                // Adding answerCustom if need
                if(slide.isPermittedCustom)
                {
                    flowLayoutPanel1.Controls.Add(answerCustomItem);
                }

                // Removing delete icons if need
                if (answerList.Count.Equals(2))
                {
                    answerList[0].setDeletable(false);
                    answerList[1].setDeletable(false);
                }

                // Adding answerAdd if need
                if (!answerList.Count.Equals(10))
                {
                    flowLayoutPanel1.Controls.Add(answerAdd);
                }
            }
            // Case of text answer
            else
            {
                slide.isPermittedCustom = false;
                checkBox2.Enabled = false;
            }

            // Check for fork
            if (isFork())
            {
                slide.isOptional = false;
                checkBox1.Enabled = false;
            }

            // Sync with model
            checkBox1.Checked = slide.isOptional;
            checkBox2.Checked = slide.isPermittedCustom;

            flowLayoutPanel1.Focus();
        }

        public bool isFork()
        {
            if (comboBox1.SelectedItem.Equals(enumToString(AnswerType.text)))
                return false;

            int next = answerList[0].getActiveVariantNumber();
            foreach (AnswerItemUC ans in answerList)
            {
                if (!ans.getActiveVariantNumber().Equals(next))
                    return true;
            }

            if(checkBox2.Checked && (!answerCustomItem.getActiveVariantNumber().Equals(next)))
                return true;

            return false;
        }

        public string enumToString(AnswerType answer)
        {
            switch(answer)
            {
                case AnswerType.one:
                    return "Один ответ";
                case AnswerType.many:
                    return "Несколько ответов";
                default:
                    return "Свободная форма";
            }
        }

        public void updateAvailableSlides()
        {
            List<int> forbidden = new List<int> { slide.slideNumber };
            bool anotherLoop = true;

            while (anotherLoop)
            {
                anotherLoop = false;

                foreach (Slide slide in test.slides)
                {
                    if (forbidden.Contains(slide.slideNumber))
                    {
                        continue;
                    }
                    foreach (Answer answer in slide.answers)
                    {
                        if (forbidden.Contains(answer.nextSlideNumber))
                        {
                            forbidden.Add(slide.slideNumber);
                            anotherLoop = true;
                            break;
                        }
                    }
                }
            }

            foreach (Slide slide in test.slides)
            {
                if (!forbidden.Contains(slide.slideNumber))
                    availableSlides.Add(slide);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)  // add/change photo
        {
            OpenFileDialog openImage = new OpenFileDialog();
            openImage.InitialDirectory = @"C:\Users\User\Desktop";
            openImage.Title = "Выберите вспомогательное фото";
            openImage.Filter = "Фото|*.png;*.jpg;*.jpeg";
            if (openImage.ShowDialog() == DialogResult.OK)
            {
                string filePath = openImage.FileName;
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Length < 1024*1024)
                {
                    slide.image = Convert.ToBase64String(File.ReadAllBytes(filePath));
                    updatePhoto();
                }
                else
                {
                    MessageBox.Show("Фото слишком большое", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else
            {
                deletePhoto();
            }

        }

        private void deletePhoto()
        {
            slide.image = null;
            updatePhoto();
        }

        private void updatePhoto()
        {
            if (slide.image == null)
            {
                pictureBox2.Image = Properties.Resources.logo1;
            }
            else
            {
                pictureBox2.Image = Image.FromStream(new MemoryStream(Convert.FromBase64String(slide.image)));
            }
        }

        private void AnswerAdd_Click(object sender, EventArgs e) // add new answer item
        {
            int nextSlide;
            if (!doRefresh)
                nextSlide = -1;
            else
                nextSlide = answerList[answerList.Count - 1].getActiveVariantRealNumber();

            Answer newAnswer = new Answer(answerList.Count);
            newAnswer.value = string.Concat("Вариант №", answerList.Count + 1);
            AnswerItemUC newAnswerItem = new AnswerItemUC();
            newAnswerItem.setNextSlides(availableSlides, nextSlide);
            newAnswerItem.GetTextBox1().Text = newAnswer.value;

            //newAnswerItem.editItemEvent += answerEdit;
            newAnswerItem.deleteItemEvent += answerDelete;
            newAnswerItem.nextSlideEvent += answerNextSlideEdit;
            
            answerList.Add(newAnswerItem);
            flowLayoutPanel1.Controls.Add(newAnswerItem);

            if (doRefresh)
                refresh();
        }

        //private void answerEdit(AnswerItemUC sender)
        //{
        //    int index = answerList.IndexOf(sender);
        //}

        private void answerDelete(AnswerItemUC sender)
        {
            int index = answerList.IndexOf(sender);
            flowLayoutPanel1.Controls.RemoveAt(index);
            answerList.RemoveAt(index);

            refresh();
        }

        private void answerNextSlideEdit(AnswerItemUC sender)
        {
            //int next;
            //if (!sender.GetTextBox1().Text.Equals("<Конец>"))
            //    next = short.Parse(sender.GetTextBox1().Text.Split(':')[0]) - 1;
            //else
            //    next = -1;

            //int index = answerList.IndexOf(sender);
            //if (!index.Equals(-1))
            //    slide.answers[index].nextSlideNumber = next;
            //else if (sender.Equals(answerCustom))
            //    answerCustom.nextSlideNumber = next;
            //else
            //    answerText.nextSlideNumber = next;

            //if (comboBox1.SelectedItem.Equals(enumToString(AnswerType.many)))
            //{
            //    foreach (Answer answer in slide.answers)
            //    {
            //        answer.nextSlideNumber = next;
            //    }

            //    answerCustom.nextSlideNumber = next;
            //}

            if (comboBox1.SelectedItem.Equals(enumToString(AnswerType.many)))
            {
                int next = sender.getActiveVariantNumber();
                foreach (AnswerItemUC answerItem in answerList)
                {
                    answerItem.setActiveVariant(next);
                }
                answerCustomItem.setActiveVariant(next);
            }

            refresh();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) // answer type has changed
        {
            string item = (string) comboBox1.SelectedItem;

            if (comboBox1.SelectedItem.Equals(enumToString(AnswerType.text)))
            {
                slide.answerType = AnswerType.text;
                answerTextItem.setActiveVariant(answerList[0].getActiveVariantNumber());
                flowLayoutPanel1.Controls.Clear();
                flowLayoutPanel1.Controls.Add(answerTextItem);
            }
            else if (comboBox1.SelectedItem.Equals(enumToString(AnswerType.one)))
            {
                slide.answerType = AnswerType.one;
                if (flowLayoutPanel1.Controls.Contains(answerTextItem))
                {
                    flowLayoutPanel1.Controls.Clear();
                    flowLayoutPanel1.Controls.AddRange(answerList.ToArray());
                }
            }
            else if (comboBox1.SelectedItem.Equals(enumToString(AnswerType.many)))
            {
                slide.answerType = AnswerType.many;
                if (flowLayoutPanel1.Controls.Contains(answerTextItem))
                {
                    flowLayoutPanel1.Controls.Clear();
                    flowLayoutPanel1.Controls.AddRange(answerList.ToArray());
                }
                else if (isFork())
                {
                    int next = answerList[0].getActiveVariantNumber();
                    foreach (AnswerItemUC answerItem in answerList)
                    {
                        answerItem.setActiveVariant(next);
                    }
                    answerCustomItem.setActiveVariant(next);
                }
            }

            refresh();
        }

        //private void EditSlideUC_VisibleChanged(object sender, EventArgs e) // mb remove
        //{
        //    updateAvailableSlides();
        //}

        public void updateSlide()   // sync slide data (question, answers and refresh) from this (EditSlide) UC
        {
            refresh();

            slide.question = textBox1.Text;

            slide.answers.Clear();

            int i;
            if (!comboBox1.SelectedItem.Equals(enumToString(AnswerType.text)))
            {
                for (i = 0; i < answerList.Count; ++i)
                {
                    slide.answers.Add(new Answer(i));

                    slide.answers[i].nextSlideNumber = answerList[i].getActiveVariantRealNumber();

                    slide.answers[i].value = answerList[i].GetTextBox1().Text;
                }
                if (slide.isPermittedCustom)
                {
                    //slide.answers.Add(new Answer(i, true));

                    //slide.answers[i].nextSlideNumber = answerList[i].getActiveVariantRealNumber();

                    //slide.answers[i].value = textBox2.Text;

                    answerCustom.answerNumber = i;
                    answerCustom.nextSlideNumber = answerCustomItem.getActiveVariantRealNumber();
                    answerCustom.value = textBox2.Text;
                    slide.answers.Add(answerCustom);
                }
            }
            else
            {
                //slide.answers.Add(new Answer(0));
                //slide.answers[0].nextSlideNumber = answerList[0].getActiveVariantRealNumber();

                answerText.value = answerTextItem.GetTextBox1().Text;
                answerText.nextSlideNumber = answerTextItem.getActiveVariantRealNumber();
                slide.answers.Add(answerText);
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)   // isOptional has changed
        {
            slide.isOptional = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)   // isPermittedCustom has changed
        {
            slide.isPermittedCustom = checkBox2.Checked;

            if (checkBox2.Checked)
            {
                answerCustomItem.setActiveVariant(answerList[answerList.Count - 1].getActiveVariantNumber());
            }

            // Turn off setting custom answer's value
            if (!checkBox2.Checked)
            {
                label1.Visible = false;
                textBox2.Visible = false;
            }
            else
            {
                label1.Visible = true;
                textBox2.Visible = true;
            }

            //if (checkBox2.Checked)
            //{
            //    slide.answers.Add(answerCustom);
            //}
            //else
            //{
            //    slide.answers.Remove(answerCustom);
            //}

            refresh();  // mb infinite recurion because of changing to the same state
        }

        private void textBox2_TextChanged(object sender, EventArgs e)   // Custom value has changed
        {
            if (textBox2.Text.Equals(""))
                textBox2.Text = "Другое";

            //answerCustom.value = textBox2.Text;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            updateSlide();
            Owner.popUC();
        }
    }
}
