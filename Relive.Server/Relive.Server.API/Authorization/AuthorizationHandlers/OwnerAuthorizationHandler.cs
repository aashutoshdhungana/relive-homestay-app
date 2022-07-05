using Microsoft.AspNetCore.Authorization;
using Relive.Server.API.Authorization.Requirements;
using Relive.Server.Core.Entities;
using System.Threading.Tasks;

namespace Relive.Server.API.Authorization.AuthorizationHandlers
{
    public class OwnerAuthorizationHandler : AuthorizationHandler<OwnerRequirement, BaseEntity>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnerRequirement requirement, BaseEntity resource)
        {
            if (context.User.HasClaim("userid", resource.Id.ToString()))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
