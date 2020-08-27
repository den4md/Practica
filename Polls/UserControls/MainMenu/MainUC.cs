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
using Newtonsoft.Json.Linq;

namespace Polls.UserControls.MainMenu
{
    public partial class MainUC : MainMenuUC, ITestCardItemSubscriber
    {
        private bool isSortDesc = true;
        private string lastResponse = "";
        private List<TestCard> testCards;

        public MainUC(MainForm owner) : base(owner)
        {
            InitializeComponent();
            bottomNavigation.setMainActive();
            refresh();
        }

        private void refresh()
        {
            string respnoseJson = ApiRequests.PublicTestsGet(isSortDesc);

            if (Parser.ResultParse(respnoseJson))
            {
                if (!lastResponse.Equals(respnoseJson))
                {
                    lastResponse = respnoseJson;
                    testCards = Parser.FieldParse<List<TestCard>>(respnoseJson, "testCards");

                    flowLayoutPanel1.Controls.Clear();
                    foreach (TestCard testCard in testCards)
                    {
                        flowLayoutPanel1.Controls.Add(new TestCardItemUC(testCard, this));
                    }
                }
                flowLayoutPanel1.Focus();
            }
            else
            {
                MessageBox.Show("Попытка получить публичные тесты обломилась",
                    "Ошибка", MessageBoxButtons.OK);
            }
        }

        public override void changeToMain() { }

        public override void RefreshUC()
        {
            refresh();
        }

        private void pictureBox1_Click(object sender, EventArgs e)  // Change sort
        {
            isSortDesc = !isSortDesc;
            if (isSortDesc)
            {
                pictureBox1.Image = Properties.Resources.sort;
                toolTip1.SetToolTip(pictureBox1, "Сортировать по возрастанию оценки");
            }
            else
            {
                pictureBox1.Image = Properties.Resources.sort_asc;
                toolTip1.SetToolTip(pictureBox1, "Сортировать по убыванию оценки");
            }
            refresh();
        }

        private void MainUC_VisibleChanged(object sender, EventArgs e)
        {
            refresh();
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

        public void DeleteClickHandler(TestCardItemUC sender)
        {
            if (MessageBox.Show("Вы собираетесь удалить этот тест навсегда. Продолжить?",
                "Внимание", MessageBoxButtons.OKCancel).Equals(DialogResult.OK))
            {
                string responseJson = ApiRequests.TestDelete(getTestCardByItem(sender).testID);

                if (Parser.ResultParse(responseJson))
                {
                    flowLayoutPanel1.Controls.Remove(sender);
                }
                else
                {
                    MessageBox.Show("Что-то пошло не так", "Ошибка", MessageBoxButtons.OK);
                }
            }
        }

        public void EditClickHandler(TestCardItemUC sender) { }

        public void StatisticsClickHandler(TestCardItemUC sender) { }

        public void HeaderClickHandler(TestCardItemUC sender)
        {
            string response = ApiRequests.PassingTestGet(getTestCardByItem(sender).testID);

            if (Parser.ResultParse(response))
            {
                Owner.addUC(new PassingTestUC(Owner, getTestCardByItem(sender).testID));
            }
            else
            {
                MessageBox.Show("Тест не найден или неактивен", "Ошибка", MessageBoxButtons.OK);
            }
        }
    }
}
