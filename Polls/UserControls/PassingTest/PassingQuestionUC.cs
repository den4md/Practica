using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.IO;
using Polls.Models;
using Polls.MyControls;

namespace Polls.UserControls.PassingTest
{
    public partial class PassingQuestionUC : UserControl
    {
        private string testID;
        private IPassingUC passingUC;
        private int slideNumber;
        private bool isOptional;

        private string imageBase64;

        private int leftSeconds;
        private bool isDecrementing = true;

        private List<ICheckable> controls = new List<ICheckable>();   // checkboxes | radiobuttons
        private TextBox customTextBox = null;

        public PassingQuestionUC(string testID, IPassingUC PassingUC, string responseJson)
        {
            this.testID = testID;
            this.passingUC = PassingUC;

            InitializeComponent();

            initUC(responseJson);
        }

        private void initUC(string responseJson)
        {
            initTimer(Parser.FieldParse<string>(responseJson, "timeleft")); // timeLeft - ushhhh

            var slide = JObject.Parse(responseJson)["slide"];

            label3.Text = slide["question"].ToObject<string>();

            slideNumber = slide["slideNumber"].ToObject<int>();
            if (slideNumber.Equals(0))  // it's the first slide
                button1.Visible = false;    // can't move back

            isOptional = slide["isOptional"].ToObject<bool>();
            button2.Enabled = isOptional;

            string image = imageBase64 = slide["image"].ToObject<string>();
            if (image != null && !image.Equals(""))
            {
                putImage(image);
            }
            else
            {
                pictureBox2.Visible = false;
                panel1.Size = new Size(440, panel1.Height);
            }

            label1.Text = $"Вопрос №{(slide["slideOrderNumber"].ToObject<int>() + 1).ToString()}";

            if (slide["answerType"].ToObject<string>().Equals("text"))
            {
                flowLayoutPanel1.Visible = false;
                textBox1.Visible = true;
                textBox1.TextChanged += answerChanged;

                if (slide["customValue"].ToObject<string>() == null)    // VERIFY!
                {
                    textBox1.GotFocus += removePlaceholder;
                    textBox1.Text = slide["answers"].Children().ToList()[0]["value"].ToObject<string>();
                }
                else
                {
                    textBox1.Text = slide["customValue"].ToObject<string>();
                }
            }
            else
            {
                linkLabel1.Visible = true;
                List<Answer> answers = slide["answers"].ToObject<List<Answer>>();

                answers = answers.OrderBy(a => a.answerNumber).ToList();    // asc

                foreach (Answer answer in answers)
                {
                    ICheckable chOrRad;
                    if (slide["answerType"].ToObject<string>().Equals("many"))
                    {
                        chOrRad = new MyCheckBox();
                    }
                    else
                    {
                        chOrRad = new MyRadioButton();
                    }

                    flowLayoutPanel1.Controls.Add((Control)chOrRad);    // Dangerous, but it works
                    controls.Add(chOrRad);
                    chOrRad.setOnCheckedChanged(answerChanged);
                    chOrRad.setChecked(answer.isSelected);


                    if (!answer.isCustom)
                    {
                        chOrRad.setText(answer.value);
                        chOrRad.setSize(new Size(433, 20));
                    }
                    else
                    {
                        chOrRad.setText("");
                        chOrRad.setSize(new Size(13, 20));

                        chOrRad.setOnCheckedChanged(customAnswerChanged);

                        TextBox textBox = new TextBox();
                        textBox.TextChanged += answerChanged;
                        customTextBox = textBox;
                        textBox.Enabled = chOrRad.isChecked();

                        if (slide["customValue"].ToObject<string>() == null)    // verify too
                        {
                            textBox.Text = answer.value;
                            textBox.GotFocus += removePlaceholder;
                        }
                        else
                        {
                            textBox.Text = slide["customValue"].ToObject<string>();
                        }
                        
                        flowLayoutPanel1.Controls.Add(textBox);
                    }
                }
            }
        }

        public void removePlaceholder(object sender, EventArgs e)
        {
            ((Control)sender).GotFocus -= removePlaceholder;
            ((Control)sender).Text = "";
        }

        public void customAnswerChanged(object sender, EventArgs e)
        {
            customTextBox.Enabled = ((ICheckable)sender).isChecked();
        }

