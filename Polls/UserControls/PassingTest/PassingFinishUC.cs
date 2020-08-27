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

namespace Polls.UserControls.PassingTest
{
    public partial class PassingFinishUC : UserControl
    {
        private string testID;
        private IPassingUC passingUC;
        private int leftSeconds;
        private bool isDecrementing = true;

        public PassingFinishUC(string testID, IPassingUC PassingUC, string responseJson)
        {
            this.testID = testID;
            this.passingUC = PassingUC;

            InitializeComponent();

            initTimer(Parser.FieldParse<string>(responseJson, "timeleft"));
        }

        private async void initTimer(string timeLeft)
        {
            string[] timeLeftArray = timeLeft.Split(':');
            leftSeconds = short.Parse(timeLeftArray[0]) * 3600 + short.Parse(timeLeftArray[1]) * 60
                + short.Parse(timeLeftArray[2]);

            var progress = new Progress<string>(s => label2.Text = s);
            await Task.Factory.StartNew(() => timer(progress),
                                TaskCreationOptions.LongRunning);

            if (leftSeconds.Equals(0))
            {
                MessageBox.Show("Время, отведённое на прохождения опроса закончилось. Опрос будет закрыт.",
                    "Внимание", MessageBoxButtons.OK);
                passingUC.closeUC();
            }
        }

        private void timer(IProgress<string> progress)
        {
            while (isDecrementing && !leftSeconds.Equals(0))
            {
                decrementTimer(progress);
            }
        }

        private void decrementTimer(IProgress<string> progress)
        {
            updateLeftTime(progress);
            Thread.Sleep(1000);
            --leftSeconds;
        }

        private void updateLeftTime(IProgress<string> progress)
        {
            int hours = leftSeconds / 3600;
            int minutes = (leftSeconds % 3600) / 60;
            int seconds = (leftSeconds % 3600) % 60;

            string z = "0";
            string e = "";
            progress.Report($"Оставшееся время - {((hours < 10) ? z : e)}{hours}:{((minutes < 10) ? z : e)}{minutes}:{((seconds < 10) ? z : e)}{seconds}");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ответ не сохранены окончательно. Продолжить?",
                "Внимание", MessageBoxButtons.OKCancel).Equals(DialogResult.OK))
            {
                isDecrementing = false;
                passingUC.closeUC();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string response = ApiRequests.PassingFinishPost(testID);

            if (Parser.ResultParse(response))
            {
                isDecrementing = false;
                passingUC.updateSlide(response);
            }
            else
            {
                MessageBox.Show("Не удалось завершить опрос ", "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string response = ApiRequests.PassingPrevPut(testID);

            if (Parser.ResultParse(response))
            {
                isDecrementing = false;
                passingUC.updateSlide(response);
            }
            else
            {
                MessageBox.Show("Возможно тест уже удалён или неактивен", "Ошибка", MessageBoxButtons.OK);
            }
        }
    }
}
