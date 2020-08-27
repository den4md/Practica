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
using Polls.UserControls.PassingTest;

namespace Polls.UserControls.MainMenu
{
    public partial class SearchUC : MainMenuUC, ITestCardItemSubscriber
    {
        private List<TestCard> testCards;

        public SearchUC(MainForm owner) : base(owner)
        {
            InitializeComponent();
            bottomNavigation.setSearchActive();
        }

        public override void changeToSearch() { }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(""))
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (!button1.Enabled)
            //    return;

            string responseJson = ApiRequests.SearchTestsGet(textBox1.Text);

            if (Parser.ResultParse(responseJson))
            {
                flowLayoutPanel1.Controls.Clear();
                testCards = Parser.FieldParse<List<TestCard>>(responseJson, "testCards");
                foreach (TestCard testCard in testCards)
                {
                    flowLayoutPanel1.Controls.Add(new TestCardItemUC(testCard, this));
                }
            }
            else
            {
                MessageBox.Show("Что-то с поиском пошло не так",
                    "Ошибка", MessageBoxButtons.OK);
            }
        }

        private TestCard getTestCardByItem(TestCardItemUC testCardItem)
        {
            return testCards[flowLayoutPanel1.Controls.IndexOf(testCardItem)];
        }

        public void AuthorClickHandler(TestCardItemUC sender)
        {
            Owner.addUC(new UserProfileUC(Owner, getTestCardByItem(sender).author.userID));
        }

        public void FavouriteClickHandler(TestCardItemUC sender)
        {
            string responseJson;
            if (sender.isFavorite)
            {
                responseJson = ApiRequests.TestFavoriteDelete(getTestCardByItem(sender).testID);
            }
            else
            {
                responseJson = ApiRequests.TestFavoritePost(getTestCardByItem(sender).testID);
            }

            if (Parser.ResultParse(responseJson))
            {
                sender.switchFavorite();
            }
            else
            {
                MessageBox.Show("Что-то пошло не так", "Ошибка", MessageBoxButtons.OK);
            }
        }

        public void DeleteClickHandler(TestCardItemUC sender) { }

        public void EditClickHandler(TestCardItemUC sender) { }

        public void StatisticsClickHandler(TestCardItemUC sender) { }

        public void HeaderClickHandler(TestCardItemUC sender)
        {
            Owner.addUC(new PassingTestUC(Owner, getTestCardByItem(sender).testID));
        }
    }
}
