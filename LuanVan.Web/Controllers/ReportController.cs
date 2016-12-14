using LuanVan.Web.Models.Domain;
using LuanVan.Web.Models.ViewModel.Report;
using LuanVan.Web.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuanVan.Web.Controllers
{
    [LVAuthorize]
    public class ReportController : Controller
    {
        private LuanVanDbContext db = new LuanVanDbContext();

        #region Report Page
        [LVAuthorize(RoleType.Teacher, RoleType.Parent)]
        public ActionResult Index()
        {
            string roleName = db.UserProfiles.Where(u => u.UserName == User.Identity.Name).Select(u => u.RoleName).FirstOrDefault();
            RoleType rType = (RoleType)Enum.Parse(typeof(RoleType), roleName);

            if (rType == RoleType.Parent)
            {
                return RedirectToAction("Student");
            }

            return View(rType);
        }

        [LVAuthorize(RoleType.Teacher)]
        public ActionResult lesson()
        {
            return View();
        }

        [LVAuthorize(RoleType.Teacher, RoleType.Parent)]
        public ActionResult Student()
        {
            string roleName = db.UserProfiles.Where(u => u.UserName == User.Identity.Name).Select(u => u.RoleName).FirstOrDefault();
            RoleType rType = (RoleType)Enum.Parse(typeof(RoleType), roleName);

            ViewBag.RoleType = rType.ToString();

            return View();
        }

        [LVAuthorize(RoleType.Teacher)]
        public ActionResult WhoLikeLesson()
        {
            return View();
        }
        #endregion Report Page

        public JsonResult GetLessonReport()
        {
            var lessons = db.Lessons
                .Where(l => l.UserProfile != null && l.UserProfile.UserName == User.Identity.Name)
                .ToList();

            var response = lessons.Select(l =>
                    new LessonReportViewModel
                    { 
                        lessonId = l.Id.ToString(),
                        lessonName = l.Name,
                        students = db.StudentLessons.Where(sl => sl.Lesson.Id == l.Id).Select(sl => sl.Student.Name).ToList(),
                        wrongTimes = db.StudentLessons.Where(sl => sl.Lesson.Id == l.Id).ToList().Select(sl => (sl.Student.StudentVocabularies.Where(sv => sv.IsFinished).Sum(r => r.NumberOfWrongTimes)).ToString()).ToList()
                    })
                .ToList();

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStudentReport()
        {
            string roleName = db.UserProfiles.Where(u => u.UserName == User.Identity.Name).Select(u => u.RoleName).FirstOrDefault();
            RoleType rType = (RoleType)Enum.Parse(typeof(RoleType), roleName);

            List<StudentLesson> studentLessons = null;

            if (rType == RoleType.Parent)
            {
                studentLessons = db.StudentLessons
                    .Where(sl => sl.Student.Parent != null && sl.Student.Parent.UserName == User.Identity.Name)
                    .ToList();
            }
            else
            {
                studentLessons = db.StudentLessons
                    .Where(sl => sl.Lesson.UserProfile != null && sl.Lesson.UserProfile.UserName == User.Identity.Name)
                    .ToList();
            }            

            var response = studentLessons
                .Select(sl =>
                    {
                        var stuResults = sl.Student.StudentVocabularies
                            .Where(sv => sv.IsFinished)
                            .GroupBy(sr => sr.UpdatedDate)
                            .Select(sr => new { date = sr.Key.ToShortDateString(), numberOfWords = sr.Count().ToString() })
                            .OrderBy(sr => sr.date)
                            .ToList();

                        return new StudentReportViewModel
                        {
                            studentId = sl.Student.Id.ToString(),
                            studentName = sl.Student.Name,
                            dates = stuResults.Select(sr => sr.date).ToList(),
                            practicedWords = stuResults.Select(sr => sr.numberOfWords).ToList()
                        };

                    }).ToList();

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLessonLikeReport()
        {
            var studentLessons = db.StudentLessons
                .Where(sll => sll.Lesson.UserProfile.UserName == User.Identity.Name)
                .ToList();

            var response = studentLessons
                .GroupBy(sl => sl.Lesson)
                .Select(sl =>
                    {
                        int numberOfLike = sl.Where(slr => slr.Feeling == 1).Count();
                        int numberOfDislike = sl.Where(slr => slr.Feeling == 2).Count();
                        int numberOfNoIdea = sl.Where(slr => slr.Feeling == 0).Count();

                        return new LessonLikeReportViewModel
                        {
                            lessonId = sl.Key.Id.ToString(),
                            lessonName = sl.Key.Name,
                            like = numberOfLike,
                            dislike = numberOfDislike,
                            noIdea = numberOfNoIdea
                        };
                    })
                .ToList();

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
