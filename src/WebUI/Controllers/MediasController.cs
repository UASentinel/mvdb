using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MediatR;
using MvDb.Application.Common.Models;
using MvDb.Application.Actions.Medias.Commands.Create;
using MvDb.Application.Actions.Medias.Commands.AddGenres;
using MvDb.Application.Actions.Medias.Queries.Get;
using MvDb.Application.Actions.Medias.Queries.GetById;
using MvDb.Application.Actions.Medias.Commands.Update;
using MvDb.Application.Actions.Medias.Commands.Delete;
using MvDb.Application.Actions.Medias.Queries.Search;
using MvDb.Application.Actions.Medias.Commands.UpdateDirectors;
using MvDb.Application.Actions.Medias.DataTransferObjects.Objects;
using MvDb.Application.Actions.Medias.Commands.UpdateActors;
using MvDb.Application.Actions.Medias.Commands.UpdateSeasonsOrder;

namespace MvDb.WebUI.Controllers;

[Authorize(Roles = "Administrator")]
public class MediasController : ApiControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ICollection<MediaDto>>> Get()
    {
        var medias = await Mediator.Send(new GetMediasQuery());

        return Ok(medias);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<MediaDto>> Get(int id)
    {
        return await Mediator.Send(new GetMediaByIdQuery(id));
    }

    [HttpPost("Search")]
    [AllowAnonymous]
    public async Task<ActionResult<ICollection<MediaDto>>> Search(SearchMediasQuery query)
    {
        var medias = await Mediator.Send(query);

        return Ok(medias);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromForm] CreateMediaCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(int id, [FromForm] UpdateMediaCommand command)
    {
        if (id != command.MediaId)
            return BadRequest();

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPut("Genres/Update/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> UpdateGenres(int id, UpdateGenresCommand command)
    {
        if (id != command.MediaId)
            return BadRequest();

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPut("Directors/Update/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> UpdateDirectors(int id, UpdateDirectorsCommand command)
    {
        if (id != command.MediaId)
            return BadRequest();

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPut("Actors/Update/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> UpdateActors(int id, UpdateActorsCommand command)
    {
        if (id != command.MediaId)
            return BadRequest();

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPut("Seasons/Reorder/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> ReorderSeasons(int id, UpdateSeasonsOrderCommand command)
    {
        if (id != command.MediaId)
            return BadRequest();

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteMediaCommand(id));

        return NoContent();
    }
}
