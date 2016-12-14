using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.Domain
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //guid
        public string ImageId { get; set; }
        //.png
        public string ImageExtension { get; set; }
        [NotMapped]
        public string ImageUrl { get { return "/img/group/" + ImageId + ImageExtension; } }
        
        //Quick Navigation
        public virtual Lesson Lesson { get; set; }
    }
}