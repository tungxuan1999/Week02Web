using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Week02.Models
{
    public class LoginModel
    {
        //public String notification;
        //[Display(Name = "")]
        //public String Notification { get { if (notification == null) return ""; else return notification; } set { notification = value; } }

        [Required(ErrorMessage = "Email Required")]
        [Display(Name = "Email *")]
        [StringLength(50, ErrorMessage = "Must be under 50 characters")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessage = "Not a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [Display(Name = "Password *")]
        [StringLength(65, ErrorMessage = "Must be under 65 characters")]
        public string MK { get; set; }
    }
}