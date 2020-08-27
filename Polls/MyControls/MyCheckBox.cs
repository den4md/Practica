using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Polls.MyControls
{
    class MyCheckBox : CheckBox, ICheckable
    {
        public bool isChecked()
        {
            return Checked;
        }

        public void setChecked(bool @checked)
        {
            Checked = @checked;
        }

        public void setOnCheckedChanged(EventHandler eventHandler)
        {
            CheckedChanged += eventHandler;
        }

        public void setSize(Size size)
        {
            Size = size;
        }

        public void setText(string text)
        {
            Text = text;
        }
    }
}
