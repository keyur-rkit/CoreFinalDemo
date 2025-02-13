using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Security.Claims;

namespace CoreFinalDemo.Filters
{
    /// <summary>
    /// JWT Authorization filter for role-based access control.
    /// </summary>
    public class JWTAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;

        public JWTAuthorizationFilter(params string[] roles)
        {
            _roles = roles.Length > 0 ? roles : null;
        }

        /// <summary>
        /// Called to perform authorization.
        /// </summary>
        /// <param name="context">The authorization filter context.</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Bypass authorization if [AllowAnonymous] is present
            bool hasAllowAnonymous = context.ActionDescriptor.EndpointMetadata
                .OfType<AllowAnonymousAttribute>().Any();

            if (hasAllowAnonymous)
            {
                return; // Skip authorization
            }

            ClaimsPrincipal user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new ObjectResult("Unauthorized: Token is missing or invalid.")
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized
                }; // 401
                return;
            }

            string roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            //// for debugging
            //foreach (var claim in user.Claims)
            //{
            //    Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
            //}

            if (roleClaim == null || (_roles != null && !_roles.Contains(roleClaim)))
            {
                context.Result = new ObjectResult("Forbidden: Access Denied.")
                {
                    StatusCode = (int)HttpStatusCode.Forbidden
                }; // 403
            }
        }
    }
}