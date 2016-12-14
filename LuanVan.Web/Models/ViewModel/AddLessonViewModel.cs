using LuanVan.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.ViewModel
{
    public class AddLessonViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public List<Lesson> Lessons { get; set; }
    }
}