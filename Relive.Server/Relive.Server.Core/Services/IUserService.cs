using Relive.Server.Core.Entities.ServiceResponseAggregate;
using Relive.Server.Core.UserAggregate;
using System;

namespace Relive.Server.Core.Services
{
    public interface IUserService
    {
        ServiceResponse<object> UserRegister(User user);
        ServiceResponse<User> UserUpdate(Guid Id, User user);
        ServiceResponse<User> UserDelete(Guid Id);
        ServiceResponse<User> UserLogIn(string username, string password);
    }
}
