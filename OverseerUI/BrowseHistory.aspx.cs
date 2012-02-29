using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ScreenWatchData;
using ScreenWatchUI;

namespace ScreenWatchUI
{
    public partial class BrowseHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //DisplayImage.ImageUrl = "C:\\ScreenWatch\\Images\notFound.jpg";
            //TextBox3.Focus();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
 
           /* ImageActions IA = new ImageActions();          

            string id = TextBox1.Text;

            string pathToImage = IA.getImagePath(id);
            if (pathToImage == null || pathToImage == String.Empty)
            {
                //DisplayImage.ImageUrl = @"~\Images\notFound.jpg";
                DisplayImage.ImageUrl = "C:\\ScreenWatch\\Images\notFound.jpg";
            }
            else
            {
                DisplayImage.ImageUrl = pathToImage;
                ThumbNail1.ImageUrl = pathToImage;
                ThumbNail2.ImageUrl = pathToImage;
                ThumbNail3.ImageUrl = pathToImage;
                ThumbNail4.ImageUrl = pathToImage;
            }*/
        }

        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            string getDate2 = TextBox2.Text;          
            string getDate3 = TextBox3.Text;
            
            TextBox2.Text = Calendar2.SelectedDate.ToShortDateString();
            getDate2 = TextBox2.Text;

            if(TextBox2.Text == getDate2)
            {
                Calendar2.Visible = false;
                Calendar3.Visible = true;                        
            }
        }    

        protected void Calendar3_SelectionChanged(object sender, EventArgs e)
        {
            TextBox3.Text = Calendar3.SelectedDate.ToShortDateString();
            //getDate3 = TextBox3.Text;
        }

        protected void ClearDates_Click(object sender, EventArgs e)
        {
            TextBox2.Text = "";
            TextBox3.Text = "";
            Calendar2.Visible = true;
            Calendar3.Visible = false;
        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ThumbNail1_Click(object sender, ImageClickEventArgs e)
        {
           

        }
    }
}