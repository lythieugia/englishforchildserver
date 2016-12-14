using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.ViewModel.Api
{
    public class CurrentGroupViewModel
    {
        public int LessonId { get; set; }
        public string LessonName { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
    }
}