using LuanVan.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.ViewModel
{
    public class DetailClassViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> StudentsInClass { get; set; }
        public List<Student> StudentsNotInAnyClass { get; set; }
        public bool CanEdit { get; set; }
    }
}