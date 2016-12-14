using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.ViewModel
{
    public class CreateGroupViewModel
    {
        [Required]
        public string Name { get; set; }        

        public HttpPostedFileBase ImageFile { get; set; }

        public int LessonId { get; set; }
    }
}