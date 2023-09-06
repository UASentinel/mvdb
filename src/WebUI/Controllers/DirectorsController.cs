﻿using Microsoft.AspNetCore.Mvc;
using MvDb.Application.Actions.Directors.Commands.Create;
using MvDb.Application.Actions.Directors.Commands.Delete;
using MvDb.Application.Actions.Directors.Commands.Update;
using MvDb.Application.Actions.Directors.DataTransferObjects;
using MvDb.Application.Actions.Directors.Queries.Get;
using MvDb.Application.Actions.Directors.Queries.GetById;
using MvDb.Application.Actions.Directors.Queries.Search;
using Microsoft.AspNetCore.Authorization;

namespace MvDb.WebUI.Controllers;

[Authorize(Roles = "Administrator")]
public class DirectorsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ICollection<DirectorDto>>> Get()
    {
        var genres = await Mediator.Send(new GetDirectorsQuery());

        return Ok(genres);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DirectorDto>> Get(int id)
    {
        return await Mediator.Send(new GetDirectorByIdQuery(id));
    }

    [HttpPost("Search")]
    public async Task<ActionResult<ICollection<DirectorDto>>> Search(SearchDirectorsQuery query)
    {
        var medias = await Mediator.Send(query);

        return Ok(medias);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromForm] CreateDirectorCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(int id, [FromForm] UpdateDirectorCommand command)
    {
        if (id != command.DirectorId)
            return BadRequest();

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteDirectorCommand(id));

        return NoContent();
    }
}