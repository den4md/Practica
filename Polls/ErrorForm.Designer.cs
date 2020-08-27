namespace Polls
{
    partial class ErrorForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label = new System.Windows.Forms.Label();
            this.confirmButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // emailLabel
            // 
            this.label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label.Location = new System.Drawing.Point(12, 4);
            this.label.Name = "emailLabel";
            this.label.Size = new System.Drawing.Size(170, 53);
            this.label.TabIndex = 0;
            this.label.Text = "<Текст ошибки>";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // submitButton
            // 
            this.confirmButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.confirmButton.Location = new System.Drawing.Point(72, 60);
            this.confirmButton.Name = "submitButton";
            this.confirmButton.Size = new System.Drawing.Size(47, 23);
            this.confirmButton.TabIndex = 1;
            this.confirmButton.Text = "Ну ок(";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // ErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 95);
            this.ControlBox = false;
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ErrorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "<Название окна>";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Button confirmButton;
    }
}