using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdeaLab.Models;
namespace IdeaLab.Controllers
{
    public class HomeController : Controller
    {
        DBIdeaLabEntities db = new DBIdeaLabEntities();
        // GET: Home
        public ActionResult Index()
        {
            Session["LoggedIn"] = null;

            //List of Branches
            List<tblBranch> list = db.tblBranches.ToList();
            ViewBag.BranchList = new SelectList(list, "BranchID", "Branch");

            //Get top 3 events from farthest date in descending order
            List<EventsModel> eventList = db.tblEvents.OrderByDescending(x => x.DateOfEvent).Select(x => new EventsModel
            {
                EventID = x.EventID,
                EventName = x.EventName,
                DateOfEvent = x.DateOfEvent,
                Details = x.Details,
                ImageID = x.ImageID
            }).Take(3).ToList();

            ViewBag.EventList = eventList;

            return View();
        }
        [HttpPost]
        public ActionResult Register(WebsiteModel model)    //for user to register and give his idea
        {
            if (ModelState.IsValid)
            {
                tblUser objtblUser = new tblUser();
                objtblUser.Name = model.user.Name;
                objtblUser.Email = model.user.Email;
                objtblUser.ContactNumber = model.user.ContactNumber;
                objtblUser.Batch = model.user.Batch;
                objtblUser.BranchID = model.user.BranchID;
                objtblUser.Idea = model.user.Idea;

                db.tblUsers.Add(objtblUser);

                db.SaveChanges();

                TempData["Successful"] = "<script>alert('Your response has been recorded');</script>";
            }
            else
                TempData["Unsuccessful"] = "<script>alert('There was some error. Try again.');</script>";

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult EventRegistration(WebsiteModel model, int EventID)  //for user to register for an upcoming event
        {
            List<EventsModel> eventList = db.tblEvents.Select(x => new EventsModel
            {
                EventID = x.EventID,
                EventName = x.EventName,
                DateOfEvent = x.DateOfEvent,
                Details = x.Details
            }).ToList();

            ViewBag.EventList = eventList;
            if (model != null)
            {
                tblEventRegistration objtblEventRegistration = new tblEventRegistration();
                objtblEventRegistration.EventID = EventID;
                objtblEventRegistration.Name = model.eventregistration.Name;
                objtblEventRegistration.ContactNumber = model.eventregistration.ContactNumber;
                objtblEventRegistration.EmailID = model.eventregistration.EmailID;

                db.tblEventRegistrations.Add(objtblEventRegistration);

                db.SaveChanges();
                TempData["Registered"] = "<script>alert('You have been registered.');</script>";
            }
            else
                TempData["Unregistered"] = "<script>alert('There was some error. Try again.');</script>";

            return RedirectToAction("Index");
        }

        public FileContentResult RetrieveImage(int ImageID) //display image of event by accessing it from backend
        {
            tblUploadedImage image = db.tblUploadedImages.FirstOrDefault(p => p.ImageID == ImageID);
            if (image != null)
            {
                return File(image.ImageByte, "image/jpg");
            }
            else
            {
                return null;
            }
        }
    }
}