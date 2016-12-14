using LuanVan.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.ViewModel
{
    public class DetailGroupViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int LessonId { get; set; }
        public List<Vocabulary> Vocabularies { get; set; }
    }
}