using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuanVan.Web.Models.ViewModel.Api
{
    public class ResponseViewModel
    {
        public bool ok { get; set; }
        public string message { get; set; }
        public object result { get; set; }
    }
}