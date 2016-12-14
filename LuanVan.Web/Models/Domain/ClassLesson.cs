using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.Domain
{
    public class ClassLesson
    {
        public int Id { get; set; }

        public virtual Class Class { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}