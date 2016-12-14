using LuanVan.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.ViewModel
{
    public class DetailStudentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ParentName { get; set; }

        public List<UserProfile> Parents { get; set; }
    }
}