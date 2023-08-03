using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MediatR;
using MvDb.Application.Common.Models;
using MvDb.Application.Actions.Episodes.Commands.Create;
using MvDb.Application.Actions.Episodes.Commands.Update;
using MvDb.Application.Actions.Episodes.DataTransferObjects;
using MvDb.Application.Actions.Episodes.Queries.Get;
using MvDb.Application.Actions.Episodes.Queries.GetById;
using MvDb.Application.Actions.Episodes.Commands.Delete;

namespace MvDb.WebUI.Controllers;

//[Authorize(Roles = "Administrator")]
public class EpisodesController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ICollection<EpisodeDto>>> Get()
    {
        var genres = await Mediator.Send(new GetEpisodesQuery());

        return Ok(genres);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EpisodeDto>> Get(int id)
    {
        return await Mediator.Send(new GetEpisodeByIdQuery(id));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateEpisodeCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(int id, UpdateEpisodeCommand command)
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
        await Mediator.Send(new DeleteEpisodeCommand(id));

        return NoContent();
    }
}
