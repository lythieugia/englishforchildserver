using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.Domain
{
    public class StudentVocabulary
    {
        public int Id { get; set; }

        public virtual Student Student { get; set; }
        public virtual Vocabulary Vocabulary { get; set; }

        public bool IsFinished { get; set; }

        public int NumberOfWrongTimes { get; set; }
        public DateTime UpdatedDate { get; set; }               
    }
}