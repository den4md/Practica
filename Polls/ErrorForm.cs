using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Polls
{
    public partial class ErrorForm : Form
    {
        public ErrorForm(string errorMessage="Произошла ошибка", string title = "Ошибка", bool closeApp =true)
        {
            InitializeComponent();
            label.Text = errorMessage;
            Text = title;
            if (closeApp)
                FormClosed += new FormClosedEventHandler(ExitFromApp);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ExitFromApp(object sender, EventArgs e)
        {         
            Application.Exit();
        }

    }
}
