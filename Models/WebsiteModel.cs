using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdeaLab.Models
{
    //this model contains reference to other models and is used to access multiple models in the same  view
    public class WebsiteModel
    {
        public EventRegistrationModel eventregistration { get; set; }
        public EventsModel events { get; set; }
        public UserModel user { get; set; }
        public ImageStoreModel image { get; set; }
        public VideosModel videos { get; set; }
    }
}