using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Polls.UserControls.MainMenu;

namespace Polls.UserControls.PassingTest
{
    public partial class PassingTestUC : OwnedUserControl, IPassingUC
    {
        private string testID;
        private string authorID;

        public PassingTestUC(MainForm owner, string testID) : base(owner)
        {
            this.testID = testID;
            InitializeComponent();

            initUC();
        }

        private void initUC()
        {
            string response = ApiRequests.PassingTestGet(testID);
            
            if (JObject.Parse(response)["slide"].ToObject<object>() != null && JObject.Parse(response)["slide"]["author"] != null)
                authorID = JObject.Parse(response)["slide"]["author"]["userID"].ToObject<string>();
            updateSlide(response);
        }

        public void updateSlide(string responseJson)
        {
            UserControl uc = null;

            switch (Parser.FieldParse<string>(responseJson, "slideType"))
            {
                case "welcome":
                    uc = new PassingWelcomeUC(testID, this, responseJson);
                    uc.Name = "PassingWelcomeUC";
                    break;
                case "question":
                    uc = new PassingQuestionUC(testID, this, responseJson);
                    uc.Name = "PassingQuestionUC";
                    break;
                case "finish":
                    uc = new PassingFinishUC(testID, this, responseJson);
                    uc.Name = "PassingFinishUC";
                    break;
                default:    //it's response from finish slide
                    uc = new PassingRateUC(testID, this, responseJson);
                    uc.Name = "PassingRateUC";
                    break;
            }

            uc.Margin = new Padding(0);
            uc.Location = new Point(0, 0);

            Controls.Clear();
            Controls.Add(uc);
        }

        public void closeUC()
        {
            Owner.popUC();
        }

        public void authorClick()
        {
            Owner.addUC(new UserProfileUC(Owner, authorID));
        }

        public void openImage(string imageBase64)
        {
            Owner.addUC(new ImageViewUC(Owner, imageBase64));
        }
    }
}
