using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.ViewModel.Report
{
    public class LessonReportViewModel
    {
        public string lessonId { get; set; }
        public string lessonName { get; set; }
        public List<string> students { get; set; }
        public List<string> wrongTimes { get; set; }
    }
}