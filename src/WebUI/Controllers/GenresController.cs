using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MediatR;
using MvDb.Application.Common.Models;
using MvDb.Application.Genres.Commands.Create;

namespace MvDb.WebUI.Controllers;

//[Authorize(Roles = "Administrator")]
public class GenresController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateGenreCommand command)
    {
        return await Mediator.Send(command);
    }
}
