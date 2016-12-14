using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.ViewModel.Report
{
    public class LessonLikeReportViewModel
    {
        public string lessonId { get; set; }
        public string lessonName { get; set; }
        public int noIdea { get; set; }
        public int like { get; set; }
        public int dislike { get; set; }
    }
}