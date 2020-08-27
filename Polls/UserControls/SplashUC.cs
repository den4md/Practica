using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Polls.UserControls.MainMenu;

namespace Polls.UserControls
{
    public partial class SplashUC : OwnedUserControl
    {
        private int splashTime = 300;
        private bool timePassed = false;

        public SplashUC(MainForm owner) : base(owner)
        {
            InitializeComponent();
        }

        private async void SplashUC_Load(object sender, EventArgs e)
        {
            if (!(Owner?.Name == "MainForm"))
            { return; }

            Thread thr = new Thread(splashTimer);
            thr.Start();

            try
            {
                await Task.Run(() => loadLogic());
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                Error("Не удалось получить сессиию. Программа будет завершена.");
            }
            if (Memory.isAuth)
            {
                changeToMain();
            }
            else
            {
                changeToLogin();
            }
        }

        private void loadLogic()
        {
            Memory.obtainData();

            while (!timePassed)
            {
                Thread.Sleep(10);
            }
        }

        private void splashTimer()
        {
            Thread.Sleep(splashTime);
            timePassed = true;
        }

        private void changeToLogin()
        {
            Owner.changeUC(new LoginUC(Owner));
        }

        private void changeToMain()
        {
            changeToLogin();
            Owner.addUC(new MainUC(Owner));
        }
    }
}
