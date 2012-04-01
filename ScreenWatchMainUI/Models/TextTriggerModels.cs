﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ScreenWatchUI.Models
{
    public class TriggerStringValidationAttribute : RegularExpressionAttribute
    {
        public TriggerStringValidationAttribute(): base(@"^[\w]{1}$") { }
    }

    public class TextTrigger
    {
        public Guid id { get; set; }
        public String userEmail { get; set; }

        [Required(ErrorMessage = "Username Required")]
        [DisplayName("Username")]
        public String userName { get; set; }

        [Required(ErrorMessage = "Trigger Text Required")]
        [TriggerStringValidation(ErrorMessage = "Not a valid trigger")]
        [DisplayName("Trigger Text")]
        public String triggerString { get; set; }
    }
}