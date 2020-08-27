using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Polls.Models
{
    public class Answer
    {
        public int answerNumber { get; set; }
        public bool isSelected { get; set; }
        public string value { get; set; }
        public bool isCustom { get; set; }
        public int nextSlideNumber { get; set; }

        public Answer(int answerNumber = 0, bool isCustom = false)
        {
            this.answerNumber = answerNumber;
            this.isCustom = isCustom;
            nextSlideNumber = -1;
            isSelected = false;
            value = "";
        }

        private bool makeError(string s)
        {
            MessageBox.Show(s, "Твоя жизнь", MessageBoxButtons.OK);

            return false;
        }

        public bool Validate()
        {
            if (value == null || value.Trim().Equals(""))
                return makeError("Есть пустой ответ");
            return true;
        }
    }
}
