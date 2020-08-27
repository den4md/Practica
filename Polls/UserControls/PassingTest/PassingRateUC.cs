using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Polls.UserControls.PassingTest
{
    public partial class PassingRateUC : UserControl
    {
        private string href;
        private IPassingUC passingUC;
        private string testID;

        public PassingRateUC(string testID, IPassingUC PassingUC, string responseJson)
        {
            this.testID = testID;
            this.passingUC = PassingUC;
            this.href = Parser.FieldParse<string>(responseJson, "href");

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)  // rate
        {
            string response = ApiRequests.RateTestPost(testID, (int)numericUpDown1.Value);

            if (Parser.ResultParse(response))
            {
                MessageBox.Show("Оценка выставлена", "Успешно", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Во время оценивания произошла ошибка", "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)  // close
        {
            passingUC.closeUC();
        }

        private void pictureBox1_Click(object sender, EventArgs e)  // copy link
        {
            if (href != null)
                Clipboard.SetText(href, TextDataFormat.UnicodeText);
        }
    }
}
