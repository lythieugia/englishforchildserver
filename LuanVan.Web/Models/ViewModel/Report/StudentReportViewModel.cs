using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.ViewModel.Report
{
    public class StudentReportViewModel
    {
        public string studentId { get; set; }
        public string studentName { get; set; }
        public List<string> dates { get; set; }
        public List<string> practicedWords { get; set; }
    }

    public class StudentResultReportViewModel
    {
        public string date { get; set; }
        public string numberOfWords { get; set; }
    }
}