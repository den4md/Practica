using System.Windows.Forms;
using System.ComponentModel.DataAnnotations;
using Polls;
using System.Threading.Tasks;
using System;

namespace Polls.UserControls
{
    public partial class RegistrationUC : OwnedUserControl
    {
        public RegistrationUC(MainForm owner) : base(owner)
        {
            InitializeComponent();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            changeToLogin();
        }

        private async void submitButton_Click(object sender, System.EventArgs e)
        {
            string validateResult = validate();
            if (!validateResult.Equals(""))
            {
                ErrorForm errorForm = new ErrorForm(validateResult, "Ошибка валидации", false);
                errorForm.ShowDialog(Owner);
            }
            else
            {
                try
                {
                    string resultString = await Task.Run(() => ApiRequests.RegistrationPost(loginTextBox.Text,
                       emailTextBox.Text, MD5Handler.GetMd5Hash(passwordTextBox.Text)));
                    bool result = Parser.ResultParse(resultString);
                    if (result)
                    {
                        //добавить что-то дополнительное об успешности
                        changeToLogin();
                    }
                    else
                    {
                        Error("Регистрация не удалась", closeApp:false);
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc);
                    Error("Не удалось выполнить запрос на регистрацию.", closeApp:false);
                }

            }
        }

        private string validate()
        {
            if (loginTextBox.Text.Equals(""))
                return "Логин обязателен";
            if (emailTextBox.Text.Equals(""))
                return "Email обязателен";
            if (!isValidEmail(emailTextBox.Text))
                return "Введите валидный email";
            if (passwordTextBox.Text.Equals(""))
                return "Пароль обязателен";
            if (passwordRepeatTextBox.Text.Equals(""))
                return "Повторите пароль";
            if (!passwordTextBox.Text.Equals(passwordRepeatTextBox.Text))
                return "Пароли не совпадают";

            return "";
        }

        private bool isValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        private void changeToLogin()
        {
            Owner.popUC();
        }
    }
}
