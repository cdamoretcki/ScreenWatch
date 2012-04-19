using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace ScreenWatchUI.Models
{

    public class PercentageValidationAttribute : RegularExpressionAttribute
    {
        public PercentageValidationAttribute() : base(@"^([1-9]|[1-9][0-9]|[1][0][0])$") { }
    }

    public class ColorBoundValidationAttribute : RegularExpressionAttribute
    {
        public ColorBoundValidationAttribute() : base(@"^#?([a-f]|[A-F]|[0-9]){3}(([a-f]|[A-F]|[0-9]){3})?$") { }
    }

    public class ToneTrigger
    {
        public Guid id { get; set; }
        public String userEmail { get; set; }
		public IEnumerable<SelectListItem> userList { get; set; }
        
        [Required(ErrorMessage = "Username Required")]
        [DisplayName("Username")]
        public String userName { get; set; }

        [Required(ErrorMessage = "Lower Color Bound Required")]
        [ColorBoundValidation(ErrorMessage = "Not a valid color (format is #FFFFFF)")]
        [DisplayName("Lower Color Bound")]
        public String lowerColorBound { get; set; }

        [Required(ErrorMessage = "Upper Color Bound Required")]
        [ColorBoundValidation(ErrorMessage = "Not a valid color (format is #FFFFFF)")]
        [DisplayName("Upper Color Bound")]
        public String upperColorBound { get; set; }
        
        [Required(ErrorMessage = "Percentage of Screen Required")]
        [PercentageValidation(ErrorMessage = "Not a valid precentage (range is 1-100)")]
        [DisplayName("Percentage of Screen")]
        public String sensitivity { get; set; }
    }
}