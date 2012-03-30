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
    public partial class TextTriggers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {
                ScreenShotActions data = new ScreenShotActions();
                RefreshTriggers(data);

                UserDropDownList.DataSource = data.getUsers();
                UserDropDownList.DataBind();
                
            }
        }

            protected void SubmitNewText(object sender, EventArgs e)
        {
            //insert trigger
            ScreenShotActions data = new ScreenShotActions();
            TextTrigger textTrigger = new TextTrigger()
            {
                triggerString = TriggerTB.Text,
                userName = UserDropDownList.Text,
            };
            data.insertTextTrigger(textTrigger);

            //refresh the triggers to bring back down the new trigger
            RefreshTriggers(data);

            //clear text boxes for a new trigger to be entered
            Clear(TriggerTB);
        }

        protected void UpdateText(object sender, EventArgs e)
            {
                ScreenShotActions updateData = new ScreenShotActions();
                TextTrigger textTrigger = new TextTrigger()
                {
                    triggerString = TriggerTB.Text,
                    userName = UserDropDownList.Text
                };
               updateData.updateTextTrigger(textTrigger);
            }

        protected void DeleteText(object sender, EventArgs e)
        {
            ScreenShotActions deleteData = new ScreenShotActions();
            TextTrigger textTrigger = new TextTrigger();
          
            deleteData.deleteTextTrigger(textTrigger.id);
            
        }

        private void RefreshTriggers(ScreenShotActions data)
        {
            textRepeater.DataSource = data.getAllTextTriggers();
            textRepeater.DataBind();
         
        }

        private void Clear(TextBox thisOldTextBox)
        {
            thisOldTextBox.Text = string.Empty;
        }
    }
}