using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MediatR;
using MvDb.Application.Common.Models;
using MvDb.Application.Actions.Actors.Commands.Create;
using MvDb.Application.Actions.Actors.Commands.Delete;
using MvDb.Application.Actions.Actors.Commands.Update;
using MvDb.Application.Actions.Actors.DataTransferObjects;
using MvDb.Application.Actions.Actors.Queries.Get;
using MvDb.Application.Actions.Actors.Queries.GetById;
using MvDb.Application.Actions.Actors.Queries.Search;
using Microsoft.AspNetCore.Identity;
using MvDb.Domain.Entities;

namespace MvDb.WebUI.Controllers;

//[Authorize(Roles = "Administrator")]
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
