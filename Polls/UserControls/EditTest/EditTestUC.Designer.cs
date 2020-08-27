namespace Polls.UserControls.EditTest
{
    partial class EditTestUC
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
            this.actionBar1 = new Polls.UserControls.EditTest.ActionBar(this);
            this.getTest();
            this.editTestSlidesUC1 = new Polls.UserControls.EditTest.EditTestSlidesUC(this.test);
            this.editTestMainUC1 = new Polls.UserControls.EditTest.EditTestMainUC(this.test);
            this.SuspendLayout();
            // 
            // actionBar1
            // 
            this.actionBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.actionBar1.Location = new System.Drawing.Point(0, 282);
            this.actionBar1.Name = "actionBar1";
            this.actionBar1.Size = new System.Drawing.Size(464, 40);
            this.actionBar1.TabIndex = 2;
            // 
            // editTestSlidesUC1
            // 
            this.editTestSlidesUC1.AutoSize = true;
            this.editTestSlidesUC1.Location = new System.Drawing.Point(0, 0);
            this.editTestSlidesUC1.Name = "editTestSlidesUC1";
            this.editTestSlidesUC1.Size = new System.Drawing.Size(465, 322);
            this.editTestSlidesUC1.TabIndex = 1;
            this.editTestSlidesUC1.Visible = false;
            // 
            // editTestMainUC1
            // 
            this.editTestMainUC1.Location = new System.Drawing.Point(0, 0);
            this.editTestMainUC1.Name = "editTestMainUC1";
            this.editTestMainUC1.Size = new System.Drawing.Size(464, 322);
            this.editTestMainUC1.TabIndex = 0;
            this.editTestMainUC1.Visible = false;
            // 
            // EditTestUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.actionBar1);
            this.Controls.Add(this.editTestSlidesUC1);
            this.Controls.Add(this.editTestMainUC1);
            this.Name = "EditTestUC";
            this.Size = new System.Drawing.Size(464, 322);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EditTestMainUC editTestMainUC1;
        private ActionBar actionBar1;
        private EditTestSlidesUC editTestSlidesUC1;
    }
}
