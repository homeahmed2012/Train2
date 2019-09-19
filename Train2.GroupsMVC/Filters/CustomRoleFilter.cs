using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using Train2.Business;

namespace Train2.GroupsMVC.Filters
{
    public class CustomRoleFilter : AuthorizeAttribute
    {
        private bool _failedRoleValidation;
        private UserService userService = new UserService();
        public string Role { get; set; }
        
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //User is not in one of the applicable roles
            bool hasRole = userService.UserHasRole(httpContext.User.Identity.Name, Role);
            if (!hasRole)
            {
                _failedRoleValidation = true;
                return false;
            }
            _failedRoleValidation = false;
            return true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (_failedRoleValidation)
            {
                filterContext.Result = new ViewResult { ViewName = "AccessError" };
            }
        }
    }
}