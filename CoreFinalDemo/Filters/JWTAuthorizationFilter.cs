using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Security.Claims;

namespace CoreFinalDemo.Filters
{
    public class JWTAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;

        public JWTAuthorizationFilter(params string[] roles)
        {
            _roles = roles.Length > 0 ? roles : null;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            ClaimsPrincipal user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (roleClaim == null || (_roles != null && !_roles.Contains(roleClaim)))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
