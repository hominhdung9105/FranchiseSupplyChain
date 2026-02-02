using IAM.Application.Dtos.RoleDtos;
using IAM.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IAM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("{id:guid}/activate")]
        public async Task<IActionResult> Activate(Guid id)
        {
            await _userService.ActivateUserAsync(id);
            return Ok();
        }

        [HttpPost("{id:guid}/deactivate")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            await _userService.DeactivateUserAsync(id);
            return Ok();
        }

        [HttpPost("{id:guid}/lock")]
        public async Task<IActionResult> Lock(Guid id)
        {
            await _userService.LockUserAsync(id);
            return Ok();
        }

        [HttpPost("{id:guid}/roles")]
        public async Task<IActionResult> AssignRoles(Guid id, [FromBody] AssignRolesRequestDto request)
        {
            await _userService.AssignRolesAsync(id, request);
            return Ok();
        }
    }
}
