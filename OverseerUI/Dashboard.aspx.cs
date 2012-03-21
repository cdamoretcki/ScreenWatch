using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ScreenWatchData;
using System.Drawing;


namespace ScreenWatchUI
{

    public partial class Dashboard : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ScreenShotActions data = new ScreenShotActions();
                RefreshTriggers(data);
            }
        }

        protected void SubmitNewText(object sender, EventArgs e)
        {
            //insert trigger
            ScreenShotActions data = new ScreenShotActions();
            TextTrigger textTrigger = new TextTrigger()
            {
                triggerString = txtTriggerTB.Text,
                userName = txtUserNameTB.Text,
                userEmail = txtUserEmailTB.Text
            };
            data.insertTextTrigger(textTrigger);

            //refresh the triggers to bring back down the new trigger
            RefreshTriggers(data);

            //clear text boxes for a new trigger to be entered
            Clear(txtTriggerTB);
            Clear(txtUserEmailTB);
            Clear(txtUserNameTB);
        }

        protected void SubmitNewTone(object sender, EventArgs e)
        {
            //insert trigger
            ScreenShotActions data = new ScreenShotActions();
            ToneTrigger toneTrigger = new ToneTrigger()
            {
                userName = txtcolorUserTB.Text,
                userEmail = txtcolorEmailTB.Text, 
                sensitivity = txtcolorSensitivity.Text,
                lowerColorBound = Color.FromArgb(Convert.ToInt32(txtcolorLBTB.Text, 16)),
                upperColorBound = Color.FromArgb(Convert.ToInt32(txtcolorUBTB.Text, 16))
            };
            data.insertToneTrigger(toneTrigger);

            //refresh the triggers to bring back down the new trigger
            RefreshTriggers(data);

            //clear text boxes for a new trigger to be entered
            Clear(txtcolorUserTB);
            Clear(txtcolorEmailTB);
            Clear(txtcolorSensitivity);
            Clear(txtcolorLBTB);
            Clear(txtcolorUBTB);
        }

        private void RefreshTriggers(ScreenShotActions data)
        {
            textRepeater.DataSource = data.getAllTextTriggers();
            textRepeater.DataBind();

            colorRepeater.DataSource = data.getAllToneTriggers();
            colorRepeater.DataBind();
        }

        private void Clear(TextBox thisOldTextBox)
        {
            thisOldTextBox.Text = string.Empty;
        }

    }


}



