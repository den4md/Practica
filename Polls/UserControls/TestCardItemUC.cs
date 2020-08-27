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

namespace Polls.UserControls
{
    public delegate void TestCardItemAction(TestCardItemUC sender);

    public partial class TestCardItemUC : UserControl
    {
        public event TestCardItemAction AuthorClick;
        public event TestCardItemAction FavouriteClick;
        public event TestCardItemAction DeleteClick;
        public event TestCardItemAction EditClick;
        public event TestCardItemAction StatisticsClick;
        public event TestCardItemAction HeaderClick;

        private string url;

        private string testID;
        private bool isActive;

        public bool isFavorite { set; get; } = false;

        public TestCardItemUC(TestCard testCard, ITestCardItemSubscriber subsciber)
        {
            InitializeComponent();
            Init(testCard);
            SubscribeToEvents(subsciber);

            url = testCard.url;
            testID = testCard.testID;
            isActive = testCard.isActive;
        }

        private void Init(TestCard testCard)
        {
            label1.Text = testCard.name;
            label2.Text = testCard.description;
            pictureBox2.Width = (int)(testCard.rating * 20);
            if (testCard.rating.Equals(0))
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
                label5.Visible = true;
            }
            toolTip1.SetToolTip(pictureBox2, string.Concat("Оценка: ", testCard.rating.ToString()));

            if (testCard.author == null)    // it's test of current user
            {
                linkLabel1.Visible = false;
                if (testCard.isPublished)
                {
                    pictureBox6.Visible = false;

                    if (!testCard.isActive)
                    {
                        pictureBox7.Image = Properties.Resources.active_off;
                        toolTip1.SetToolTip(pictureBox7, "Сделать тест активным");
                    }
                    else
                    {
                        toolTip1.SetToolTip(pictureBox7, "Сделать тест неактивным");
                    }
                }
                else
                {
                    pictureBox7.Visible = false;
                    pictureBox8.Visible = false;
                }
            }
            else                            // it's test of other user
            {
                linkLabel1.Text = testCard.author.login;
                pictureBox5.Visible = false;    // delete
                pictureBox6.Visible = false;    // edit
                pictureBox7.Visible = false;    // visibility 
                pictureBox8.Visible = false;    // statistics
            }

            // tags
            if (testCard.tagNames == null || testCard.tagNames.Count.Equals(0))
            {
                label3.Visible = false;
            }
            else
            {
                foreach (string tag in testCard.tagNames)   // not optimal
                {
                    Label label = new Label();
                    label.AutoSize = true;
                    label.Text = tag;
                    flowLayoutPanel1.Controls.Add(label);
                }
            }
        }

        internal void resetActions()    // no author, editing, analitics & ...
        {
            linkLabel1.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
            pictureBox7.Visible = false;
            pictureBox8.Visible = false;
        }

        internal void switchFavorite()
        {
            isFavorite = !isFavorite;
            if (isFavorite)
            {
                pictureBox3.Image = Properties.Resources.fav_filled;
            }
            else
            {
                pictureBox3.Image = Properties.Resources.fav_outlined;
            }
        }

        public void SubscribeToEvents(ITestCardItemSubscriber subscriber)
        {
            AuthorClick += subscriber.AuthorClickHandler;
            FavouriteClick += subscriber.FavouriteClickHandler;
            DeleteClick += subscriber.DeleteClickHandler;
            EditClick += subscriber.EditClickHandler;
            StatisticsClick += subscriber.StatisticsClickHandler;
            HeaderClick += subscriber.HeaderClickHandler;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) // Open author profile
        {
            AuthorClick?.Invoke(this);
        }

        private void pictureBox3_Click(object sender, EventArgs e)  // Add to/Remove from favourites
        {
            FavouriteClick?.Invoke(this);
        }

        internal void unPublished()
        {
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)  // Copy link
        {
            if (url != null)
                Clipboard.SetText(url, TextDataFormat.UnicodeText);
        }

        private void pictureBox5_Click(object sender, EventArgs e)  // Delete test
        {
            DeleteClick?.Invoke(this);
        }

        private void pictureBox6_Click(object sender, EventArgs e)  // Edit test
        {
            EditClick?.Invoke(this);
        }

        private void pictureBox7_Click(object sender, EventArgs e)  // Change test activity
        {
            string responseJson = ApiRequests.TestActivityPut(testID, !isActive);
            if (Parser.ResultParse(responseJson))
            {
                isActive = !isActive;
                if (!isActive)
                {
                    pictureBox7.Image = Properties.Resources.active_off;
                    toolTip1.SetToolTip(pictureBox7, "Сделать тест активным");
                }
                else
                {
                    pictureBox7.Image = Properties.Resources.active_on;
                    toolTip1.SetToolTip(pictureBox7, "Сделать тест неактивным");
                }
            }
            else
            {
                MessageBox.Show("Что-то пошло не так", "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)  // Open test statistics
        {
            StatisticsClick?.Invoke(this);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            HeaderClick?.Invoke(this);
        }
    }
}
