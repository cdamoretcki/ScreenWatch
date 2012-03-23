using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ScreenWatchData;
using System.Drawing;
using System.Text.RegularExpressions;


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
                triggerString = TriggerTB.Text,
                userName = UserNameTB.Text,                
            };
            data.insertTextTrigger(textTrigger);

            //refresh the triggers to bring back down the new trigger
            RefreshTriggers(data);

            //clear text boxes for a new trigger to be entered
            Clear(TriggerTB);          
            Clear(UserNameTB);
        }

        protected void SubmitNewTone(object sender, EventArgs e)
        {
            //insert trigger
            ScreenShotActions data = new ScreenShotActions();
            ToneTrigger toneTrigger = new ToneTrigger()
            {
                userName = colorUserTB.Text,
                sensitivity = colorSensitivity.Text,
                //System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#FFCC66"); 
                
                lowerColorBound = System.Drawing.ColorTranslator.FromHtml(colorLBTB.Text),
                upperColorBound = System.Drawing.ColorTranslator.FromHtml(colorUBTB.Text) 
                
            };
            data.insertToneTrigger(toneTrigger);

            //refresh the triggers to bring back down the new trigger
            RefreshTriggers(data);

            //clear text boxes for a new trigger to be entered
            Clear(colorUserTB);            
            Clear(colorSensitivity);
            Clear(colorLBTB);
            Clear(colorUBTB);
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



