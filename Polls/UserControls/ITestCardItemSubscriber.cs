using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polls.UserControls
{
    public interface ITestCardItemSubscriber
    {
        void AuthorClickHandler(TestCardItemUC sender);
        void FavouriteClickHandler(TestCardItemUC sender);
        void DeleteClickHandler(TestCardItemUC sender);
        void EditClickHandler(TestCardItemUC sender);
        void StatisticsClickHandler(TestCardItemUC sender);
        void HeaderClickHandler(TestCardItemUC sender);
    }
}
