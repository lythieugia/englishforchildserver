using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.Domain
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //guid
        public string ImageId { get; set; }
        //.png
        public string ImageExtension { get; set; }
        [NotMapped]
        public string ImageUrl { get { return "/img/lesson/" + ImageId + ImageExtension; } }        

        public virtual UserProfile UserProfile { get; set; }
    }
}