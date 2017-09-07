using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IdeaLab.Models
{
    public class VideosModel
    {
        public int VideoID { get; set; }
        [Required(ErrorMessage ="Link is required")]
        public string VideoLink { get; set; }
    }
}