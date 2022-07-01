using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Relive.Server.API.DTOs.UserDTOs;
using System.Threading.Tasks;
using Relive.Server.Core;
using System;
using Relive.Server.API.Services;
using Relive.Server.Core.UserAggregate;
using Relive.Server.Core.Interfaces;
using Relive.Server.Core.Specifications;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace Relive.Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserAuthenticationService _userAuthenticationService;
        private readonly IRepository<User> _userRepository;
        private readonly ILogger<User> _logger;
        private readonly IMapper _mapper;
        public UserController(UserAuthenticationService userAuthenticationService, IRepository<User> userRepository,IMapper mapper, ILogger<User> logger)
        {
            _userAuthenticationService = userAuthenticationService;
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            try
            {
                BaseSpecification<User> specification = new BaseSpecification<User>(x => x.Email == userLogin.Email.ToUpper());
                var dbUser = (await _userRepository.GetAsync(specification)).FirstOrDefault();
                if (dbUser == null)
                {
                    return BadRequest("User of the provided email doesnot exist");
                }
                string hashedPassword = _userAuthenticationService.HashPassword(userLogin.Password);
                if (!(hashedPassword == dbUser.Password))
                {
                    return BadRequest("The Email and Password cobination incorrect");
                }
                string token = _userAuthenticationService.GenerateToken(dbUser, UserTypes.Traveler);
                _logger.LogInformation($"{dbUser.Email} logged on");
                return Ok(new { dbUser, token });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return StatusCode(500, "Server Error");
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegister userRegister)
        {
            try
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
                await _userRepository.InsertAsync(user);
                await _userRepository.SaveAsync();
                _logger.LogInformation($"User {user.Email} registered");
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return StatusCode(500, "Server Error");
            }
        }

        [HttpPatch]
        [Route("Edit/{id:Guid}")]
        public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody] UserUpdate user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Modelstate invlaid");
                    return BadRequest();
                }
                user.Password = (user.Password == null) ? null : _userAuthenticationService.HashPassword(user.Password);
                User dbUser = await _userRepository.GetByIdAsync(id);
                if (dbUser == null) return BadRequest("User not found");
                _mapper.Map(user, dbUser);
                _userRepository.Update(dbUser);
                await _userRepository.SaveAsync();
                _logger.LogInformation($"{dbUser.Email} was updated");
                return Ok(dbUser);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return StatusCode(500,"Server Error");
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("Delete/{id:Guid}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            try
            {
                User dbUser = await _userRepository.GetByIdAsync(id);
                if (dbUser == null) return BadRequest("User not found");
                _userRepository.Delete(dbUser);
                await _userRepository.SaveAsync();
                _logger.LogInformation($"{dbUser.Email} was deleted");
                return Ok("User deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return StatusCode(500, "Server Error");
            }
        }
    }
}
