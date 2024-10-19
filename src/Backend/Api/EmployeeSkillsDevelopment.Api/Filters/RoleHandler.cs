using EmployeeSkillsDevelopment.Core.Interfaces;
using EmployeeSkillsDevelopment.Core.Services;
using EmployeeSkillsDevelopment.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace EmployeeSkillsDevelopment.Api.Filters
{
    public class RoleHandler : AuthorizationHandler<RoleRequirement>
    {
        private readonly IUserService _userService;

        public RoleHandler(IUserService userService)
        {
            _userService = userService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            //var objectId = context.User.FindFirst("oid")?.Value; // Get user ID from token
            var url = "http://schemas.microsoft.com/identity/claims/objectidentifier";
            var objectId = context.User.FindFirst(url)?.Value;

            if (objectId == null)
            {
                context.Fail();
                return Task.FromResult(0);
            }
            var roleResponse = _userService.GetUserRole(objectId);

            if (roleResponse.Success && roleResponse.Data != null)
            {
                // Check if the user role matches any of the required roles
                if (requirement.AllowedRoles.Contains(roleResponse.Data))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
            else
            {
                context.Fail();
            }
            return Task.FromResult(0);
        }
    }

}
