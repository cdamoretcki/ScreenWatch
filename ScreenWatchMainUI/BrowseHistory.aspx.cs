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
			
		}

        protected void SelectDate(object sender, EventArgs e)
		{
            DateTime date;
            DateTime.TryParse(DateTextBox.Text, out date);
			ScreenShotActions data = new ScreenShotActions();
			List<ScreenShot> lstOfScreenShots = data.getScreenShotsByDateRange(date, date.AddDays(1));

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

        protected void ThumbNail1_Click(object sender, EventArgs e)
        {

        }
	}
}