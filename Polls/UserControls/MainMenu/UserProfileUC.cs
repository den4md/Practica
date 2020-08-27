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
using Polls.Models;
using Polls.UserControls.EditTest;
using Polls.UserControls.PassingTest;

namespace Polls.UserControls.MainMenu
{
    public partial class UserProfileUC : MainMenuUC, ITestCardItemSubscriber
    {
        private List<TestCard> testCards;

        public UserProfileUC(MainForm owner, string userID = "") : base(owner)
        {
            InitializeComponent();
            bottomNavigation.setProfileActive();

            if (userID.Equals(""))
            {
                pictureBox1.Visible = false;
                profilePictureBox.Location = new Point(7, profilePictureBox.Location.Y);
            }
            else
            {
                editPictureBox.Visible = false;
                logoutPictureBox.Visible = false;
                bottomNavigation.Visible = false;
                flowLayoutPanel1.Size = new Size(flowLayoutPanel1.Size.Width,
                    flowLayoutPanel1.Size.Height + 20);
            }
            string requestResult = ApiRequests.ProfileGet(userID);
            if (Parser.ResultParse(requestResult))
            {
                object emailObject = Parser.FieldParse<object>(requestResult, "email");
                if (emailObject == null)
                {
                    email.Text = "<ЗАКРЫТ>";
                }
                else
                {
                    email.Text = (string)emailObject;
                }
                loginLabel.Text = Parser.FieldParse<string>(requestResult, "login");

                testCards = Parser.FieldParse<List<TestCard>>(requestResult, "createdTests");

                foreach (TestCard testCard in testCards)
                {
                    flowLayoutPanel1.Controls.Add(new TestCardItemUC(testCard, this));
                }

                foreach (Control control in flowLayoutPanel1.Controls)
                {
                    if (control is TestCardItemUC)
                    {
                        ((TestCardItemUC)control).resetActions();
                    }
                }
            }
            else
            {
                Error("Ошибка при получении профиля", closeApp: false);
                Owner.changeUC(new MainMenuUC(Owner));
            }
        }

        private TestCard getTestCardByItem(TestCardItemUC item)
        {
            return testCards[flowLayoutPanel1.Controls.IndexOf(item)];
        }

        public void AuthorClickHandler(TestCardItemUC sender) { /*pass*/}

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

        public void EditClickHandler(TestCardItemUC sender)
        {
            Owner.addUC(new EditTestUC(Owner, getTestCardByItem(sender).testID));
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

        public void StatisticsClickHandler(TestCardItemUC sender)
        {
            throw new NotImplementedException();
        }

        private void changeToLogin()
        {
            Owner.popUC();
        }

        private void editPictureBox_Click(object sender, EventArgs e)
        {
            Owner.addUC(new EditProfileUC(Owner, email.Text));
        }

        private async void logoutPictureBox_Click(object sender, EventArgs e)
        {
            string resultString = await Task.Run(() => ApiRequests.LogoutPost());
            bool result = Parser.ResultParse(resultString);
            if (result)
            {
                //добавить что-то дополнительное об успешности
                Memory.isAuth = false;
                Settings.SaveSettingsCrypt();
                changeToLogin();
            }
            else
            {
                Error("Выйти не удалось", closeApp: false);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Owner.popUC();
        }

        public override void changeToProfile() { }

        public void HeaderClickHandler(TestCardItemUC sender)
        {
            Owner.addUC(new PassingTestUC(Owner, getTestCardByItem(sender).testID));
        }
    }
}
