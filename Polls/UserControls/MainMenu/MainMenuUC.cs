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

namespace Polls.UserControls.MainMenu
{
    public partial class MainMenuUC : OwnedUserControl
    {
        protected MainMenuBottomNav bottomNavigation;

        public MainMenuUC(MainForm owner) : base(owner)
        {
            InitializeComponent();
            bottomNavInit();
        }

        private void bottomNavInit()
        {
            bottomNavigation = new MainMenuBottomNav();
            bottomNavigation.setOwner(this);
            bottomNavigation.Location = new Point(0, 282);
            bottomNavigation.Visible = true;
            bottomNavigation.TabIndex = 0;
            Controls.Add(bottomNavigation);
        }

        virtual public void changeToMain()
        {
            Owner.changeUC(new MainUC(Owner));
        }

        virtual public void changeToSearch()
        {
            Owner.changeUC(new SearchUC(Owner));
        }

        virtual public void openNewtest()
        {
            Owner.addUC(new EditTestUC(Owner));
        }

        virtual public void changeToUserTests()
        {
            Owner.changeUC(new UserTestsUC(Owner));
        }

        virtual public void changeToProfile()
        {
            Owner.changeUC(new UserProfileUC(Owner));
        }
    }
}
