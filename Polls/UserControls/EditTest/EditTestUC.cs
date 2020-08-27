using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Polls.Models;

namespace Polls.UserControls.EditTest
{
    public partial class EditTestUC : OwnedUserControl   // TODO: change to OwnedUC
    {
        private Test test;
        private string testID;
        private bool isSaved = false;
        public EditTestUC(MainForm owner, string testID = "") : base(owner)
        {
            this.testID = testID;
            InitializeComponent();

            if (Equals(testID, ""))
            { 
                editTestSlidesUC1.pictureBox3_Click(null, null);
            }

            editTestSlidesUC1.SetSuperOwner(this);

            editTestMainUC1.SetSuperOwner(this);

            SwitchToGeneral(); // sync data in UC with actual data from Test-obj

            InitSavedEvent();

            RefreshUC();
        }

        private void getTest()
        {
            if (Equals(testID, ""))
            {
                test = new Test();
            }
            else
            {
                isSaved = true;
                actionBar1.ToPublish();

                string responseJson = ApiRequests.TestEditGet(testID);
                if (Parser.ResultParse(responseJson))
                {
                    test = Parser.FieldParse<Test>(responseJson, "test");
                }
                else
                {
                    MessageBox.Show("Во время получения теста произошла ошибка (возможно тест уже опубликован или он принадлежит не Вам)",
                        "Ошибка", MessageBoxButtons.OK);
                }
            }
        }

        private void InitSavedEvent()
        {
            foreach (Control control in editTestMainUC1.Controls)
            {
                if (!(control is Label))
                {
                    control.GotFocus += EditTestEvent;
                }
            }
        }

        public void EditTestEvent(object sender, EventArgs e)
        {
            if (isSaved)
            {
                actionBar1.ToSave();
                isSaved = false;
            }
        }

        public override void RefreshUC()
        {
            if (isSaved)
                actionBar1.ToPublish();
            else
                actionBar1.ToSave();
        }
        
        public void Save()
        {
            actionBar1.setEnableSaveButton(false);
            editTestMainUC1.UpdateTest();
            //editTestSlidesUC1.updateTest();

            if (test.Validate())
                if (testID.Equals(""))
                {
                    string response = ApiRequests.TestEditPost(test);
                    if (Parser.ResultParse(response))
                    {
                        testID = Parser.FieldParse<string>(response, "testID");
                        actionBar1.ToPublish();
                        isSaved = true;
                    }
                    else
                    {
                        MessageBox.Show("Произошла неизвестная ошибка при отправке теста на сервер", "Ошибка", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    string response = ApiRequests.TestEditPut(test, testID);
                    if (Parser.ResultParse(response))
                    {
                        actionBar1.ToPublish();
                        isSaved = true;
                    }
                    else
                    {
                        MessageBox.Show("Произошла неизвестная ошибка при отправке теста на сервер", "Ошибка", MessageBoxButtons.OK);
                    }
                }

            actionBar1.setEnableSaveButton(true);
        }

        public void Publish()
        {
            if (MessageBox.Show("Если вы опубликуете тест, он станет доступен для прохождения," +
                "\n но больше вы не сможете его редактировать. Опубликовать?",
                "Внимание", MessageBoxButtons.OKCancel).Equals(DialogResult.OK))
            {
                ApiRequests.TestPublishPost(testID);
                Exit();
            }
        }

        public void Exit()
        {
            if (isSaved || MessageBox.Show("Тест не сохранён. Хотите выйти без сохранения?", "Внимание",
                    MessageBoxButtons.OKCancel).Equals(DialogResult.OK))
            { 
                Owner.popUC();
            }
        }

        internal void ChangeToEditSlide(int slideNumber)
        {
            Owner.addUC(new EditSlideUC(test, test.GetSlide(slideNumber), Owner));
            EditTestEvent(null, null);
        }

        public void SwitchToGeneral()
        {
            editTestMainUC1.Visible = true;
            editTestSlidesUC1.Visible = false;
        }

        public void SwitchToSlideList()
        {
            editTestMainUC1.UpdateTest();
            EditTestEvent(null, null);
            editTestMainUC1.Visible = false;
            editTestSlidesUC1.Visible = true;
        }
    }
}
