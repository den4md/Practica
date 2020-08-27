using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Polls.UserControls.EditTest;

namespace Polls.UserControls
{
    public partial class OwnedUserControl : UserControl
    {
        public MainForm Owner { get; set; }
        public OwnedUserControl(MainForm owner)
        {
            InitializeComponent();
            Location = new Point(0, 0);
            Owner = owner;
            owner.Controls.Add(this);
            Size = new Size(464, 322);  // refactor
            TabIndex = 0;
            Visible = false;
        }

        virtual public void RefreshUC()
        { }

        protected void Error(string errorMessage = "Произошла ошибка", string title = "Ошибка", bool closeApp = true)
        {
            ErrorForm errorForm = new ErrorForm(errorMessage, title, closeApp);
            errorForm.ShowDialog(this.Owner);
        }

        //public static explicit operator OwnedUserControl(EditSlideUC v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
