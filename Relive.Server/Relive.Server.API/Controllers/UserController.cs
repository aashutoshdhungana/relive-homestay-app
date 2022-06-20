using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Relive.Server.API.DTOs.UserDTOs;
using System.Threading.Tasks;
using Relive.Server.Core;
using System;
using Relive.Server.API.Services;
using Relive.Server.Core.UserAggregate;
using Relive.Server.Core.Intefaces;
using Relive.Server.Core.Interfaces;
using Relive.Server.Core.Specifications;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Relive.Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserAuthenticationService _userAuthenticationService;
        private readonly IRepository<User> _userRepository;

        public UserController(UserAuthenticationService userAuthenticationService, IRepository<User> userRepository)
        {
            _userAuthenticationService = userAuthenticationService;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            BaseSpecification<User> specification = new BaseSpecification<User>(x => x.Email == userLogin.Email.ToUpper());
            var dbUser = (await _userRepository.GetAsync(specification)).FirstOrDefault();
            if (dbUser == null)
            {
                return BadRequest("User of the provided email doesnot exist");
            }
            string hashedPassword = _userAuthenticationService.HashPassword(userLogin.Password);
            if (hashedPassword == dbUser.Password)
            {
                string token = _userAuthenticationService.GenerateToken(dbUser, UserTypes.Traveler);
                return Ok(new { dbUser, token });
            }
            return BadRequest("The Email and Password cobination incorrect");
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegister userRegister)
        {
            User user = new User
            (
                Guid.NewGuid(),
                userRegister.FirstName,
                userRegister.LastName,
                userRegister.Email,
                userRegister.PhoneNumber,
                userRegister.IsTraveller,
                userRegister.IsHost,
                userRegister.IsAdmin,
                _userAuthenticationService.HashPassword(userRegister.Password)
            );
            try
            {
                await _userRepository.InsertAsync(user);
                await _userRepository.SaveAsync();
                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        public async Task<IActionResult> ChangePassword()
        {
            
        }
    }
}
