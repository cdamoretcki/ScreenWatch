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
         
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {

        }
        
        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {        // always work in a try/catch block so exceptions dont crash your app 
                //API CALL
                ScreenShotActions SSA = new ScreenShotActions();
                if (SSA != null)
                {       
                    List<TextTrigger> lstOftextTriggers = SSA.getTextTriggers(); //instacne to getTextTiggers
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
                                NewCell.Text = textTrigger.tokenString;
                                NewRow.Cells.Add(NewCell);
                                //DocumentsToDownloadTable.Rows.Add(NewRow);
                                DocumentsToDownloadTable.Controls.Add(NewRow);

                                NewRow1 = new TableRow();
                                NewCell = new TableCell();
                                NewCell.Text = textTrigger.tokenString;
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
            try
            {        
                //API CALL
                ScreenShotActions SSA = new ScreenShotActions();
                if (SSA != null)
                {       
                    List<ColorTrigger> lstOfColorTriggers = SSA.getColorTriggers(); 
                    if (lstOfColorTriggers != null)
                    {        
                        TableRow NewRow = null;
                        TableRow NewRow1 = null;
                        TableRow NewRow2 = null;      
                        TableCell NewCell = null;                        
                        ColorTrigger colorTrigger = null; 

                        for (int x = 0; x < lstOfColorTriggers.Count; x++)
                        {
                            colorTrigger = (ColorTrigger)lstOfColorTriggers[x];      
                            if (colorTrigger != null)
                            {
                                NewRow = new TableRow();        
                                NewCell = new TableCell();      
                                NewCell.Text = colorTrigger.tokenString;
                                NewRow.Cells.Add(NewCell);             
                                DocumentsToDownloadTable1.Controls.Add(NewRow);

                                NewRow1 = new TableRow();
                                NewCell = new TableCell();
                                NewCell.Text = colorTrigger.tokenString;
                                NewRow.Cells.Add(NewCell);
                                DocumentsToDownloadTable1.Controls.Add(NewRow1);
                                
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
                                DocumentsToDownloadTable1.Controls.Add(NewRow2);  
                            }     
                        }        
                    } 
                }
            }
            catch (Exception x1)
            {
                //MessageBox.Show(x1.Message,"Problem"); 
            }
        }       
        }
      

}

                                  
    
