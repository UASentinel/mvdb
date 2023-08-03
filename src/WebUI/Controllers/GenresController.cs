using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MediatR;
using MvDb.Application.Common.Models;
using MvDb.Application.Actions.Genres.Commands.Create;
using MvDb.Application.Actions.Genres.Commands.Delete;
using MvDb.Application.Actions.Genres.Commands.Update;
using MvDb.Application.Actions.Genres.Queries.GetById;
using MvDb.Application.Actions.Genres.Queries.Get;
using MvDb.Application.Actions.Genres.DataTransferObjects;
using MvDb.Application.Actions.Actors.Commands.Update;

namespace MvDb.WebUI.Controllers;

//[Authorize(Roles = "Administrator")]
public class GenresController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ICollection<GenreDto>>> Get()
    {
        var genres = await Mediator.Send(new GetGenresQuery());

        return Ok(genres);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GenreDto>> Get(int id)
    {
        return await Mediator.Send(new GetGenreByIdQuery(id));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateGenreCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(int id, UpdateGenreCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteGenreCommand(id));

        return NoContent();
    }
}
