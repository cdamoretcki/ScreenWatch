using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace ScreenWatchUI.Models
{
    public class User : ScreenWatchData.User
    {
        [DisplayName("User Name")]
        public String userName { get; set; }
        
        [DisplayName("E-Mail Address")]
        public String email { get; set; }

        [DisplayName("Monitored?")]
        public Boolean isMonitored { get; set; }

        [DisplayName("Adminsitrator?")]
        public Boolean isAdmin { get; set; }    
    }
}