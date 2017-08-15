using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using IdeaLab.Models;

namespace IdeaLab.Controllers
{
    public class AdminController : Controller
    {
        DBIdeaLabEntities db = new DBIdeaLabEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(EventsModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.EventID > 0) //EDITS THE EXISTING RECORD IN tblTasks
                {
                    tblEvent objtblEventUpdated = db.tblEvents.SingleOrDefault(x => x.EventID == model.EventID);

                    objtblEventUpdated.EventName = model.EventName;
                    objtblEventUpdated.DateOfEvent = model.DateOfEvent;
                    objtblEventUpdated.Details = model.Details;

                    db.SaveChanges();

                    TempData["updated"] = "<script>alert('Event Updated');</script>";

                    //return RedirectToAction("ViewEvents");

                }
                else //new Record
                {
                    HttpPostedFileBase file = Request.Files["ImageData"];
                    int imgID = UploadImageInDataBase(file);
                    if (imgID != 0)
                    {

                        if (model.DateOfEvent >= DateTime.Now.Date)
                        {
                            tblEvent objtblEvent = new tblEvent();
                            objtblEvent.EventName = model.EventName;
                            objtblEvent.DateOfEvent = model.DateOfEvent;
                            objtblEvent.Details = model.Details;
                            objtblEvent.ImageID = imgID;

                            db.tblEvents.Add(objtblEvent);
                            db.SaveChanges();

                            TempData["msg"] = "<script>alert('Event Added');</script>";
                        }
                        else
                        {
                            ViewBag.DateError = "Invalid Date";
                            return View();
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }
        public int UploadImageInDataBase(HttpPostedFileBase file)
        {
            ImageStoreModel imagestoremodel = new ImageStoreModel();
            imagestoremodel.ImageByte = ConvertToBytes(file);

            tblUploadedImage objtblUploadedImage = new tblUploadedImage();
            objtblUploadedImage.ImageByte = imagestoremodel.ImageByte;

            db.tblUploadedImages.Add(objtblUploadedImage);

            int i = db.SaveChanges();
            int imageID = objtblUploadedImage.ImageID;
            if (i == 1)
            {
                return imageID;
            }
            else
            {
                return 0;
            }
        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            Stream stream = image.InputStream;
            BinaryReader reader = new BinaryReader(stream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }
        public ActionResult ViewEvents(EventsModel model)
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
        public ActionResult EditEvent(int EventID)
        {
            EventsModel model = new EventsModel();
            if(EventID>0)
            {
                tblEvent objtblEvent = db.tblEvents.SingleOrDefault(x => x.EventID == EventID);
                model.EventID = objtblEvent.EventID;
                model.EventName = objtblEvent.EventName;
                model.DateOfEvent = objtblEvent.DateOfEvent;
                model.Details = objtblEvent.Details;
            }
            return PartialView("EditEventPartial", model);
        }
        public JsonResult DeleteEvent(int EventID)
        {
            bool result = false;

            tblEvent objtblEvent = db.tblEvents.SingleOrDefault(x => x.EventID == EventID);
            if (objtblEvent != null)
            {
                result = true;
                db.tblEvents.Remove(objtblEvent);
            }
            db.SaveChanges();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewEventRegistrations(EventRegistrationModel model)
        {
            List<EventRegistrationModel> eventRegList = db.tblEventRegistrations.Select(x => new EventRegistrationModel
            {
                RegistrationID = x.RegistrationID,
                EventName = x.tblEvent.EventName,
                Name = x.Name,
                ContactNumber = x.ContactNumber,
                EmailID = x.EmailID
            }).ToList();

            ViewBag.EventRegList = eventRegList;
            return View();
        }
        public JsonResult DeleteEventReg(int RegistrationID)
        {
            bool result = false;

            tblEventRegistration objtblEventRegistration = db.tblEventRegistrations.SingleOrDefault(x => x.RegistrationID == RegistrationID);
            if (objtblEventRegistration != null)
            {
                result = true;
                db.tblEventRegistrations.Remove(objtblEventRegistration);
            }
            db.SaveChanges();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewIdeas(UserModel model)
        {
            List<UserModel> userList = db.tblUsers.Select(x => new UserModel
            {
                ID = x.ID,
                Name = x.Name,
                Batch = x.Batch,
                Branch = x.tblBranch.Branch,
                ContactNumber = x.ContactNumber,
                Email = x.Email,
                Idea = x.Idea
            }).ToList();

            ViewBag.UserList = userList;
            return View();
        }
        public JsonResult DeleteIdea(int ID)
        {
            bool result = false;

            tblUser objtblUser = db.tblUsers.SingleOrDefault(x => x.ID == ID);
            if (objtblUser != null)
            {
                result = true;
                db.tblUsers.Remove(objtblUser);
            }
            db.SaveChanges();

            return Json(result, JsonRequestBehavior.AllowGet);
        }       
    }
}