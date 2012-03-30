using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ScreenWatchUI.Models
{
    public class EmailValidationAttribute : RegularExpressionAttribute
    {
        public EmailValidationAttribute() : base(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$") { }
    }

    public class User
    {
        [Required(ErrorMessage = "Username Required")]
        [DisplayName("Username")]
        public String userName { get; set; }
        
        [Required(ErrorMessage = "E-Mail Address Required")]
        [EmailValidation(ErrorMessage = "Not a valid E-Mail Address")]
        [DisplayName("E-Mail Address")]
        public String email { get; set; }

        [DisplayName("Is This User Monitored?")]
        public Boolean isMonitored { get; set; }

        [DisplayName("Is This User An Administrator?")]
        public Boolean isAdmin { get; set; }    
    }
}