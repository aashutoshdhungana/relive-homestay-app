using Microsoft.AspNetCore.Authorization;

namespace Relive.Server.API.Authorization.Requirements
{
    public class EmailVerifiedRequirement: IAuthorizationRequirement 
    {
        // This is a marker class for email verification requirement
    }
}
