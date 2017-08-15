using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IdeaLab.Models
{
    public class EventRegistrationModel
    {
        public int RegistrationID { get; set; }
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Contact Number")]
        [RegularExpression(@"([6-9][0-9]{9})", ErrorMessage = "Invalid phone number")]
        public string ContactNumber { get; set; }
        [Required(ErrorMessage = "Enter Email ID")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w)+)+)", ErrorMessage = "Email is not valid")]
        public string EmailID { get; set; }
        public Nullable<int> EventID { get; set; }
        public string EventName { get; set; }

    }
}