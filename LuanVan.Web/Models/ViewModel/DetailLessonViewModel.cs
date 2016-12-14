using LuanVan.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.ViewModel
{
    public class DetailLessonViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public List<Group> Groups { get; set; }

        public List<Class> ClassLearningThisLesson { get; set; }
        public List<Class> ClassNotLearningThisLesson { get; set; }
    }
}