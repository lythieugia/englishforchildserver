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
    public class ClassController : Controller
    {
        private LuanVanDbContext db = new LuanVanDbContext();

        //
        // GET: /Class/

        public ActionResult Index()
        {
            return View(db.Classes.ToList());
        }

        //
        // GET: /Class/Details/5

        public ActionResult Details(int id = 0)
        {
            var cls = db.Classes.Find(id);
            if (cls == null)
            {
                return HttpNotFound();
            }

            var listStuInClass = db.Students
                .Where(s => s.Class.Id == id)
                .OrderBy(s => s.Name)
                .ToList();

            var listStuNotInAnyClass = db.Students
                .ToList()
                .Where(s => s.Class == null)
                .OrderBy(s => s.Name)
                .ToList();

            var canEditClass = db.ClassLessons
                .Where(cl => cl.Class.Id == id)
                .FirstOrDefault();

            var response = new DetailClassViewModel
            {
                Id = id,
                Name = cls.Name,

                StudentsInClass = listStuInClass,
                StudentsNotInAnyClass = listStuNotInAnyClass,

                CanEdit = canEditClass == null ? true : false
            };

            ViewBag.RemoveStudentError = TempData["RemoveStudentError"] != null ? TempData["RemoveStudentError"] : "";

            return View(response);
        }

        public ActionResult AddStudent(int classId, int studentId)
        {
            var cls = db.Classes.Find(classId);
            var stu = db.Students.Find(studentId);

            if (cls != null && stu != null)
            {
                stu.Class = cls;

                db.SaveChanges();
            }            

            return RedirectToAction("Details", new { Id = classId });
        }

        public ActionResult RemoveStudent(int classId, int studentId)
        {
            var student = db.Students.ToList().Where(s => s.Class != null && s.Class.Id == classId && s.Id == studentId).FirstOrDefault();

            if (student != null)
            {
                student.Class = null;
                db.SaveChanges();
            }

            return RedirectToAction("Details", new { Id = classId });
        }

        //
        // GET: /Class/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Class/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Class cls)
        {
            if (ModelState.IsValid)
            {
                db.Classes.Add(cls);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cls);
        }

        //
        // GET: /Class/Edit/5

        public ActionResult Edit(int id = 0)
        {
            var cls = db.Classes.Find(id);
            if (cls == null)
            {
                return HttpNotFound();
            }
            return View(cls);
        }

        //
        // POST: /Class/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Class cls)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cls).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cls);
        }

        //
        // GET: /Class/Delete/5

        public ActionResult Delete(int id = 0)
        {
            var cls = db.Classes.Find(id);
            if (cls == null)
            {
                return HttpNotFound();
            }
            return View(cls);
        }

        //
        // POST: /Class/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var cls = db.Classes.Find(id);
            db.Classes.Remove(cls);
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