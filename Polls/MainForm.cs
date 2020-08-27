using Polls.UserControls;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Polls
{
    public partial class MainForm : Form
    {
        private List<OwnedUserControl> stackUC = new List<OwnedUserControl>();

        //public SplashUC splashUC;
        //public LoginUC loginUC;
        //public RegistrationUC registrationUC;
        //public MainUC mainUC;
        //public SearchUC searchUC;
        //public CreatedTestshUC createdTestsUC;
        //public PassedTestsUC passedTestsUC;
        //public FavoriteTestsUC favoriteTestsUC;

        public MainForm()
        {
            InitializeComponent();
            this.addUC(new SplashUC(this));
        }

        public void addUC(OwnedUserControl uc)
        {
            uc.Owner = this;

            if (!stackUC.Count.Equals(0))
            {
                stackUC[stackUC.Count - 1].Visible = false;
            }
            stackUC.Add(uc);
            uc.Visible = true;
        }

        public void popUC()
        {
            if (!stackUC.Count.Equals(0))
            {
                stackUC[stackUC.Count - 1].Visible = false;
                stackUC.RemoveAt(stackUC.Count - 1);
                if (!stackUC.Count.Equals(0))
                {
                    stackUC[stackUC.Count - 1].Visible = true;
                }
            }
            refresh();
        }

        public void changeUC(OwnedUserControl uc)
        {
            popUC();
            addUC(uc);
        }

        public void refresh()
        {
            if (!stackUC.Count.Equals(0))
            {
                stackUC[stackUC.Count - 1].RefreshUC();
            }
        }
    }
}
