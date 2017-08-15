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
            List<tblBranch> list = db.tblBranches.ToList();
            ViewBag.BranchList = new SelectList(list, "BranchID", "Branch");

             List<EventsModel> eventList = db.tblEvents.OrderByDescending(x=> x.DateOfEvent).Select(x => new EventsModel
            {
                EventID = x.EventID,
                EventName = x.EventName,
                DateOfEvent = x.DateOfEvent,
                Details = x.Details,
                ImageID = x.ImageID
             }).Take(3).ToList();

            ViewBag.EventList = eventList;

            //List<tblUploadedImage> imagelist = db.tblUploadedImages.ToList();
            //ViewBag.ImageList = db.tblUploadedImages.Select(x => new ImageStoreModel { ImageID = x.ImageID, ImageByte = x.ImageByte }).ToList();

            return View();
        }
        [HttpPost]
        public ActionResult Register(WebsiteModel model)
        {
            if(ModelState.IsValid)
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
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult EventRegistration(WebsiteModel model, int EventID)
        {
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

        public FileContentResult RetrieveImage(int ImageID)
        {
            tblUploadedImage image = db.tblUploadedImages.FirstOrDefault(p => p.ImageID == ImageID);
            if (image != null)
            {
                return File(image.ImageByte,"image/jpg");
            }
            else
            {
                return null;
            }
        }
    }
}