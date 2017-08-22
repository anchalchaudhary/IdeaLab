using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace IdeaLab.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter LoginID")]
        public string LoginID { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        public string Password { get; set; }
    }
}