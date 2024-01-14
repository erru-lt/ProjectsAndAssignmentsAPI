using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjectsAndNotesAPI.Data.Identity
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequiresRoleAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _roleName;  

        public RequiresRoleAttribute(string roleName)
        {
            _roleName = roleName;    
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(!context.HttpContext.User.IsInRole(_roleName))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