        public void answerChanged(object sender = null, EventArgs e = null)
        {
            if (!isOptional)
            {
                if (!textBox1.Visible)
                {
                    int customControlOrderNumber = -1;
                    if (customTextBox != null)   // isPermittedCustom == true
                    {
                        customControlOrderNumber = flowLayoutPanel1.Controls.IndexOf(customTextBox) - 1;
                    }

                    for (int i = 0; i < controls.Count; ++i)
                    {
                        if (i.Equals(customControlOrderNumber))
                        {
                            if (controls[i].isChecked() && !customTextBox.Text.Equals(""))
                            {
                                button2.Enabled = true;
                                return;
                            }
                        }
                        else if (controls[i].isChecked())
                        {
                            button2.Enabled = true;
                            return;
                        }
                    }
                }
                else
                {
                    if (!textBox1.Text.Equals(""))
                    {
                        button2.Enabled = true;
                        return;
                    }
                }

                button2.Enabled = false;
            }
        }

        private void putImage(string imageString)
        {
            Image image = Image.FromStream(new MemoryStream(Convert.FromBase64String(imageString)));
            double ratio = image.Width * 1.0 / image.Height;
            if (ratio < 1)
            {
                pictureBox2.Height = (int) (pictureBox2.Width * image.Height * 1.0 / image.Width);
            }
            else
            {
                pictureBox2.Width = (int)(pictureBox2.Height * image.Width * 1.0 / image.Height);
            }
            pictureBox2.Image = image;
        }

        private async void initTimer(string timeLeft)
        {
            string[] timeLeftArray = timeLeft.Split(':');
            leftSeconds = short.Parse(timeLeftArray[0]) * 3600 + short.Parse(timeLeftArray[1]) * 60
                + short.Parse(timeLeftArray[2]);


            var progress = new Progress<string>(s => label2.Text = s);
            await Task.Factory.StartNew(() => timer(progress),   
                                TaskCreationOptions.LongRunning);

            if (leftSeconds.Equals(0))
            {
                MessageBox.Show("Время, отведённое на прохождения опроса закончилось. Опрос будет закрыт.",
                    "Внимание", MessageBoxButtons.OK);
                passingUC.closeUC();
            }
        }

        private void timer(IProgress<string> progress)
        {
            while (isDecrementing && !leftSeconds.Equals(0))
            {
                decrementTimer(progress);
            }
        }

        private void decrementTimer(IProgress<string> progress)
        {
            updateLeftTime(progress);
            Thread.Sleep(1000);
            --leftSeconds;
        }

        private void updateLeftTime(IProgress<string> progress)
        {
            int hours = leftSeconds / 3600;
            int minutes = (leftSeconds % 3600) / 60;
            int seconds = (leftSeconds % 3600) % 60;

            string z = "0";
            string e = "";
            progress.Report($"Оставшееся время - {((hours < 10) ? z : e)}{hours}:{((minutes < 10) ? z : e)}{minutes}:{((seconds < 10) ? z : e)}{seconds}");
        }

        private void pictureBox1_Click(object sender, EventArgs e)  // exit test
        {
            if (MessageBox.Show("Ответ на текущий вопрос не сохранится. Продолжить?", 
                "Внимание", MessageBoxButtons.OKCancel).Equals(DialogResult.OK))
            {
                isDecrementing = false;
                passingUC.closeUC();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)  //open full image
        {
            passingUC.openImage(imageBase64);
        }

        private void button1_Click(object sender, EventArgs e)  // prev
        {
            string response = ApiRequests.PassingPrevPut(testID);

            if (Parser.ResultParse(response))
            {
                isDecrementing = false;
                passingUC.updateSlide(response);
            }
            else
            {
                MessageBox.Show("Возможно тест уже удалён или неактивен", "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)  // next
        {
            string response = ApiRequests.PassingNextPost(testID, getNextSlideForm());

            if (Parser.ResultParse(response))
            {
                isDecrementing = false;
                passingUC.updateSlide(response);
            }
            else
            {
                MessageBox.Show("Ошибка при отправке ответа", "Ошибка", MessageBoxButtons.OK);
            }
        }

        private NextSlideForm getNextSlideForm()
        {
            NextSlideForm form = new NextSlideForm();
            form.slideNumberSent = slideNumber;

            if (textBox1.Visible)
            {
                form.answerNumbers.Add(0);
                form.value = textBox1.Text;
            }
            else
            {
                for (int i = 0; i < controls.Count; ++i)
                {
                    if (controls[i].isChecked())
                    {
                        form.answerNumbers.Add(i);
                        if (flowLayoutPanel1.Controls.Count >= (i + 2) && flowLayoutPanel1.Controls[i + 1] is TextBox)
                        {
                            form.value = ((TextBox)flowLayoutPanel1.Controls[i + 1]).Text;
                        }
                    }
                }
            }

            return form;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // uncheck all
        {
            foreach (ICheckable control in controls)
            {
                control.setChecked(false);
            }

            answerChanged();
        }
    }
}
