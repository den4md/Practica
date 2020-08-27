using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace Polls.UserControls.PassingTest
{
    public partial class PassingWelcomeUC : UserControl
    {
        private IPassingUC passingUC;
        private string testID;

        public PassingWelcomeUC(string testID, IPassingUC PassingUC, string responseJson)
        {
            this.testID = testID;
            this.passingUC = PassingUC;
            InitializeComponent();

            initContent(responseJson);
        }

        private void initContent(string responseJson)
        {
            JObject slide = Parser.FieldParse<JObject>(responseJson, "slide");

            label1.Text = slide["name"].ToObject<string>();
            label2.Text = slide["description"].ToObject<string>();
            linkLabel1.Text = slide["author"]["login"].ToObject<string>();
            pictureBox2.Width = (int)(20 * slide["rating"].ToObject<float>());
            if (!pictureBox2.Width.Equals(0))
            {
                label3.Visible = false;
            }
            else
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
            }

            foreach (string tag in slide["tagNames"].ToObject<string[]>())
            {
                Label label = new Label();
                label.Text = tag;
                label.AutoSize = true;
                flowLayoutPanel1.Controls.Add(label);
            }
            if (flowLayoutPanel1.Controls.Count.Equals(0))
                label4.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string response = ApiRequests.PassingNextPost(testID, null);

            if (Parser.ResultParse(response))
            {
                passingUC.updateSlide(response);
            }
            else
            {
                MessageBox.Show("Ошибка начала прохождения. Возможно тест удалён или неактивен",
                    "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            passingUC.closeUC();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            passingUC.authorClick();
        }
    }
}
