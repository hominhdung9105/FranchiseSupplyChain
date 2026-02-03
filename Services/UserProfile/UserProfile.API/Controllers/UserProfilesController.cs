using Microsoft.AspNetCore.Mvc;
using UserProfile.Application.Dtos;
using UserProfile.Application.Interfaces;

namespace UserProfile.API.Controllers
{
    [ApiController]
    [Route("api/user-profiles")]
    public sealed class UserProfilesController : ControllerBase
    {
        private readonly IUserProfileService _service;

        public UserProfilesController(IUserProfileService service)
        {
            _service = service;
        }

        [HttpGet("{userId:guid}")]
        public async Task<ActionResult<UserProfileDto>> GetById(Guid userId, CancellationToken cancellationToken)
        {
            var result = await _service.GetByIdAsync(userId, cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<UserProfileDto>>> List([FromQuery] int skip = 0, [FromQuery] int take = 20, CancellationToken cancellationToken = default)
        {
            var result = await _service.ListAsync(skip, take, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<UserProfileDto>> Create([FromBody] CreateUserProfileRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var created = await _service.CreateAsync(request, cancellationToken);
                return CreatedAtAction(nameof(GetById), new { userId = created.UserId }, created);
            }
            catch (InvalidOperationException)
            {
                return Conflict();
            }
        }

        [HttpPut("{userId:guid}")]
        public async Task<ActionResult<UserProfileDto>> Update(Guid userId, [FromBody] UpdateUserProfileRequest request, CancellationToken cancellationToken)
        {
            var updated = await _service.UpdateAsync(userId, request, cancellationToken);
            return updated is null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> Delete(Guid userId, CancellationToken cancellationToken)
        {
            var deleted = await _service.DeleteAsync(userId, cancellationToken);
            return deleted ? NoContent() : NotFound();
        }
    }
}
