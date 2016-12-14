using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.ViewModel.Api
{
    public class GroupVocabularyViewModel
    {
        public int groupId { get; set; }
        public string groupName { get; set; }
        public List<VocabularyInGroupViewModel> vocabularies { get; set; }
    }

    public class VocabularyInGroupViewModel
    {
        public string word { get; set; }
        public string imageId { get; set; }
        public string imageUrl { get; set; }
    }
}