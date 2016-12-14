using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuanVan.Web.Models.Domain;
using LuanVan.Web.Models.ViewModel;
using LuanVan.Web.Support;

namespace LuanVan.Web.Controllers
{
    [LVAuthorize(RoleType.Teacher)]
    public class StudentController : Controller
    {
        private LuanVanDbContext db = new LuanVanDbContext();

        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            var student = db.Students.Find(id);

            var parents = db.UserProfiles
                .ToList()
                .Where(p => p.RoleName == RoleType.Parent.ToString() && ((student.Parent != null && p.UserId != student.Parent.UserId) || student.Parent == null))
                .ToList();

            var response = new DetailStudentViewModel
            {
                Id = student.Id,
                Name = student.Name,
                ParentName = student.Parent != null ? student.Parent.UserName : "",

                Parents = parents
            };

            return View(response);
        }

        public ActionResult ChangeParent(int studentId, int parentId)
        {
            var student = db.Students.Find(studentId);
            var parent = db.UserProfiles.Find(parentId);

            student.Parent = parent;
            db.SaveChanges();

            return RedirectToAction("Details", new { Id = studentId });
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        public ActionResult Edit(int id = 0)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        public ActionResult Delete(int id = 0)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddLesson(int studentId)
        {
            var student = db.Students.Find(studentId);
            if (student == null)
            {
                return HttpNotFound();
            }

            var lessons = db.Lessons
                //.Where(l => !l.StudentLessons.Any(sl => sl.Student.Id == studentId))
                .ToList();

            var response = new AddLessonViewModel 
            {
                StudentId = student.Id,
                StudentName = student.Name,
                Lessons = lessons
            };

            return View(response);
        }

        public ActionResult AddStudentToLesson(int lessonId, int studentId)
        {
            var student = db.Students.Find(studentId);
            var lesson = db.Lessons.Find(lessonId);
            var studentLesson = db.StudentLessons.Where(sl => sl.Student.Id == studentId && sl.Lesson.Id == lessonId).FirstOrDefault();

            if (studentLesson == null && student != null && lesson != null)
            {
                var newStudentLesson = new StudentLesson
                {
                    Student = student,
                    Lesson = lesson
                };

                db.StudentLessons.Add(newStudentLesson);

                db.SaveChanges();

                return RedirectToAction("Details", new { id = studentId });
            }

            return HttpNotFound();
        }

        public ActionResult RemoveStudentFromLesson(int lessonId, int studentId)
        {
            var studentLesson = db.StudentLessons.Where(sl => sl.Student.Id == studentId && sl.Lesson.Id == lessonId).FirstOrDefault();

            if (studentLesson != null)
            {
                db.StudentLessons.Remove(studentLesson);

                db.SaveChanges();

                return RedirectToAction("Details", new { id = studentId });
            }

            return HttpNotFound();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}