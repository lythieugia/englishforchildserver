using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.Domain
{
    public class StudentGroup
    {
        public int Id { get; set; }

        public virtual Student Student { get; set; }
        public virtual Group Group { get; set; }

        public bool IsFinished { get; set; }
    }
}