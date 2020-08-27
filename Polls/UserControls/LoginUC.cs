using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations;
using Polls.UserControls.MainMenu;

namespace Polls.UserControls
{
    public partial class LoginUC : OwnedUserControl
    {
        public LoginUC(MainForm owner) : base(owner)
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            changeToRegistration();
        }

        private void changeToRegistration()
        {
            Owner.addUC(new RegistrationUC(Owner));
        }

        private async void submitButton_Click(object sender, EventArgs e)
        {
            string validateResult = validate();
            if (!validateResult.Equals(""))
            {
                ErrorForm errorForm = new ErrorForm(validateResult, "Ошибка валидации", false);
                errorForm.ShowDialog(Owner);
            }
            else
            {
                string resultString = await Task.Run(() => ApiRequests.LoginPost(emailTextBox.Text,
                    MD5Handler.GetMd5Hash(passwordTextBox.Text)));
                bool result = Parser.ResultParse(resultString);
                if (result)
                {
                    //добавить что-то дополнительное об успешности
                    Memory.isAuth = true;
                    Settings.SaveSettingsCrypt();
                    changeToMain();
                }
                else
                {
                    Error("Авторизация не удалась", closeApp: false);
                }
            }
        }

        private string validate()
        {
            if (emailTextBox.Text.Equals(""))
                return "Email обязателен";
            if (!isValidEmail(emailTextBox.Text))
                return "Введите валидный email";
            if (passwordTextBox.Text.Equals(""))
                return "Пароль обязателен";

            return "";
        }

        private bool isValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        private void changeToMain()
        {
            Owner.addUC(new MainUC(Owner));
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (passwordTextBox != null)
            {
                passwordTextBox.Text = "";  // Clearing password textbox when UC is changed to another
            }
        }
    }
}
