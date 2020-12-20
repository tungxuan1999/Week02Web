using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Week02.Models
{
    public class RegisterModel
    {
        //public String notification;
        //[Display(Name = "")]
        //public String Notification { get { if (notification == null) return ""; else return notification; } set { notification = value; } }

        [Required(ErrorMessage = "Name Required")]
        [Display(Name = "Name *")]
        [StringLength(50, ErrorMessage = "Must be under 50 characters")]
        public string Ten { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [Display(Name = "Password *")]
        [StringLength(65, ErrorMessage = "Must be under 65 characters")]
        public string MK { get; set; }

        [Required(ErrorMessage = "Confirm Password Required")]
        [Display(Name = "Confirm Password *")]
        [StringLength(65, ErrorMessage = "Must be under 65 characters")]
        public string CFMK { get; set; }

        [Required(ErrorMessage = "Email Required")]
        [Display(Name = "Email *")]
        [StringLength(50, ErrorMessage = "Must be under 50 characters")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessage = "Not a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Birthday Required")]
        [Display(Name = "Birthday *")]
        [Column(TypeName = "date")]
        public DateTime Ngaysinh { get; set; }

        [Required(ErrorMessage = "Phone Required")]
        [Display(Name = "Phone *")]
        [StringLength(10, ErrorMessage = "Must be under 10 characters")]
        [RegularExpression("^([\\+]?(?:00)?[0-9]{1,3}[\\s.-]?[0-9]{1,12})([\\s.-]?[0-9]{1,4}?)$", ErrorMessage = "Phone not exist")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "Address Required")]
        [Display(Name = "Address *")]
        [StringLength(50, ErrorMessage = "Must be under 50 characters")]
        public string Diachi { get; set; }
    }
}