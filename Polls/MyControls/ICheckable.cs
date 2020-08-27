using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.MyControls
{
    interface ICheckable
    {
        bool isChecked();
        void setChecked(bool @checked);
        void setText(string text);
        void setSize(Size size);
        void setOnCheckedChanged(EventHandler eventHandler);
    }
}
