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
using System.Drawing;

namespace ScreenWatchUI
{
    public partial class ToneTriggers : System.Web.UI.Page
    {  
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ScreenShotActions data = new ScreenShotActions();
                RefreshTriggers(data);
            }
        }
        protected void SubmitNewTone(object sender, EventArgs e)
        {
            //insert trigger
            ScreenShotActions data = new ScreenShotActions();
            ToneTrigger toneTrigger = new ToneTrigger()
            {
                userName = colorUserTB.Text,
                sensitivity = colorSensitivity.Text,
               

                lowerColorBound = ColorTranslator.FromHtml(colorLBTB.Text),
                upperColorBound = ColorTranslator.FromHtml(colorUBTB.Text)

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
         
            colorRepeater.DataSource = data.getAllToneTriggers();
            colorRepeater.DataBind();
        }
        private void Clear(TextBox thisOldTextBox)
        {
            thisOldTextBox.Text = string.Empty;
        }
    }
}