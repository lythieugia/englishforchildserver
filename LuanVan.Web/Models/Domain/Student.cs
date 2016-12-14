using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.Domain
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual Class Class { get; set; }

        public virtual UserProfile Parent { get; set; }

        //For quick navigation
        public virtual List<StudentVocabulary> StudentVocabularies { get; set; }
    }
}