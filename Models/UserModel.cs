using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IdeaLab.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage="Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w)+)+)", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [RegularExpression(@"([6-9][0-9]{9})",ErrorMessage = "Invalid phone number" )]
        [Required(ErrorMessage = "Enter Contact Number")]
        public string ContactNumber { get; set; }
        [Required(ErrorMessage = "Enter Batch")]
        [RegularExpression(@"^([2][0]\d{2}[-][0-9]{2})", ErrorMessage = "Batch is not valid")]
        public string Batch { get; set; }
        [Required(ErrorMessage = "Select Branch")]
        public Nullable<int> BranchID { get; set; }
        [Required(ErrorMessage = "Enter Idea")]
        [RegularExpression(@"^(.{1,200})", ErrorMessage = "Only 200 characters allowed")]
        public string Idea { get; set; }

        public string Branch { get; set; }
    }
}