using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

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
        
        [Required(ErrorMessage = "Sensitivity Required")]
        [DisplayName("Sensitivity Bound")]
        public String sensitivity { get; set; }
    }
}