using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Polls.UserControls.MainMenu
{
    public partial class MainMenuBottomNav : UserControl
    {
        private MainMenuUC _owner;
        private bool isMainActive = false;
        private bool isSearchActive = false;
        private bool isTestsActive = false;
        private bool isProfileActive = false;


        public MainMenuBottomNav()
        {
            InitializeComponent();
        }

        public void setOwner(MainMenuUC owner)
        {
            _owner = owner;
        }

        private void labelHome_Click(object sender, EventArgs e)
        {
            _owner.changeToMain();
        }

        private void labelSearch_Click(object sender, EventArgs e)
        {
            _owner.changeToSearch();
        }

        private void labelNewTest_Click(object sender, EventArgs e)
        {
            _owner.openNewtest();
        }

        private void labelTests_Click(object sender, EventArgs e)
        {
            _owner.changeToUserTests();
        }

        private void labelProfile_Click(object sender, EventArgs e)
        {
            _owner.changeToProfile();
        }

        private void labelHome_MouseEnter(object sender, EventArgs e)
        {
            if (!isMainActive)
                labelMain.BorderStyle = BorderStyle.Fixed3D;
            if (!isSearchActive)
                labelSearch.BorderStyle = BorderStyle.None;
            labelNewTest.BorderStyle = BorderStyle.None;
            if (!isTestsActive)
                labelTests.BorderStyle = BorderStyle.None;
            if (!isProfileActive)
                labelProfile.BorderStyle = BorderStyle.None;
        }

        private void labelHome_MouseLeave(object sender, EventArgs e)
        {
            if (!isMainActive)
                labelMain.BorderStyle = BorderStyle.None;
        }

        private void labelTests_MouseEnter(object sender, EventArgs e)
        {
            if (!isMainActive)
                labelMain.BorderStyle = BorderStyle.None;
            if (!isSearchActive)
                labelSearch.BorderStyle = BorderStyle.None;
            labelNewTest.BorderStyle = BorderStyle.None;
            if (!isTestsActive)
                labelTests.BorderStyle = BorderStyle.Fixed3D;
            if (!isProfileActive)
                labelProfile.BorderStyle = BorderStyle.None;
        }

        private void labelTests_MouseLeave(object sender, EventArgs e)
        {
            if (!isTestsActive)
                labelTests.BorderStyle = BorderStyle.None;
        }

        private void labelSearch_MouseEnter(object sender, EventArgs e)
        {
            if (!isMainActive)
                labelMain.BorderStyle = BorderStyle.None;
            if (!isSearchActive)
                labelSearch.BorderStyle = BorderStyle.Fixed3D;
            labelNewTest.BorderStyle = BorderStyle.None;
            if (!isTestsActive)
                labelTests.BorderStyle = BorderStyle.None;
            if (!isProfileActive)
                labelProfile.BorderStyle = BorderStyle.None;
        }

        private void labelSearch_MouseLeave(object sender, EventArgs e)
        {
            if (!isSearchActive)
                labelSearch.BorderStyle = BorderStyle.None;
        }

        private void labelNewTest_MouseEnter(object sender, EventArgs e)
        {
            if (!isMainActive)
                labelMain.BorderStyle = BorderStyle.None;
            if (!isSearchActive)
                labelSearch.BorderStyle = BorderStyle.None;
            labelNewTest.BorderStyle = BorderStyle.Fixed3D;
            if (!isTestsActive)
                labelTests.BorderStyle = BorderStyle.None;
            if (!isProfileActive)
                labelProfile.BorderStyle = BorderStyle.None;
        }

        private void labelNewTest_MouseLeave(object sender, EventArgs e)
        {
            labelNewTest.BorderStyle = BorderStyle.None;
        }

        private void labelProfile_MouseEnter(object sender, EventArgs e)
        {
            if (!isMainActive)
                labelMain.BorderStyle = BorderStyle.None;
            if (!isSearchActive)
                labelSearch.BorderStyle = BorderStyle.None;
            labelNewTest.BorderStyle = BorderStyle.None;
            if (!isTestsActive)
                labelTests.BorderStyle = BorderStyle.None;
            if (!isProfileActive)
                labelProfile.BorderStyle = BorderStyle.Fixed3D;
        }

        private void labelProfile_MouseLeave(object sender, EventArgs e)
        {
            if (!isProfileActive)
                labelProfile.BorderStyle = BorderStyle.None;
        }

        public void setMainActive()
        {
            isMainActive = true;
            labelMain.Cursor = Cursor;
            labelMain.BorderStyle = BorderStyle.FixedSingle;
        }

        public void setSearchActive()
        {
            isSearchActive = true;
            labelSearch.Cursor = Cursor;
            labelSearch.BorderStyle = BorderStyle.FixedSingle;
        }

        public void setTestsActive()
        {
            isTestsActive = true;
            labelTests.Cursor = Cursor;
            labelTests.BorderStyle = BorderStyle.FixedSingle;
        }

        public void setProfileActive()
        {
            isProfileActive = true;
            labelProfile.Cursor = Cursor;
            labelProfile.BorderStyle = BorderStyle.FixedSingle;
        }
    }
}
