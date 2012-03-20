using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ScreenWatchData;


namespace ScreenWatchUI
{

    public partial class Dashboard : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.Table DocumentsToDownloadTable;

        protected void Page_Load(object sender, EventArgs e)
        {
            ScreenShotActions data = new ScreenShotActions();
            ScreenShotActions dataTone = new ScreenShotActions();
            ScreenShotActions dataToneSubmit = new ScreenShotActions();

            textRepeater.DataSource = data.getAllTextTriggers();
            textRepeater.DataBind();

            textSubmitRepeater.DataSource = data.getAllTextTriggers();
            textSubmitRepeater.DataBind();

            //TextGridView.DataSource = data.getAllTextTriggers();
           // TextGridView.DataBind();

            colorRepeater.DataSource = dataTone.getAllToneTriggers();
            colorRepeater.DataBind();

            colorSubmitRepeater.DataSource = dataTone.getAllToneTriggers();
           colorSubmitRepeater.DataBind();

            //ColorGridView.DataSource = data.getAllToneTriggers();
            //colorGridView.DataBind();
        }

        protected void Submit(object sender, EventArgs e)
        {
            foreach (RepeaterItem iter in colorRepeater.Items)
            {
                // id livre courant  
                if (iter.ItemType == ListItemType.Item || iter.ItemType == ListItemType.AlternatingItem)        
                { 
                    string guid = ((HiddenField)iter.FindControl("guid")).Value.ToString();
                    int nbExemplaires = int.Parse(((System.Web.UI.WebControls.TextBox)iter.FindControl("txtcolorUser")).Text.ToString()); 
                }   
            } 
        }


        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {        // always work in a try/catch block so exceptions dont crash your app 
                //API CALL
                ScreenShotActions SSA = new ScreenShotActions();
                if (SSA != null)
                {
                    List<TextTrigger> lstOftextTriggers = SSA.getAllTextTriggers(); //instacne to getTextTiggers
                    if (lstOftextTriggers != null)
                    {

                        TableRow NewRow = null;
                        TableRow NewRow1 = null;
                        TableRow NewRow2 = null;
                        TableCell NewCell = null;
                        TextBox NewTextBox = null;
                        TextTrigger textTrigger = null;

                        for (int x = 0; x < lstOftextTriggers.Count; x++)
                        {
                            textTrigger = (TextTrigger)lstOftextTriggers[x];
                            if (textTrigger != null)
                            {

                                NewRow = new TableRow();
                                NewCell = new TableCell();
                                NewCell.Text = textTrigger.triggerString;
                                NewRow.Cells.Add(NewCell);
                                //DocumentsToDownloadTable.Rows.Add(NewRow);
                                DocumentsToDownloadTable.Controls.Add(NewRow);

                                NewRow1 = new TableRow();
                                NewCell = new TableCell();
                                NewCell.Text = textTrigger.triggerString;
                                NewRow.Cells.Add(NewCell);
                                DocumentsToDownloadTable.Controls.Add(NewRow1);

                                NewRow2 = new TableRow();
                                NewCell = new TableCell();
                                NewTextBox = new TextBox();
                                NewCell.Controls.Add(NewTextBox);
                                NewRow.Cells.Add(NewCell);
                                DocumentsToDownloadTable.Controls.Add(NewRow2);
                            }     // end if
                        }        //end for 
                    } // end if              
                }
            }
            catch (Exception x1)
            {
                //MessageBox.Show(x1.Message,"Problem"); 
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string username = "Kathy";
            try
            {
                //API CALL
                ScreenShotActions SSA = new ScreenShotActions();
                if (SSA != null)
                {
                    List<ToneTrigger> lstOfColorTriggers = SSA.getToneTriggersByUser(username);
                    if (lstOfColorTriggers != null)
                    {
                        TableRow NewRow = null;
                        TableRow NewRow1 = null;
                        TableRow NewRow2 = null;
                        TableCell NewCell = null;
                        ToneTrigger colorTrigger = null;

                        foreach (var colortrigger in lstOfColorTriggers)
                        {
                            NewRow = new TableRow();
                            NewCell = new TableCell();
                            NewCell.Text = colorTrigger.sensitivity;
                            NewRow.Cells.Add(NewCell);
                          //  DocumentsToDownloadTable1.Controls.Add(NewRow);

                            NewRow1 = new TableRow();
                            NewCell = new TableCell();
                            NewCell.Text = colorTrigger.sensitivity;
                            NewRow.Cells.Add(NewCell);
                          //  DocumentsToDownloadTable1.Controls.Add(NewRow1);

                            NewRow2 = new TableRow();
                            NewCell = new TableCell();

                            //Create textbox
                            TextBox txtcolorPicker = new TextBox();
                            txtcolorPicker.ID = "colorPicker";
                            txtcolorPicker.Attributes.Add("runat", "server");

                            //Create colorpicker as an extendor to the textbox created above
                            AjaxControlToolkit.ColorPickerExtender CPE = new AjaxControlToolkit.ColorPickerExtender();
                            CPE.TargetControlID = "colorPicker";
                            CPE.Enabled = true;
                            CPE.ID = "colorPicker_ColorPickerExtender";
                            
                            NewCell.Controls.Add(txtcolorPicker);
                            NewRow.Cells.Add(NewCell);
                           // DocumentsToDownloadTable1.Controls.Add(NewRow2);
                        }
                    }
                }
            }
            catch (Exception x1)
            {
               
            }
        }

   
    }


}



