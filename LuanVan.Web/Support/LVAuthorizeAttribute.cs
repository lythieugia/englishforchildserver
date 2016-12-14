using LuanVan.Web.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LuanVan.Web.Support
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class LVAuthorizeAttribute : AuthorizeAttribute
    {
        private List<RoleType> _roleTypes { get; set; }

        public LVAuthorizeAttribute(params object[] roleTypes)
        {
            _roleTypes = new List<RoleType>();
            foreach (var item in roleTypes)
            {
                if (item.GetType().BaseType == typeof(Enum))
                {
                    _roleTypes.Add((RoleType)item);
                }                
            }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string destController = filterContext.RouteData.Values["controller"].ToString().ToLower();
            string destAction = filterContext.RouteData.Values["action"].ToString().ToLower();

            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);

            if (!skipAuthorization)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (_roleTypes.Count > 0)
                    {
                        var db = new LuanVanDbContext();
                        string roleName = db.UserProfiles.Where(u => u.UserName == HttpContext.Current.User.Identity.Name).Select(u => u.RoleName).FirstOrDefault();

                        if (!string.IsNullOrEmpty(roleName))
                        {
                            RoleType rType = (RoleType)Enum.Parse(typeof(RoleType), roleName);

                            var intersectRole = _roleTypes.Intersect(new List<RoleType>() { rType });

                            if (intersectRole.Count() == 0)
                            {
                                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", Controller = "Home" }));
                            }
                        }
                        else
                        {
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", Controller = "Home" }));
                        }
                    }
                }
                else
                {
                    base.OnAuthorization(filterContext);
                } 
            }              
        }
    }
}