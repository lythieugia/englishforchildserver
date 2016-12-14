using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.ViewModel.Api
{
    public class StudentViewModel
    {
        public int studentId { get; set; }
        public string studentName { get; set; }
        public int classId { get; set; }
        public string className { get; set; }
    }
}