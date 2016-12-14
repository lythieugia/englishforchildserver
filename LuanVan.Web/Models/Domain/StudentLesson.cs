using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.Domain
{
    public class StudentLesson
    {
        public int Id { get; set; }

        public virtual Student Student { get; set; }
        public virtual Lesson Lesson { get; set; }

        public bool IsFinished { get; set; }

        //0: NoIdea, 1: Like, 2: DisLike
        public int Feeling { get; set; }
    }
}