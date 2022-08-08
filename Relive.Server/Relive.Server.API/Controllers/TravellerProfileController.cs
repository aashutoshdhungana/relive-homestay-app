using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Relive.Server.API.DTOs.ProfileDTOs.Traveller;
using Relive.Server.Core.Entities.ProfileAggregate;
using Relive.Server.Core.Interfaces;
using Relive.Server.Core.UserAggregate;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Relive.Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TravellerProfileController : ControllerBase
    {
        readonly IRepository<TravellerProfile> _travellerRepository;
        readonly IRepository<User> _userRepository;
        readonly IAuthorizationService _authorizationService;
        readonly IMapper _mapper;

        public TravellerProfileController(IRepository<TravellerProfile> travellerRepo, IRepository<User> userRepo, IAuthorizationService authorizationService, IMapper mapper)
        {
            _travellerRepository = travellerRepo;
            _userRepository = userRepo;
            _authorizationService = authorizationService;
            _mapper = mapper;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> CreateProfile([FromBody] TravellerCreate travellerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Utilities.Utilities.GenerateValidationErrorResponse(ModelState));
                }

                TravellerProfile travellerProfile = _mapper.Map<TravellerCreate, TravellerProfile>(travellerDto);
                travellerProfile.OwnerId = travellerDto.UserId;
                // This check ensures the user is already created (valid token cannot exist without user)
                var authResult = await _authorizationService.AuthorizeAsync(User, travellerProfile, "OwnerPolicy");
                if (!authResult.Succeeded)
                {
                    return Forbid("Bearer");
                }

                travellerProfile.Id = Guid.NewGuid();
                travellerProfile.CreatedBy = User.Identity.Name;
                travellerProfile.CreatedOn = DateTime.UtcNow;
                travellerProfile.DisplayPicture = Utilities.Utilities.SaveImage(travellerProfile.Id, "DisplayPicture", travellerDto.DisplayPicture);
                await _travellerRepository.InsertAsync(travellerProfile);
                await _travellerRepository.SaveAsync();
                return Ok(travellerProfile);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [AllowAnonymous]
        [Route("{Id:Guid}")]
        [HttpGet]
        public IActionResult GetProfileById()
        {
            return Ok("Profile found");
        }

        [Route("Edit/{Id:Guid}")]
        [HttpPatch]
        public IActionResult EditProfile()
        {
            return Ok("Profile Edited Successfuly");
        }

        [Route("Delete/{Id:Guid}")]
        [HttpDelete]
        public IActionResult DeleteProfile()
        {
            return Ok("Profile Created Successfuly");
        }
    }
}
