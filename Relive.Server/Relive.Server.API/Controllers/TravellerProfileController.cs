using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        readonly IAuthorizationService _authorizationService;
        readonly IMapper _mapper;

        public TravellerProfileController(IRepository<TravellerProfile> travellerRepo, IAuthorizationService authorizationService, IMapper mapper)
        {
            _travellerRepository = travellerRepo;
            _authorizationService = authorizationService;
            _mapper = mapper;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> CreateProfile([FromBody] TravellerDTO travellerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Utilities.Utilities.GenerateValidationErrorResponse(ModelState));
                }

                TravellerProfile travellerProfile = _mapper.Map<TravellerDTO, TravellerProfile>(travellerDto);
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
        public async Task<IActionResult> GetProfileById(Guid Id)
        {
            try
            {
                TravellerProfile profile = await _travellerRepository.GetByIdAsync(Id);
                if (profile == null)
                {
                    return NotFound();
                }
                TravellerDTO travellerDTO = _mapper.Map<TravellerProfile, TravellerDTO>(profile);
                return Ok(travellerDTO);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [AllowAnonymous]
        [Route("User/{UserId:Guid}")]
        [HttpGet]
        public async Task<IActionResult> GetProfileByUserId(Guid UserId)
        {
            try
            {
                TravellerProfile profile = await _travellerRepository.GetByOwnerIdAsync(UserId);
                if (profile == null)
                {
                    return NotFound();
                }
                TravellerDTO travellerDTO = _mapper.Map<TravellerProfile, TravellerDTO>(profile);
                return Ok(travellerDTO);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [Route("Edit/User/{UserId:Guid}")]
        [HttpPatch]
        public async Task<IActionResult> EditProfileByUserId(Guid UserId, [FromBody] TravellerEdit travellerEdit)
        {
            try
            {
                TravellerProfile dbProfile = await _travellerRepository.GetByOwnerIdAsync(UserId);
                if (dbProfile == null)
                {
                    return BadRequest("Profile not found");
                }
                var authResult = await _authorizationService.AuthorizeAsync(User, dbProfile, "OwnerPolicy");
                if (!authResult.Succeeded)
                {
                    return Forbid("Bearer");
                }
                _mapper.Map(travellerEdit, dbProfile);
                _travellerRepository.Update(dbProfile);
                var saveTask =  _travellerRepository.SaveAsync();
                TravellerDTO travellerDTO = _mapper.Map<TravellerProfile, TravellerDTO>(dbProfile);
                await saveTask;
                return Ok(travellerDTO);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPatch]
        [Route("Edit/{Id:Guid}")]
        public async Task<IActionResult> EditById(Guid Id, TravellerEdit travellerEdit)
        {
            TravellerProfile dbProfile = await _travellerRepository.GetByIdAsync(Id);
            if (dbProfile == null)
            {
                return BadRequest("Profile not found");
            }
            var authResult = await _authorizationService.AuthorizeAsync(User, dbProfile, "OwnerPolicy");
            if (!authResult.Succeeded)
            {
                return Forbid("Bearer");
            }
            _mapper.Map(travellerEdit, dbProfile);
            _travellerRepository.Update(dbProfile);
            var saveTask = _travellerRepository.SaveAsync();
            TravellerDTO travellerDTO = _mapper.Map<TravellerProfile, TravellerDTO>(dbProfile);
            await saveTask;
            return Ok(travellerDTO);
        }
        [Route("Delete/{Id:Guid}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteProfileById(Guid Id)
        {
            try
            {
                TravellerProfile profile = await _travellerRepository.GetByIdAsync(Id);
                if (profile == null)
                {
                    return NotFound();
                }
                await _travellerRepository.DeleteAsync(profile);
                await _travellerRepository.SaveAsync();
                return Ok("Profile Created Successfuly");
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [Route("Delete/User/{UserId:Guid}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteProfileByUserId(Guid UserId)
        {
            try
            {
                TravellerProfile profile = await _travellerRepository.GetByOwnerIdAsync(UserId);
                if (profile == null)
                {
                    return NotFound();
                }
                await _travellerRepository.DeleteAsync(profile);
                await _travellerRepository.SaveAsync();
                return Ok("Delete Successfuly");
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
