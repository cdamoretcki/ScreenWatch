﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ScreenWatchData;

namespace ParentsEyes
{
    public partial class _Default : System.Web.UI.Page
    {
        protected String returnValue = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            /**
             * IS THIS DEAD CODE?
             */

            /*ScreenWatchData.ScreenShotActions screenShotActions = new ScreenWatchData.ScreenShotActions();
            String id = TextBox1.Text;

            String imagePath = screenShotActions.getImagePath(id);
            if (imagePath == null || imagePath == String.Empty)
            {
                DisplayImage.ImageUrl = @"~\Images\notFound.jpg";
            }
            else
            {
                DisplayImage.ImageUrl = imagePath;
            }*/

        }

    }
}
