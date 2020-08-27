namespace Polls.UserControls.MainMenu
{
    partial class UserTestsUC
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelCreated = new System.Windows.Forms.Label();
            this.labelPassed = new System.Windows.Forms.Label();
            this.labelFavorite = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(0, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(464, 2);
            this.label1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(153, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 32);
            this.label2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(307, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1, 32);
            this.label3.TabIndex = 2;
            // 
            // labelCreated
            // 
            this.labelCreated.Location = new System.Drawing.Point(0, 0);
            this.labelCreated.Name = "labelCreated";
            this.labelCreated.Size = new System.Drawing.Size(154, 32);
            this.labelCreated.TabIndex = 3;
            this.labelCreated.Text = "Созданные";
            this.labelCreated.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelCreated.Click += new System.EventHandler(this.label4_Click);
            // 
            // labelPassed
            // 
            this.labelPassed.Location = new System.Drawing.Point(153, 0);
            this.labelPassed.Name = "labelPassed";
            this.labelPassed.Size = new System.Drawing.Size(155, 32);
            this.labelPassed.TabIndex = 4;
            this.labelPassed.Text = "Пройденные";
            this.labelPassed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelPassed.Click += new System.EventHandler(this.label5_Click);
            // 
            // labelFavorite
            // 
            this.labelFavorite.Location = new System.Drawing.Point(307, 0);
            this.labelFavorite.Name = "labelFavorite";
            this.labelFavorite.Size = new System.Drawing.Size(157, 32);
            this.labelFavorite.TabIndex = 5;
            this.labelFavorite.Text = "Избранные";
            this.labelFavorite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelFavorite.Click += new System.EventHandler(this.label6_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 32);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(464, 245);
            this.flowLayoutPanel1.TabIndex = 6;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // UserTestsUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelCreated);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelPassed);
            this.Controls.Add(this.labelFavorite);
            this.Name = "UserTestsUC";
            this.Size = new System.Drawing.Size(464, 322);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelCreated;
        private System.Windows.Forms.Label labelPassed;
        private System.Windows.Forms.Label labelFavorite;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}
