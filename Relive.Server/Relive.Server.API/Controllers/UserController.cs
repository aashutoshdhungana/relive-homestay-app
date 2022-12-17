using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Relive.Server.API.DTOs.UserDTOs;
using Relive.Server.API.Services;
using Relive.Server.Core.Interfaces;
using Relive.Server.Core.Specifications;
using Relive.Server.Core.UserAggregate;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Relive.Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly UserAuthenticationService _userAuthenticationService;
        private readonly IRepository<User> _userRepository;
        private readonly ILogger<User> _logger;
        private readonly IMapper _mapper;
        public UserController(UserAuthenticationService userAuthenticationService, IRepository<User> userRepository, IMapper mapper, ILogger<User> logger, IAuthorizationService authorizationService)
        {
            _userAuthenticationService = userAuthenticationService;
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Validation Error");
                    return BadRequest(Utilities.Utilities.GenerateValidationErrorResponse(ModelState));
                }
                BaseSpecification<User> specification = new BaseSpecification<User>(x => x.Email == userLogin.Email.ToUpper());
                var dbUser = (await _userRepository.GetAsync(specification)).FirstOrDefault();
                if (dbUser == null)
                {
                    _logger.LogError("User not found");
                    return BadRequest(Utilities.Utilities.GenerateGeneralErrorResponse(new string[] { "User with email not found" }));
                }

                string hashedPassword = _userAuthenticationService.HashPassword(userLogin.Password);
                if (!(hashedPassword == dbUser.Password))
                {
                    return BadRequest(Utilities.Utilities.GenerateGeneralErrorResponse(new string[] { "Email and Password doesnot match" }));
                }

                string token = _userAuthenticationService.GenerateToken(dbUser, UserTypes.Traveler);
                _logger.LogInformation($"{dbUser.Email} logged on");
                return Ok(new { dbUser, token });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return StatusCode(500, new string[] { "Server Error" });
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegister userRegister)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Validation error");
                    return BadRequest(Utilities.Utilities.GenerateValidationErrorResponse(ModelState));
                }
                User user = new User
                (
                    Guid.NewGuid(),
                    Utilities.Utilities.TrimAndCapitalize(userRegister.FirstName),
                    Utilities.Utilities.TrimAndCapitalize(userRegister.LastName),
                    userRegister.Email,
                    userRegister.Phone,
                    _userAuthenticationService.HashPassword(userRegister.Password),
                    userRegister.CreatedOn,
                    userRegister.CreatedBy
                );
                user.OwnerId = user.Id;
                await _userRepository.InsertAsync(user);
                await _userRepository.SaveAsync();
                _logger.LogInformation($"User {user.Email} registered");
                return Ok(new { Messgae = "Created Successfully", UserId = user.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return StatusCode(500, Utilities.Utilities.GenerateGeneralErrorResponse(new string[] { "Server Error" }));
            }
        }

        [Authorize]
        [HttpPatch]
        [Route("Edit/{id:Guid}")]
        public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody] UserUpdate user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Modeinvlaid");
                    return BadRequest(Utilities.Utilities.GenerateValidationErrorResponse(ModelState));
                }
                user.Password = (user.Password == null) ? null : _userAuthenticationService.HashPassword(user.Password);
                User dbUser = await _userRepository.GetByIdAsync(id);
                if (dbUser == null) return BadRequest("User not found");
                var authResult = await _authorizationService.AuthorizeAsync(User, dbUser, "OwnerPolicy");
                if (!authResult.Succeeded)
                {
                    return Forbid("User not authorized for the action");
                }
                _mapper.Map(user, dbUser);
                _userRepository.Update(dbUser);
                await _userRepository.SaveAsync();
                _logger.LogInformation($"{dbUser.Email} was updated");
                return Ok(dbUser);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return StatusCode(500, Utilities.Utilities.GenerateGeneralErrorResponse(new string[] { "Server Error" }));
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
                var authResult = await _authorizationService.AuthorizeAsync(User, dbUser, "OwnerPolicy");
                if (!authResult.Succeeded)
                {
                    return Forbid("User not authorized for the action");
                }
                _userRepository.Delete(dbUser);
                await _userRepository.SaveAsync();
                _logger.LogInformation($"{dbUser.Email} was deleted");
                return Ok("User deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return StatusCode(500, Utilities.Utilities.GenerateGeneralErrorResponse(new string[] { "Server Error" }));
            }
        }
    }
}
