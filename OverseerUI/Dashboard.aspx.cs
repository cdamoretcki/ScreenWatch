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
                {        // make sure you got this thing before you make calls agains it 
                    List<TextTrigger> lstOftextTriggers = SSA.getTextTriggers(); //instacne to getTextTiggers
                    if (lstOftextTriggers != null)
                    {        // again make sure you got something ; validation
                        TableRow NewRow = null;       // a pointer for a new HTML row 
                        TableCell NewCell = null;     // a pointer for a new HTML cell 
                        TextTrigger textTrigger = null; // pointer to use in textTrigger list

                        for (int x = 0; x < lstOftextTriggers.Count; x++)
                        {
                            textTrigger = (TextTrigger)lstOftextTriggers[x];       // i'm casting here, you might not have to have the (TextTrigger) part, not sure 
                            if (textTrigger != null)
                            {
                                NewRow = new TableRow();        // create a new row in memory 
                                NewCell = new TableCell();        // create a new cell for the row above, you could create as many cells as you need here. Many cells to 1 row                                                                      
                                NewCell.Text = textTrigger.tokenString;

                                NewRow.Cells.Add(NewCell);                // add that cell to the row 
                                DocumentsToDownloadTable.Controls.Add(NewRow);        // and add the row to the table (use your table name)
                            }     // end if
                        }        //end for 
                    } // end if
                    //else { 
                    // MessageBox.Show("list is empty","Problem"); 
                    // } 

                }
            }
            catch (Exception x1)
            {
                //MessageBox.Show(x1.Message,"Problem"); 
            }
        }       
        }

    }                              
    
