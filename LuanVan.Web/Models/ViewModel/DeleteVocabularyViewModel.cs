using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.ViewModel
{
    public class DeleteVocabularyViewModel
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string ImageUrl { get; set; }
        public int GroupId { get; set; }
    }
}