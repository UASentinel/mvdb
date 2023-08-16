using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvDb.Domain.Entities;

namespace MvDb.WebUI.Controllers;

public class AuthController : ApiControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    public AuthController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    [HttpGet("Roles/:id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ICollection<string>>> GetRoles(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user != null)
        {
            var roles = await _userManager.GetRolesAsync(user);
            if (roles != null)
                return Ok(roles);
        }

        return new List<string>();
    }
}
