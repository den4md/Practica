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
    public partial class EditTestMainUC : UserControl
    {
        private EditTestUC superOwner;
        private Test test;

        private List<TextBox> tagList = new List<TextBox>();

        public EditTestMainUC(Test test)
        {
            this.test = test;
            InitializeComponent();
            VisibleChanged += EditTestMainUC_VisibleChanged;

            tagList.Add(textBox3);
            tagList.Add(textBox4);
            tagList.Add(textBox5);
            tagList.Add(textBox6);
            tagList.Add(textBox7);
        }

        private void EditTestMainUC_VisibleChanged(object sender, EventArgs e)
        {
            textBox1.Text = test.name;
            textBox2.Text = test.description;
            checkBox1.Checked = test.isAnonym;
            checkBox2.Checked = test.isPrivate;
            checkBox3.Checked = test.isRepeatable;
            numericUpDown1.Value = ushort.Parse((test.completingLimit.Split(':'))[0]);
            numericUpDown2.Value = ushort.Parse((test.completingLimit.Split(':'))[1]);

            for (int i = 0; i < test.tagNames.Count; ++i)
            {
                tagList[i].Text = test.tagNames[i];
                tagList[i].Visible = true;
            }
            checkTags();
        }

        private void checkTags(object sender = null, EventArgs e = null)
        {
            int firstEmpty = 5;
            for (int i = 0; i < 5; ++i)
            {
                if (tagList[i].Text.Equals("")) // not sure that this is correct
                {
                    if (firstEmpty < i)
                    {
                        tagList[i].Visible = false;
                    }
                    else
                    {
                        firstEmpty = i;
                        tagList[i].Visible = true;
                    }
                }
                else
                {
                    if (firstEmpty < i)
                    {
                        tagList[firstEmpty].Text = tagList[i].Text;
                        tagList[i].Text = "";
                        tagList[firstEmpty].Visible = true;
                        i = firstEmpty + 1;
                        firstEmpty = 5;
                    }
                }
            }
        }

        public void SetSuperOwner(EditTestUC superOwner)
        {
            this.superOwner = superOwner;
        }
        
        public void UpdateTest()
        {
            test.name = textBox1.Text;
            test.description = textBox2.Text;
            test.isAnonym = checkBox1.Checked;
            test.isPrivate = checkBox2.Checked;
            test.isRepeatable = checkBox3.Checked;

            try
            {
                test.SetCompletingTime((int)numericUpDown1.Value, (int)numericUpDown2.Value);
            }
            catch
            {
                MessageBox.Show("Время, отведённое на прохождение должен ыть в пределах от 00:05 до 23:59", "Ошибка", MessageBoxButtons.OK);
                numericUpDown1.Value = 0;
                numericUpDown2.Value = 5;
                test.SetCompletingTime(0, 5);
            }

            test.tagNames.Clear();
            checkTags();
            for (int i = 0; i < 5; ++i)
            {
                if (tagList[i].Text.Equals(""))
                {
                    break;
                }
                test.tagNames.Add(tagList[i].Text);
            }
            }

        private void pictureBox2_Click(object sender, EventArgs e)  // switch to slide list
        {
            superOwner.SwitchToSlideList();
        }
    }
}
