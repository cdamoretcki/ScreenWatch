using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace ScreenWatchUI.Models
{
    public class TriggerStringValidationAttribute : RegularExpressionAttribute
    {
        public TriggerStringValidationAttribute() : base(@"^[\S]+$") { }
    }

    public class TextTrigger
    {
        public Guid id { get; set; }
        public String userEmail { get; set; }
        public IEnumerable<SelectListItem> userList { get; set; }

        [Required(ErrorMessage = "Username Required")]
        [DisplayName("Username")]
        public String userName { get; set; }

        [Required(ErrorMessage = "Trigger Text Required")]
        [TriggerStringValidation(ErrorMessage = "Not a valid trigger string")]
        [DisplayName("Trigger Text")]
        public String triggerString { get; set; }

    }
}