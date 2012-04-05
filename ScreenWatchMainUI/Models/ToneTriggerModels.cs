using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace ScreenWatchUI.Models
{

    public class ToneTrigger
    {
        public Guid id { get; set; }
        public String userEmail { get; set; }

        [Required(ErrorMessage = "Username Required")]
        [DisplayName("Username")]
        public String userName { get; set; }

        [Required(ErrorMessage = "Lower Color Bound Required")]
        [DisplayName("Lower Color Bound")]
        public String lowerColorBound { get; set; }

        [Required(ErrorMessage = "Upper Color Bound Required")]
        [DisplayName("Upper Color Bound")]
        public String upperColorBound { get; set; }
        
        [Required(ErrorMessage = "Percentage of Screen Required")]
        [DisplayName("Percentage of Screen")]
        public String sensitivity { get; set; }

        public IEnumerable<SelectListItem> userList { get; set; }
    }
}