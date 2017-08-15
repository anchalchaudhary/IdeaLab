using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdeaLab.Models
{
    public class WebsiteModel
    {
        public EventRegistrationModel eventregistration { get; set; }
        public EventsModel events { get; set; }
        public UserModel user { get; set; }
        public ImageStoreModel image { get; set; }
    }
}