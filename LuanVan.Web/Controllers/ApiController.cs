using LuanVan.Web.Models.Domain;
using LuanVan.Web.Models.ViewModel.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuanVan.Web.Controllers
{
    public class ApiController : Controller
    {
        private LuanVanDbContext db = new LuanVanDbContext();

        public ActionResult Index()
        {
            return Json(new { name = HttpContext.User.Identity.Name }, JsonRequestBehavior.AllowGet);
        }

        #region student
        public JsonResult GetStudentByName(string name)
        {
            ResponseViewModel response = null;

            if (!string.IsNullOrEmpty(name))
            {
                var student = db
                .Students
                .Where(s => s.Name.Replace(" ", "").ToLower() == name.Replace(" ", "").ToLower())
                .FirstOrDefault();

                //Name: TRAN VAN A, TRANVANA
                if (student != null)
                {
                    if (student.Class != null)
                    {
                        response = new ResponseViewModel()
                        {
                            ok = true,
                            message = "success",
                            result = new StudentViewModel
                            {
                                studentId = student.Id,
                                studentName = student.Name,
                                classId = student.Class.Id,
                                className = student.Class.Name
                            }
                        };                        
                    }
                    else
                    {
                        response = new ResponseViewModel
                        {
                            ok = false,
                            message = "Student is not in any class."
                        };
                    }

                    return Json(response, JsonRequestBehavior.AllowGet);
                }
            }

            response = new ResponseViewModel
            {
                ok = false,
                message = "Missing Student Name!"
            };

            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion student

        #region New APIs
        public JsonResult GetAllLessonByStudentId(int studentId)
        {
            var listLesson = db.StudentLessons
                    .Where(sl => sl.Student.Id == studentId)
                    .Select(sl => sl.Lesson)
                    .ToList();

            var response = new ResponseViewModel()
            {
                ok = true,
                message = "success",
                result = listLesson
            };

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllGroupByLessonId(int lessonId, int studentId)
        {
            var listGroup = db.StudentGroups
                    .Where(sg => sg.Group.Lesson.Id == lessonId && sg.Student.Id == studentId)
                    .ToList()
                    .Select(sg => new { Group = sg.Group, IsFinished = sg.IsFinished })
                    .ToList();

            var response = new ResponseViewModel()
            {
                ok = true,
                message = "success",
                result = listGroup
            };

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllVocabularyByGroupId(int groupId)
        {
            var listVocabulary = db.Vocabularies
                .Where(v => v.Group.Id == groupId)
                .ToList();

            var response = new ResponseViewModel
            {
                ok = true,
                message = "success",
                result = listVocabulary
            };

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateResult(UpdateGroupResultRequest request)
        {
            ResponseViewModel response = null;

            var student = db.Students.Find(request.studentId);

            if (student != null)
            {
                if (request.results != null)
                {
                    //1. Update all StudentVocabularies
                    var currentGroupId = 0;
                    foreach (var vResult in request.results)
                    {
                        var sVobulary2 = db.StudentVocabularies
                            .ToList();

                        var sVobulary = db.StudentVocabularies
                            .ToList()
                            .Where(sv => sv.Vocabulary.Id == vResult.vocabularyId && sv.Student.Id == request.studentId)
                            .FirstOrDefault();

                        if (sVobulary != null)
                        {
                            sVobulary.NumberOfWrongTimes = vResult.numberOfWrongTimes;
                            sVobulary.UpdatedDate = DateTime.Now;
                            sVobulary.IsFinished = true;
                        }

                        if (currentGroupId == 0)
                        {
                            currentGroupId = sVobulary.Vocabulary.Group.Id;
                        }
                    }

                    //2. Because all StudentVocabulary are finished => StudentGroup is finished
                    var sGroup = db.StudentGroups
                        .Where(sg => sg.Group.Id == currentGroupId && sg.Student.Id == request.studentId)
                        .FirstOrDefault();

                    sGroup.IsFinished = true;

                    db.SaveChanges();

                    response = new ResponseViewModel()
                    {
                        ok = true,
                        message = "Success"
                    };
                }
                else
                {
                    response = new ResponseViewModel()
                    {
                        ok = false,
                        message = "results cannot be null."
                    };
                }
            }
            else
            {
                response = new ResponseViewModel()
                {
                    ok = false,
                    message = "Student Not found!"
                };
            }

            return Json(response);
        }
        #endregion New APIs

        [HttpPost]
        public JsonResult StudentLikeLesson(int studentId, int lessonId, int feeling)
        {
            ResponseViewModel response = null;

            var student = db.Students.Find(studentId);
            var lesson = db.Lessons.Find(lessonId);

            var studentLesson = db.StudentLessons.Where(sl => sl.Student.Id == studentId && sl.Lesson.Id == lessonId).FirstOrDefault();

            if (studentLesson != null)
            {
                studentLesson.Feeling = feeling;

                db.SaveChanges();

                response = new ResponseViewModel
                {
                    ok = true,
                    message = "Success"
                };
            }
            else
            {
                response = new ResponseViewModel
                {
                    ok = false,
                    message = "Invalid input"
                };
            }

            return Json(response);
        }

        //public JsonResult GetGroupVocabularyByImageId(string imageId)
        //{
        //    ResponseViewModel response = null;

        //    //1. Get Group By ImageUrl
        //    var group = db.Groups
        //        .Where(g => g.Vocabularies.Any(v => v.ImageId == imageId))
        //        .FirstOrDefault();

        //    //2. Check if it is the top unfinished group of a lesson
        //    if (group != null)
        //    {
        //        //3. Get All Images In A Group then Return
        //        response = new ResponseViewModel
        //        {
        //            ok = true,
        //            message = "success",
        //            result = new GroupVocabularyViewModel
        //            {
        //                groupId = group.Id,
        //                groupName = group.Name,
        //                vocabularies = group.Vocabularies
        //                    .Select(v => 
        //                        new VocabularyInGroupViewModel
        //                        { 
        //                            word = v.Word, 
        //                            imageId = v.ImageId, 
        //                            imageUrl = v.ImageUrl
        //                        })
        //                    .ToList()
        //            }
        //        };
        //    }
        //    else
        //    {
        //        response = new ResponseViewModel
        //        {
        //            ok = false,
        //            message = "Image does not exist."
        //        };
        //    }

        //    return Json(response, JsonRequestBehavior.AllowGet);
        //}

        

        //private void SetCurrentLearning(int currentGroupId)
        //{
        //    var studentLesson = db.StudentLessons
        //        .Where(sl => sl.Lesson.Groups.Any(g => g.Id == currentGroupId))
        //        .FirstOrDefault();

        //    studentLesson.CurrentGroupId = currentGroupId;

        //    var allStudentLessons = db.StudentLessons.ToList();
        //    foreach (var item in allStudentLessons)
        //    {
        //        item.CurrentGroupId = 0;
        //    }

        //    db.SaveChanges();
        //}

        //public JsonResult GetLastFinishedGroup(int studentId)
        //{
        //    ResponseViewModel response = null;

        //    var studentLesson = db.StudentLessons
        //        //.Where(sl => sl.CurrentGroupId != 0)
        //        .FirstOrDefault();

        //    if (studentLesson != null)
        //    {
        //        var currentGroup = db.Groups.Find(studentLesson.CurrentGroupId);
        //        var result = new CurrentGroupViewModel
        //        {
        //            LessonId = currentGroup.Lesson.Id,
        //            LessonName = currentGroup.Lesson.Name,
        //            GroupId = currentGroup.Id,
        //            GroupName = currentGroup.Name
        //        };
        //        response = new ResponseViewModel
        //        {
        //            ok = true,
        //            message = "Exist",
        //            result = result
        //        };
        //    }
        //    else
        //    {
        //        response = new ResponseViewModel
        //        {
        //            ok = false,
        //            message = "No last unfinshed group"
        //        };
        //    }        

        //    return Json(response, JsonRequestBehavior.AllowGet);
        //}

        
    }
}
