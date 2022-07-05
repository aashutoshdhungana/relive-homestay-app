using Microsoft.AspNetCore.Authorization;
using System;

namespace Relive.Server.API.Authorization.Requirements
{
    public class OwnerRequirement: IAuthorizationRequirement
    {
        // This is just a marker class for owner requirement
    }
}
