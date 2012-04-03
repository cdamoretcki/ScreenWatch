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
            if (!IsPostBack)
            {
                Meow();
            }
		}

        private void Meow()
        {
            ThumbNailRepeater.DataSource = new List<ScreenShot>()
                {
                    new ScreenShot
                    {
                         filePath = @"~\Images\Sleeping-cat.jpg", 
                         thumbnailFilePath = @"~\Images\sleeping-cat-thumb.jpg"                    
                    }                
                };
            ThumbNailRepeater.DataBind();
        }

        protected void SelectDate(object sender, EventArgs e)
		{
            DateTime date;
            DateTime.TryParse(DateTextBox.Text, out date);
			ScreenShotActions data = new ScreenShotActions();
			List<ScreenShot> screenShots = data.getScreenShotsByDateRange(date, date.AddDays(1));
            if (screenShots.Count > 0)
            {
                ThumbNailRepeater.DataSource = screenShots;
                ThumbNailRepeater.DataBind();
                foreach (var screenShot in screenShots)
                {
                    screenShot.image.Dispose();
                    screenShot.thumbnail.Dispose();
                }
            }
            else
            {
                Meow();
            }
		}
	}
}