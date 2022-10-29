using Relive.Server.Core.Constants;
using Relive.Server.Core.Entities.ServiceResponseAggregate;
using Relive.Server.Core.Interfaces;
using Relive.Server.Core.Services;
using Relive.Server.Core.Specifications;
using Relive.Server.Core.UserAggregate;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Relive.Server.API.Services
{
    public class UserServices : IUserService
    {
        private readonly IRepository<User> _userRepo;
        private readonly UserAuthenticationService _userAuth;

        public UserServices(IRepository<User> userRepo, UserAuthenticationService userAuth)
        {
            _userRepo = userRepo;
            _userAuth = userAuth;
        }
        public ServiceResponse<User> UserDelete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<object>> UserLogIn(string email, string password)
        {
            ServiceResponse<object> response = null;
            BaseSpecification<User> specification = new BaseSpecification<User>(x => x.Email == email);
            
            var dbUser = (await _userRepo.GetAsync(specification)).FirstOrDefault();
            if (dbUser == null)
            {
                response = new ServiceResponse<object>()
                {
                    Code = AppCodes.UserNotFound,
                    Message = AppMessages.UserNotFound,
                    HttpCode = HttpStatusCode.OK,
                    Data = null
                };
                return response;
            }
            bool isPasswordCorrect = _userAuth.CompareHash(password, dbUser.Password);
            if (!isPasswordCorrect)
            {
                response = new ServiceResponse<object>()
                {
                    Code = AppCodes.PasswordIncorrect,
                    Message = AppMessages.PasswordIncorrect,
                    HttpCode = HttpStatusCode.OK,
                    Data = null
                };
                return response;
            }

            string token = _userAuth.GenerateToken(dbUser, UserTypes.Traveler);
            response = new ServiceResponse<object>()
            {
                Code = AppCodes.Success,
                Message = AppMessages.UserLogInSuccess,
                HttpCode = HttpStatusCode.OK,
                Data = new
                {
                    dbUser,
                    token
                }
            };
            
            return response;
        }

        public ServiceResponse<User> UserRegister(User user)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<User> UserUpdate(Guid Id, User user)
        {
            throw new NotImplementedException();
        }
    }
}
