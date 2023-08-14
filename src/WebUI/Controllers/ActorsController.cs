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

namespace MvDb.WebUI.Controllers;

//[Authorize(Roles = "Administrator")]
public class ActorsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ICollection<ActorDto>>> Get()
    {
        var genres = await Mediator.Send(new GetActorsQuery());

        return Ok(genres);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ActorDto>> Get(int id)
    {
        return await Mediator.Send(new GetActorByIdQuery(id));
    }

    [HttpPost("Search")]
    public async Task<ActionResult<ICollection<ActorDto>>> Search(SearchActorsQuery query)
    {
        var medias = await Mediator.Send(query);

        return Ok(medias);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromForm] CreateActorCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(int id, [FromForm] UpdateActorCommand command)
    {
        if (id != command.ActorId)
            return BadRequest();

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteActorCommand(id));

        return NoContent();
    }
}
