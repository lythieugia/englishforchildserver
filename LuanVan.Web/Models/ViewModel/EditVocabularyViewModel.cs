using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.ViewModel
{
    public class EditVocabularyViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Word { get; set; }
        public string ImageUrl { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public int GroupId { get; set; }
    }
}