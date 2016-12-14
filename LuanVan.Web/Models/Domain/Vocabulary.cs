using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.Domain
{
    public class Vocabulary
    {
        public int Id { get; set; }
        public string Word { get; set; }

        //guid
        public string ImageId { get; set; }
        //.png
        public string ImageExtension { get; set; }
        [NotMapped]
        public string ImageUrl { get { return "/img/vocabulary/" + ImageId + ImageExtension; } }

        public virtual Group Group { get; set; }
    }
}