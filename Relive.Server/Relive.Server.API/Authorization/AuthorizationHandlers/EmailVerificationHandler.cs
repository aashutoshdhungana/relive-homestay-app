using Microsoft.AspNetCore.Authorization;
using Relive.Server.API.Authorization.Requirements;
using System.Threading.Tasks;

namespace Relive.Server.API.Authorization.AuthorizationHandlers
{
    public class EmailVerificationHandler : AuthorizationHandler<EmailVerifiedRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmailVerifiedRequirement requirement)
        {
            if (context.User.HasClaim("emailverified", "true"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
