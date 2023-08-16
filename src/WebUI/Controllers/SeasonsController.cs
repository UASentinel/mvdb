using Microsoft.AspNetCore.Mvc;
using MvDb.Application.Actions.Seasons.Commands.Create;
using MvDb.Application.Actions.Seasons.Commands.Delete;
using MvDb.Application.Actions.Seasons.Commands.Update;
using MvDb.Application.Actions.Seasons.Commands.UpdateEpisodesOrder;
using MvDb.Application.Actions.Seasons.DataTransferObjects;
using MvDb.Application.Actions.Seasons.Queries.Get;
using MvDb.Application.Actions.Seasons.Queries.GetById;
using Microsoft.AspNetCore.Authorization;

namespace MvDb.WebUI.Controllers;

[Authorize(Roles = "Administrator")]
public class SeasonsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ICollection<SeasonDto>>> Get()
    {
        var genres = await Mediator.Send(new GetSeasonsQuery());

        return Ok(genres);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SeasonDto>> Get(int id)
    {
        return await Mediator.Send(new GetSeasonByIdQuery(id));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromForm] CreateSeasonCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(int id, [FromForm] UpdateSeasonCommand command)
    {
        if (id != command.SeasonId)
            return BadRequest();

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteSeasonCommand(id));

        return NoContent();
    }

    [HttpPut("Episodes/Reorder/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> ReorderEpisodes(int id, UpdateEpisodesOrderCommand command)
    {
        if (id != command.SeasonId)
            return BadRequest();

        await Mediator.Send(command);

        return NoContent();
    }
}
