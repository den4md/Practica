using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Polls.UserControls.EditTest
{
    public partial class ActionBar : UserControl
    {
        private EditTestUC owner;

        public ActionBar(EditTestUC owner)
        {
            InitializeComponent();
            this.owner = owner;
        }

        public void ToPublish()
        {
            button1.Text = "Опубликовать";
        }

        public void ToSave()
        {
            button1.Text = "Сохранить";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text.Equals("Сохранить"))
                owner.Save();
            else
                owner.Publish();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            owner.Exit();
        }

        public void setEnableSaveButton(bool enable)
        {
            button1.Enabled = enable;
        }
    }
}
