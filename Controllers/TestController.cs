using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdeaLab.Models;
namespace IdeaLab.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        DBIdeaLabEntities db = new DBIdeaLabEntities();
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Index(EventsModel model)
        {
            List<EventsModel> eventList = db.tblEvents.Select(x => new EventsModel
            {
                EventID = x.EventID,
                EventName = x.EventName,
                DateOfEvent = x.DateOfEvent,
                Details = x.Details
            }).ToList();

            ViewBag.EventList = eventList;
            return View();
        }
        //public ActionResult EventRegistration()
        //{
        //    return PartialView("EventRegistration");
        //}
        [HttpPost]
        public ActionResult EventRegistration(WebsiteModel model, int EventID)
        {
            //string test = model.events.EventID.ToString();
            List<EventsModel> eventList = db.tblEvents.Select(x => new EventsModel
            {
                EventID = x.EventID,
                EventName = x.EventName,
                DateOfEvent = x.DateOfEvent,
                Details = x.Details
            }).ToList();

            ViewBag.EventList = eventList;

            tblEventRegistration objtblEventRegistration = new tblEventRegistration();
            objtblEventRegistration.EventID = EventID;
            objtblEventRegistration.Name = model.eventregistration.Name;
            objtblEventRegistration.ContactNumber = model.eventregistration.ContactNumber;
            objtblEventRegistration.EmailID = model.eventregistration.EmailID;

            db.tblEventRegistrations.Add(objtblEventRegistration);

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}