namespace Polls.UserControls.MainMenu
{
    partial class MainMenuBottomNav
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
            this.labelMain = new System.Windows.Forms.Label();
            this.labelSearch = new System.Windows.Forms.Label();
            this.labelProfile = new System.Windows.Forms.Label();
            this.labelTests = new System.Windows.Forms.Label();
            this.labelNewTest = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelHome
            // 
            this.labelMain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelMain.Location = new System.Drawing.Point(3, 0);
            this.labelMain.Name = "labelHome";
            this.labelMain.Size = new System.Drawing.Size(82, 40);
            this.labelMain.TabIndex = 0;
            this.labelMain.Text = "Домой";
            this.labelMain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelMain.Click += new System.EventHandler(this.labelHome_Click);
            this.labelMain.MouseEnter += new System.EventHandler(this.labelHome_MouseEnter);
            this.labelMain.MouseLeave += new System.EventHandler(this.labelHome_MouseLeave);
            // 
            // labelSearch
            // 
            this.labelSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelSearch.Location = new System.Drawing.Point(91, 0);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(82, 40);
            this.labelSearch.TabIndex = 1;
            this.labelSearch.Text = "Поиск";
            this.labelSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelSearch.Click += new System.EventHandler(this.labelSearch_Click);
            this.labelSearch.MouseEnter += new System.EventHandler(this.labelSearch_MouseEnter);
            this.labelSearch.MouseLeave += new System.EventHandler(this.labelSearch_MouseLeave);
            // 
            // labelProfile
            // 
            this.labelProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelProfile.Location = new System.Drawing.Point(379, 0);
            this.labelProfile.Name = "labelProfile";
            this.labelProfile.Size = new System.Drawing.Size(82, 40);
            this.labelProfile.TabIndex = 2;
            this.labelProfile.Text = "Профиль";
            this.labelProfile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelProfile.Click += new System.EventHandler(this.labelProfile_Click);
            this.labelProfile.MouseEnter += new System.EventHandler(this.labelProfile_MouseEnter);
            this.labelProfile.MouseLeave += new System.EventHandler(this.labelProfile_MouseLeave);
            // 
            // labelTests
            // 
            this.labelTests.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelTests.Location = new System.Drawing.Point(291, 0);
            this.labelTests.Name = "labelTests";
            this.labelTests.Size = new System.Drawing.Size(82, 40);
            this.labelTests.TabIndex = 3;
            this.labelTests.Text = "Мои Тесты";
            this.labelTests.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelTests.Click += new System.EventHandler(this.labelTests_Click);
            this.labelTests.MouseEnter += new System.EventHandler(this.labelTests_MouseEnter);
            this.labelTests.MouseLeave += new System.EventHandler(this.labelTests_MouseLeave);
            // 
            // labelNewTest
            // 
            this.labelNewTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelNewTest.Location = new System.Drawing.Point(191, 0);
            this.labelNewTest.Name = "labelNewTest";
            this.labelNewTest.Size = new System.Drawing.Size(82, 40);
            this.labelNewTest.TabIndex = 4;
            this.labelNewTest.Text = "Новый";
            this.labelNewTest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelNewTest.Click += new System.EventHandler(this.labelNewTest_Click);
            this.labelNewTest.MouseEnter += new System.EventHandler(this.labelNewTest_MouseEnter);
            this.labelNewTest.MouseLeave += new System.EventHandler(this.labelNewTest_MouseLeave);
            // 
            // MainMenuBottomNav
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.labelNewTest);
            this.Controls.Add(this.labelTests);
            this.Controls.Add(this.labelProfile);
            this.Controls.Add(this.labelSearch);
            this.Controls.Add(this.labelMain);
            this.Name = "MainMenuBottomNav";
            this.Size = new System.Drawing.Size(464, 40);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelMain;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.Label labelProfile;
        private System.Windows.Forms.Label labelTests;
        private System.Windows.Forms.Label labelNewTest;
    }
}
