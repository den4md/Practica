using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.UserControls.PassingTest
{
    public class NextSlideForm
    {
        public int slideNumberSent      { get; set;}
        public List<int> answerNumbers { get; set; } = new List<int>();    // mb need null
        public string value { get; set; } = null;
    }
}
