using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.UserControls.PassingTest
{
    public interface IPassingUC
    {
        void updateSlide(string responseJson);
        void closeUC();
        void authorClick();
        void openImage(string imageBase64);
    }
}
