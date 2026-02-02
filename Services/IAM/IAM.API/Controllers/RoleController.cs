using IAM.Application.Dtos.RoleDtos;
using IAM.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace IAM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateRoleRequestDto request)
        {
            var response = await _roleService.CreateRoleAsync(request);
            return Ok(response);
        }

        [HttpPost("{id:guid}/permissions")]
        public async Task<IActionResult> AssignPermissions(Guid id, [FromBody] AssignPermissionsRequestDto request)
        {
            await _roleService.AssignPermissionsAsync(id, request);
            return Ok();
        }
    }
}
