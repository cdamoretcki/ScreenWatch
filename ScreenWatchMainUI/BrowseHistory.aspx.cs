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

            }
		}

        protected void SelectDate(object sender, EventArgs e)
		{
            DateTime date;
            DateTime.TryParse(DateTextBox.Text, out date);
			ScreenShotActions data = new ScreenShotActions();
			List<ScreenShot> lstOfScreenShots = data.getScreenShotsByDateRange(date, date.AddDays(1));

            ThumbNailRepeater.DataSource = lstOfScreenShots;
            ThumbNailRepeater.DataBind();
            foreach (var screenShot in lstOfScreenShots)
            {
                screenShot.image.Dispose();
                screenShot.thumbnail.Dispose();
            }
		}
	}
}