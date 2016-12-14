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
    public class LessonController : Controller
    {
        private LuanVanDbContext db = new LuanVanDbContext();

        public ActionResult Index()
        {
            var lessons = db.Lessons
                .Where(c => c.UserProfile != null && c.UserProfile.UserName == User.Identity.Name)
                .ToList();

            return View(lessons);
        }

        public ActionResult Details(int id = 0)
        {
            var lesson = db.Lessons.Find(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }

            var classLearningThisLesson = db.ClassLessons.ToList()
                .Where(cl => cl.Lesson.Id == id)
                .Select(cl => cl.Class)
                .OrderBy(c => c.Name)
                .ToList();

            var classNotLearningThisLesson = db.Classes.ToList()
                .Where(c => !classLearningThisLesson.Exists(c2 => c2.Id == c.Id))
                .OrderBy(c => c.Name)
                .ToList();

            var response = new DetailLessonViewModel
            {
                Id = lesson.Id,
                Name = lesson.Name,
                ImageUrl = lesson.ImageUrl,
                Groups = db.Groups.Where(g => g.Lesson.Id == id).ToList(),

                ClassLearningThisLesson = classLearningThisLesson,
                ClassNotLearningThisLesson = classNotLearningThisLesson
            };

            return View(response);
        }

        public ActionResult AddClass(int lessonId, int classId)
        {
            //1. Create new ClassLesson
            var les = db.Lessons.Find(lessonId);
            var cls = db.Classes.Find(classId);

            if (les != null && cls != null)
            {
                var newClsLes = new ClassLesson
                {
                    Class = cls,
                    Lesson = les
                };

                db.ClassLessons.Add(newClsLes);

                var students = db.Students
                    .Where(s => s.Class.Id == classId)
                    .ToList();

                //2. Create StudentLesson for all students in the class
                foreach (var stu in students)
                {                    
                    var newStuLes = new StudentLesson
                    {
                        Student = stu,
                        Lesson = les
                    };

                    db.StudentLessons.Add(newStuLes);

                    //3. Create StudentGroup for all students in the class
                    var groups = db.Groups
                        .Where(g => g.Lesson.Id == lessonId)
                        .ToList();

                    foreach(var grp in groups)
                    {
                        var newStuGrp = new StudentGroup
                        {
                            Student =  stu,
                            Group = grp
                        };

                        db.StudentGroups.Add(newStuGrp);

                        //4. Create StudentVocabulary for all students in the class
                        var vocabularies = db.Vocabularies
                        .Where(v => v.Group.Id == grp.Id)
                        .ToList();

                        foreach (var voca in vocabularies)
                        {
                            var newStuVocabulary = new StudentVocabulary
                            {
                                Student = stu,
                                Vocabulary = voca,
                                UpdatedDate = DateTime.Now
                            };

                            db.StudentVocabularies.Add(newStuVocabulary);
                        }
                    }
                }

                

                db.SaveChanges();
            }

            return RedirectToAction("Details", new { Id = lessonId });
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateLessonViewModel request)
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

                var lesson = new Lesson
                {
                    Name = request.Name,
                    UserProfile = user
                };

                if (request.ImageFile != null)
                {
                    var imageId = Guid.NewGuid().ToString().Replace("-", "").ToLower();
                    var imageExtension = Path.GetExtension(request.ImageFile.FileName);

                    var imageName = imageId + imageExtension;
                    string pic = System.IO.Path.GetFileName(imageName);

                    string folderPath = "/img/lesson";
                    Directory.CreateDirectory(Server.MapPath("~" + folderPath));
                    string path = System.IO.Path.Combine(Server.MapPath("~" + folderPath), pic);
                    request.ImageFile.SaveAs(path);

                    lesson.ImageId = imageId;
                    lesson.ImageExtension = imageExtension;
                }

                db.Lessons.Add(lesson);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(request);
        }

        public ActionResult Edit(int id = 0)
        {
            var lesson = db.Lessons.Find(id);

            if (lesson == null)
            {
                return HttpNotFound();
            }

            var response = new EditLessonViewModel
            {
                Id = lesson.Id,
                Name = lesson.Name,
                ImageUrl = lesson.ImageUrl
            };

            return View(response);
        }

        //
        // POST: /Course/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditLessonViewModel request)
        {
            if (ModelState.IsValid)
            {
                var lesson = db.Lessons.Find(request.Id);
                lesson.Name = request.Name;

                if (request.ImageFile != null)
                {
                    var imageId = Guid.NewGuid().ToString().Replace("-", "").ToLower();
                    var imageExtension = Path.GetExtension(request.ImageFile.FileName);
                    var imageName = imageId + imageExtension;
                    string pic = System.IO.Path.GetFileName(imageName);

                    string folderPath = "/img/lesson";
                    Directory.CreateDirectory(Server.MapPath("~" + folderPath));
                    string path = System.IO.Path.Combine(Server.MapPath("~" + folderPath), pic);
                    request.ImageFile.SaveAs(path);

                    lesson.ImageId = imageId;
                    lesson.ImageExtension = imageExtension;
                }                

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(request);
        }

        //public ActionResult Delete(int id = 0)
        //{
        //    Course course = db.Courses.Find(id);
        //    if (course == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(course);
        //}

        ////
        //// POST: /Course/Delete/5

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Course course = db.Courses.Find(id);
        //    db.Courses.Remove(course);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}