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
using Polls.UserControls.EditTest;
using Polls.UserControls.PassingTest;

namespace Polls.UserControls.MainMenu
{
    public partial class UserTestsUC : MainMenuUC, ITestCardItemSubscriber
    {
        private Font fontInActive { get { return new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 204); } }
        private Font fontActive { get { return new Font("Microsoft Sans Serif", 8.75F, FontStyle.Underline, GraphicsUnit.Point, 204); } }

        // Cached
        private string lastResponseCreated  = "";
        private string lastResponsePassed   = "";
        private string lastResponseFavorite = "";

        private List<TestCardItemUC> testCardItemsCreated  = new List<TestCardItemUC>();
        private List<TestCardItemUC> testCardItemsPassed   = new List<TestCardItemUC>();
        private List<TestCardItemUC> testCardItemsFavorite = new List<TestCardItemUC>();

        private List<TestCardItemUC> activeCardItems;

        private List<TestCard> testCardsCreated  = new List<TestCard>();
        private List<TestCard> testCardsPassed   = new List<TestCard>();
        private List<TestCard> testCardsFavorite = new List<TestCard>();

        private List<TestCard> activeCards;

        private int activeTab;

        private delegate string getTestsDelegate();

        public UserTestsUC(MainForm owner) : base(owner)
        {
            InitializeComponent();
            bottomNavigation.setTestsActive();
            label4_Click(this, null);
        }

        public override void RefreshUC()
        {
            showCards();
        }

        private void label4_Click(object sender, EventArgs e)   // created
        {
            setCreatedActive();
            activeTab = 1;
            showCards();
        }

        private void label5_Click(object sender, EventArgs e)   // passed
        {
            setPassedActive();
            activeTab = 2;
            showCards();
        }

        private void label6_Click(object sender, EventArgs e)   // favorite
        {
            setFavoriteActive();
            activeTab = 3;
            showCards();
        }

        private void showCards()
        {

            getTestsDelegate getTests = null;
            string lastResponse = "";
            string fieldName = "testCards";

            switch (activeTab)
            {
                case 1:
                    activeCards = testCardsCreated;
                    activeCardItems = testCardItemsCreated;
                    getTests = ApiRequests.CreatedTestsGet;
                    lastResponse = lastResponseCreated;
                    fieldName = "personalTestCards";
                    break;
                case 2:
                    activeCards = testCardsPassed;
                    activeCardItems = testCardItemsPassed;
                    getTests = ApiRequests.PassedTestsGet;
                    lastResponse = lastResponsePassed;
                    break;
                case 3:
                    activeCards = testCardsFavorite;
                    activeCardItems = testCardItemsFavorite;
                    getTests = ApiRequests.FavoriteTestsGet;
                    lastResponse = lastResponseFavorite;
                    break;
            }

            string responseJson = getTests();

            if(Parser.ResultParse(responseJson))
            {
                if (!lastResponse.Equals(responseJson))  // update cache
                {
                    lastResponse = responseJson;

                    switch (activeTab)
                    {
                        case 1:
                            lastResponseCreated = lastResponse;
                            break;
                        case 2:
                            lastResponsePassed = lastResponse;
                            break;
                        case 3:
                            lastResponseFavorite = lastResponse;
                            break;
                    }

                    activeCards.Clear();
                    activeCards.AddRange(Parser.FieldParse<List<TestCard>>(responseJson, fieldName));


                    activeCardItems.Clear();
                    foreach (TestCard testCard in activeCards)
                    {
                        var item = new TestCardItemUC(testCard, this);
                        if (activeTab.Equals(3))
                            item.switchFavorite();
                        if (activeTab.Equals(1) && !testCard.isPublished)
                            item.unPublished();
                        activeCardItems.Add(item);
                    }
                }

                flowLayoutPanel1.Controls.Clear();
                flowLayoutPanel1.Controls.AddRange(activeCardItems.ToArray());

                if (flowLayoutPanel1.Controls.Count.Equals(0))
                {
                    flowLayoutPanel1.AutoScroll = false;
                }
                else
                {
                    flowLayoutPanel1.AutoScroll = true;
                }

                flowLayoutPanel1.Focus();
            }
            else
            {
                MessageBox.Show("Во время получения тестов произошла ошибка", "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void setCreatedActive()
        {
            labelCreated.Font = fontActive;
            labelPassed.Font = labelFavorite.Font = fontInActive;
        }

        private void setPassedActive()
        {
            labelPassed.Font = fontActive;
            labelCreated.Font = labelFavorite.Font = fontInActive;
        }

        private void setFavoriteActive()
        {
            labelFavorite.Font = fontActive;
            labelPassed.Font = labelCreated.Font = fontInActive;
        }

        private TestCard getTestCardByItem(TestCardItemUC testCardItem)
        {
            return activeCards[activeCardItems.IndexOf(testCardItem)];
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

        public void DeleteClickHandler(TestCardItemUC sender)
        {
            if (MessageBox.Show("Вы собираетесь удалить этот тест навсегда. Продолжить?",
                "Внимание", MessageBoxButtons.OKCancel).Equals(DialogResult.OK))
            {
                string responseJson = ApiRequests.TestDelete(getTestCardByItem(sender).testID);

                if (Parser.ResultParse(responseJson))
                {
                    flowLayoutPanel1.Controls.Remove(sender);
                    activeCards.Remove(getTestCardByItem(sender));
                    activeCardItems.Remove(sender);
                }
                else
                {
                    MessageBox.Show("Что-то пошло не так", "Ошибка", MessageBoxButtons.OK);
                }
            }
        }

        public void EditClickHandler(TestCardItemUC sender)
        {
            Owner.addUC(new EditTestUC(Owner, getTestCardByItem(sender).testID));
        }

        public void StatisticsClickHandler(TestCardItemUC sender)
        {
            Owner.addUC(new StatisticsUC(Owner, getTestCardByItem(sender).testID));
        }

        public override void changeToUserTests() { }

        public void HeaderClickHandler(TestCardItemUC sender)
        {
            if (!activeTab.Equals(1) || getTestCardByItem(sender).isPublished)
            {
                Owner.addUC(new PassingTestUC(Owner, getTestCardByItem(sender).testID));
            }
        }
    }
}
