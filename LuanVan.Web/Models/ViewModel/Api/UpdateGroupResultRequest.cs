using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.ViewModel.Api
{
    public class UpdateGroupResultRequest
    {
        public int studentId { get; set; }
        public List<VocabularyResult> results { get; set; }
    }

    public class VocabularyResult
    {
        public int vocabularyId { get; set; }
        public int numberOfWrongTimes { get; set; }
    }
}