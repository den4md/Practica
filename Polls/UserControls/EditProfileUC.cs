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

namespace Polls.UserControls
{
    public partial class EditProfileUC : OwnedUserControl
    {
        public EditProfileUC(MainForm owner, string email) : base(owner)
        {
            InitializeComponent();
            textBox2.GotFocus += GotFocus1Handler;
            textBox2.LostFocus += LostFocus1Handler;
            LostFocus1Handler(null, null);
            textBox3.GotFocus += GotFocus2Handler;
            textBox3.LostFocus += LostFocus2Handler;
            LostFocus2Handler(null, null);

            label3.Text = email;
        }

        private bool isPass1TextBoxEmpty = true;
        private bool gotFocus1Work = false;

        public void GotFocus1Handler(object sender, EventArgs e)
        {
            if (isPass1TextBoxEmpty)
            {
                gotFocus1Work = true;
                isPass1TextBoxEmpty = false;
                textBox2.ForeColor = SystemColors.WindowText;
                textBox2.Text = "";
                textBox2.UseSystemPasswordChar = true;
                gotFocus1Work = false;
            }
        }

        public void LostFocus1Handler(object sender, EventArgs e)
        {
            if (!gotFocus1Work & textBox2.Text.Equals(""))
            {
                textBox2.ForeColor = SystemColors.InactiveCaptionText;
                isPass1TextBoxEmpty = true;
                textBox2.Text = "Текущий пароль";
                textBox2.UseSystemPasswordChar = false;
            }
        }

        private bool isPass2TextBoxEmpty = true;
        private bool gotFocus2Work = false;

        public void GotFocus2Handler(object sender, EventArgs e)
        {
            if (!gotFocus2Work & isPass2TextBoxEmpty)
            {
                gotFocus2Work = true;
                isPass2TextBoxEmpty = false;
                textBox3.ForeColor = SystemColors.WindowText;
                textBox3.Text = "";
                textBox3.UseSystemPasswordChar = true;
                gotFocus2Work = false;
            }
        }

        public void LostFocus2Handler(object sender, EventArgs e)
        {
            if (!gotFocus2Work & textBox3.Text.Equals(""))
            {
                textBox3.ForeColor = SystemColors.InactiveCaptionText;
                textBox3.Text = "Текущий пароль";
                textBox3.UseSystemPasswordChar = false;
                isPass2TextBoxEmpty = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Owner.popUC();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string validateResult = validateEmailChange();
            if (!validateResult.Equals(""))
            {
                Error(validateResult, closeApp: false);
                clearTextBox();
                return;
            }

            bool requestResult = Parser.ResultParse(ApiRequests.ProfileEmailPut
                (MD5Handler.GetMd5Hash(textBox2.Text), textBox1.Text));

            if (requestResult)
            {
                Owner.popUC();
                Owner.refresh();
            }
            else
            {
                Error("Данные введены неправильно или такой email уже занят", closeApp:false);
                clearTextBox();
            }
        }

        private string validateEmailChange()
        {
            if (textBox1.Text.Equals(""))
                return "Email обязателен";
            if (!isValidEmail(textBox1.Text))
                return "Введите валидный email";
            if (textBox1.Text.Equals(label3.Text))
                return "Новый email идентичен текущему";
            if (textBox2.Text.Equals("") | isPass1TextBoxEmpty)
                return "Текущий пароль обязателен";

            return "";
        }

        private bool isValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string validateResult = validatePasswordChange();
            if (!validateResult.Equals(""))
            {
                Error(validateResult, closeApp: false);
                clearTextBox();
                return;
            }

            bool requestResult = Parser.ResultParse(ApiRequests.ProfilePasswordPut
                (MD5Handler.GetMd5Hash(textBox3.Text), MD5Handler.GetMd5Hash(textBox4.Text)));

            if (requestResult)
            {
                Owner.popUC();
            }
            else
            {
                Error("Данные введены неправильно", closeApp: false);
                clearTextBox();
            }
        }

        private string validatePasswordChange()
        {
            if (textBox4.Text.Equals(""))
                return "Новый пароль обязателен";
            if (textBox5.Text.Equals(""))
                return "Повтор пароля обязателен";
            if (!textBox4.Text.Equals(textBox5.Text))
                return "Новые пароли не совпадают";
            if (textBox3.Text.Equals("") | isPass2TextBoxEmpty)
                return "Текущий пароль обязателен";

            return "";
        }

        private void clearTextBox()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

            LostFocus1Handler(null, null);
            LostFocus2Handler(null, null);
        }
    }
}
