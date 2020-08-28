namespace Polls.UserControls
{
    partial class RegistrationUC
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.headerLabel = new System.Windows.Forms.Label();
            this.emailLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.loginLabel = new System.Windows.Forms.Label();
            this.loginTextBox = new System.Windows.Forms.TextBox();
            this.passwordRepeatLabel = new System.Windows.Forms.Label();
            this.passwordRepeatTextBox = new System.Windows.Forms.TextBox();
            this.submitButton = new System.Windows.Forms.Button();
            this.loginLinkLabel = new System.Windows.Forms.LinkLabel();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // headerLabel
            // 
            this.headerLabel.Font = new System.Drawing.Font("Nominee-Condensed", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.headerLabel.Location = new System.Drawing.Point(0, 29);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(464, 29);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = "Регистрация";
            this.headerLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(129, 123);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(32, 13);
            this.emailLabel.TabIndex = 5;
            this.emailLabel.Text = "Email";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(129, 159);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(45, 13);
            this.passwordLabel.TabIndex = 6;
            this.passwordLabel.Text = "Пароль";
            // 
            // emailTextBox
            // 
            this.emailTextBox.Location = new System.Drawing.Point(197, 120);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(122, 20);
            this.emailTextBox.TabIndex = 2;
            // 
            // loginLabel
            // 
            this.loginLabel.AutoSize = true;
            this.loginLabel.Location = new System.Drawing.Point(129, 85);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(38, 13);
            this.loginLabel.TabIndex = 5;
            this.loginLabel.Text = "Логин";
            // 
            // loginTextBox
            // 
            this.loginTextBox.Location = new System.Drawing.Point(197, 82);
            this.loginTextBox.Name = "loginTextBox";
            this.loginTextBox.Size = new System.Drawing.Size(122, 20);
            this.loginTextBox.TabIndex = 1;
            // 
            // passwordRepeatLabel
            // 
            this.passwordRepeatLabel.Location = new System.Drawing.Point(129, 186);
            this.passwordRepeatLabel.Name = "passwordRepeatLabel";
            this.passwordRepeatLabel.Size = new System.Drawing.Size(62, 31);
            this.passwordRepeatLabel.TabIndex = 6;
            this.passwordRepeatLabel.Text = "Повторите пароль";
            // 
            // passwordRepeatTextBox
            // 
            this.passwordRepeatTextBox.Location = new System.Drawing.Point(197, 191);
            this.passwordRepeatTextBox.Name = "passwordRepeatTextBox";
            this.passwordRepeatTextBox.Size = new System.Drawing.Size(122, 20);
            this.passwordRepeatTextBox.TabIndex = 4;
            this.passwordRepeatTextBox.UseSystemPasswordChar = true;
            // 
            // submitButton
            // 
            this.submitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.submitButton.Location = new System.Drawing.Point(172, 224);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(123, 37);
            this.submitButton.TabIndex = 5;
            this.submitButton.Text = "Зарегистрироваться";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // loginLinkLabel
            // 
            this.loginLinkLabel.AutoSize = true;
            this.loginLinkLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginLinkLabel.Location = new System.Drawing.Point(140, 275);
            this.loginLinkLabel.Name = "loginLinkLabel";
            this.loginLinkLabel.Size = new System.Drawing.Size(186, 13);
            this.loginLinkLabel.TabIndex = 6;
            this.loginLinkLabel.TabStop = true;
            this.loginLinkLabel.Text = "Уже есть аккаунт? Авторизуйтесь!";
            this.loginLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(197, 156);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(122, 20);
            this.passwordTextBox.TabIndex = 3;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // RegistrationUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.loginLinkLabel);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.passwordRepeatTextBox);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.loginTextBox);
            this.Controls.Add(this.emailTextBox);
            this.Controls.Add(this.passwordRepeatLabel);
            this.Controls.Add(this.loginLabel);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.emailLabel);
            this.Controls.Add(this.headerLabel);
            this.Name = "RegistrationUC";
            this.Size = new System.Drawing.Size(464, 322);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Label loginLabel;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label passwordRepeatLabel;

        private System.Windows.Forms.TextBox loginTextBox;
        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox passwordRepeatTextBox;

        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.LinkLabel loginLinkLabel;
    }
}
