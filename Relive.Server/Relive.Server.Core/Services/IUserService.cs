using Relive.Server.Core.Entities.ServiceResponseAggregate;
using Relive.Server.Core.UserAggregate;
using System;
using System.Threading.Tasks;

namespace Relive.Server.Core.Services
{
    public interface IUserService
    {
        ServiceResponse<User> UserRegister(User user);
        ServiceResponse<User> UserUpdate(Guid Id, User user);
        ServiceResponse<User> UserDelete(Guid Id);
        Task<ServiceResponse<object>> UserLogIn(string username, string password);
    }
}
