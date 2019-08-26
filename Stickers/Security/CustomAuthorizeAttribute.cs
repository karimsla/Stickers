
using Model;
using Services.serviceAdmin;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Stickers.Security
{
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Method)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {

        
        private string[] allowedroles;
        public CustomAuthorizeAttribute(params string[] Roles)
        {
            this.allowedroles = (string[])Roles.Clone();
        }


        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
        

            IserviceAdmin spa = new serviceAdmin();
            
            IPrincipal user = httpContext.User;
            bool authorize = false;
           


            string userid = user.Identity.Name;
            Admin _admin = spa.Get(x=>x.username==userid);

            user.IsInRole(_admin.type);

            if (_admin != null && Roles.Contains(_admin.type))
            {
                authorize = true;
            }
                



               
            return authorize;
        }

           

        
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            //{
            //    filterContext.Result = new ViewResult()
            //    {
            //        ViewName = "~/Home/Unauthorized"
            //    };
            //}

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(new
                 RouteValueDictionary(new { controller = "Admin", action = "login" }));
            }
            // filterContext.Result = new HttpUnauthorizedResult();
           
            else 
            {
                filterContext.Result = new RedirectToRouteResult(new
                 RouteValueDictionary(new { controller = "Home", action = "Unauthorized" }));
            }
        }


    }
}