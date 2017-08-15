using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace IdeaLab.Models
{
    public class EventsModel
    {
        public int EventID { get; set; }
        [Required(ErrorMessage ="Event Name required")]
        public string EventName { get; set; }
        [Required(ErrorMessage = "Event Date is required")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DateOfEvent { get; set; }
        [Required(ErrorMessage = "Event Deatils are required")]
        public string Details { get; set; }
        public Nullable<int> ImageID { get; set; }
        //public HttpPostedFileWrapper ImageFile { get; set; }
        //public FileContentResult ImageFile { get; set; }
        public byte[] ImageByte { get; set; }
    }
}