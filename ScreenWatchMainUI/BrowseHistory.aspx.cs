using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ScreenWatchData;
using ScreenWatchUI;
using System.Text;
using System.IO;

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

		protected void ThumbNail1_Click(object sender, EventArgs e)
        {


        }
		protected void Button1_Click(object sender, EventArgs e)
		{

			DateTime getDate2;
			DateTime getDate3;

			DateTime.TryParse(TextBox2.Text, out getDate2);
			DateTime.TryParse(TextBox3.Text, out getDate3);

			ScreenShotActions SSA = new ScreenShotActions();

			List<ScreenShot> lstOfScreenShots = new List<ScreenShot>();

			lstOfScreenShots = SSA.getScreenShotsByDateRange(getDate2, getDate3);

			for (int i = 0; i < lstOfScreenShots.Count; i++)
			{
				ImageButton btn = new ImageButton();
				string MyEvent = "ThumbNail1_Click";
				string MyId = "ThumbNail" + (i + 1).ToString();
				btn.ImageUrl = lstOfScreenShots[i].thumbnailFilePath;
				btn.Attributes.Add("runat", "server");
				btn.ID = MyId;                
				btn.Height = Unit.Pixel(60);
				btn.Width = Unit.Pixel(140);
				btn.Attributes.Add("OnClick", MyEvent);
				ThumbNailHeader.Controls.Add(btn);
			}
		}

			 
	   
		protected void ClearDates_Click(object sender, EventArgs e)
		{
			TextBox2.Text = "";
			TextBox3.Text = "";
		
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