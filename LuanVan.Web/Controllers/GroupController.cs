using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuanVan.Web.Models.Domain;
using LuanVan.Web.Models.ViewModel;
using System.IO;
using LuanVan.Web.Support;

namespace LuanVan.Web.Controllers
{
    [LVAuthorize(RoleType.Teacher)]
    public class GroupController : Controller
    {
        private LuanVanDbContext db = new LuanVanDbContext();

        public ActionResult Details(int id = 0, string error = "")
        {
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }

            var response = new DetailGroupViewModel
            {
                Id = group.Id,
                Name = group.Name,
                ImageUrl = group.ImageUrl,
                LessonId = group.Lesson.Id,
                Vocabularies = db.Vocabularies.Where(v => v.Group.Id == id).ToList()
            };

            ViewBag.Error = error;

            return View(response);
        }

        //
        // GET: /Lesson/Create

        public ActionResult Create(int lessonId )
        {
            var response = new CreateGroupViewModel
            {
                LessonId = lessonId
            };

            return View(response);
        }

        //
        // POST: /Lesson/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateGroupViewModel request)
        {
            if (request.ImageFile == null)
            {
                ModelState.AddModelError("", "Image is required.");
                return View(request);
            }

            if (ModelState.IsValid)
            {
                var user = db.UserProfiles
                    .Where(u => u.UserName == User.Identity.Name)
                    .FirstOrDefault();

                var lesson = db.Lessons
                    .Where(c => c.Id == request.LessonId)
                    .FirstOrDefault();

                var group = new Group
                {
                    Name = request.Name,
                    Lesson = lesson
                };

                if (request.ImageFile != null)
                {
                    var imageId = Guid.NewGuid().ToString().Replace("-", "").ToLower();
                    var imageExtension = Path.GetExtension(request.ImageFile.FileName);

                    var imageName = imageId + imageExtension;
                    string pic = System.IO.Path.GetFileName(imageName);

                    string folderPath = "/img/group";
                    Directory.CreateDirectory(Server.MapPath("~" + folderPath));
                    string path = System.IO.Path.Combine(Server.MapPath("~" + folderPath), pic);
                    request.ImageFile.SaveAs(path);

                    group.ImageId = imageId;
                    group.ImageExtension = imageExtension;
                }

                db.Groups.Add(group);

                db.SaveChanges();

                return RedirectToAction("Details", "Lesson", new { Id = request.LessonId });
            }

            return View(request);
        }

        //
        // GET: /Lesson/Edit/5

        public ActionResult Edit(int id = 0)
        {
            var group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }

            var response = new EditGroupViewModel
            {
                Id = group.Id,
                Name = group.Name,
                ImageUrl = group.ImageUrl,
                LessonId = group.Lesson.Id
            };

            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditGroupViewModel request)
        {
            if (ModelState.IsValid)
            {
                var group = db.Groups.Find(request.Id);
                group.Name = request.Name;

                if (request.ImageFile != null)
                {
                    var imageId = Guid.NewGuid().ToString().Replace("-", "").ToLower();
                    var imageExtension = Path.GetExtension(request.ImageFile.FileName);
                    var imageName = imageId + imageExtension;
                    string pic = System.IO.Path.GetFileName(imageName);

                    string folderPath = "/img/group";
                    Directory.CreateDirectory(Server.MapPath("~" + folderPath));
                    string path = System.IO.Path.Combine(Server.MapPath("~" + folderPath), pic);
                    request.ImageFile.SaveAs(path);


                    group.ImageId = imageId;
                    group.ImageExtension = imageExtension;
                }                

                db.SaveChanges();
                return RedirectToAction("Details", "Lesson", new { Id = group.Lesson.Id });
            }
            return View(request);
        }

        //
        // GET: /Lesson/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        //
        // POST: /Lesson/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var group = db.Groups.Find(id);
            db.Groups.Remove(group);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}