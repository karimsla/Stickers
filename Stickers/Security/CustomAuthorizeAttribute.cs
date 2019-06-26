
using Model;
using Services.serviceAdmin;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EventWeb.Security
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



            if (_admin != null)
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
           
          
            filterContext.Result = new HttpUnauthorizedResult();
        }


    }
}