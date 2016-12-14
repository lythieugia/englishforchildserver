using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.ViewModel
{
    public class EditLessonViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string ImageUrl { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}