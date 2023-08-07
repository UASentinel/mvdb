using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MediatR;
using MvDb.Application.Common.Models;
using MvDb.Application.Actions.Medias.Commands.Create;
using MvDb.Application.Actions.Medias.Commands.AddGenres;
using MvDb.Application.Actions.Medias.DataTransferObjects;
using MvDb.Application.Actions.Medias.Queries.Get;
using MvDb.Application.Actions.Medias.Queries.GetById;
using MvDb.Application.Actions.Medias.Commands.Update;
using MvDb.Application.Actions.Medias.Commands.Delete;

namespace MvDb.WebUI.Controllers;

//[Authorize(Roles = "Administrator")]
public class MediasController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ICollection<MediaDto>>> Get()
    {
        var genres = await Mediator.Send(new GetMediasQuery());

        return Ok(genres);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MediaDto>> Get(int id)
    {
        return await Mediator.Send(new GetMediaByIdQuery(id));
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
        if (id != command.Id)
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

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteMediaCommand(id));

        return NoContent();
    }
}
