namespace Polls.UserControls
{
    partial class LoginUC : OwnedUserControl
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
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.submitButton = new System.Windows.Forms.Button();
            this.registrationLinkLabel = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // headerLabel
            // 
            this.headerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.headerLabel.Font = new System.Drawing.Font("Nominee-Condensed", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.headerLabel.Location = new System.Drawing.Point(0, 29);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.headerLabel.Size = new System.Drawing.Size(464, 27);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = "Авторизация";
            this.headerLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(135, 124);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(32, 13);
            this.emailLabel.TabIndex = 1;
            this.emailLabel.Text = "Email";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(135, 176);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(45, 13);
            this.passwordLabel.TabIndex = 2;
            this.passwordLabel.Text = "Пароль";
            // 
            // emailTextBox
            // 
            this.emailTextBox.Location = new System.Drawing.Point(186, 121);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(122, 20);
            this.emailTextBox.TabIndex = 1;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(186, 173);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(122, 20);
            this.passwordTextBox.TabIndex = 2;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(178, 225);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(104, 37);
            this.submitButton.TabIndex = 3;
            this.submitButton.Text = "Войти";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // registrationLinkLabel
            // 
            this.registrationLinkLabel.AutoSize = true;
            this.registrationLinkLabel.Location = new System.Drawing.Point(145, 275);
            this.registrationLinkLabel.Name = "registrationLinkLabel";
            this.registrationLinkLabel.Size = new System.Drawing.Size(174, 13);
            this.registrationLinkLabel.TabIndex = 4;
            this.registrationLinkLabel.TabStop = true;
            this.registrationLinkLabel.Text = "Нет аккаунта? Зарегистрируйте!";
            this.registrationLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // LoginUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.registrationLinkLabel);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.emailTextBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.emailLabel);
            this.Controls.Add(this.headerLabel);
            this.Name = "LoginUC";
            this.Size = new System.Drawing.Size(464, 322);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.Label passwordLabel;

        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;

        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.LinkLabel registrationLinkLabel;
    }
}
