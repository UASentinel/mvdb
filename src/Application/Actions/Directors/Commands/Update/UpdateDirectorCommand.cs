using MediatR;
using Microsoft.AspNetCore.Http;
using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Actions.Directors.Commands.Update;

public record UpdateDirectorCommand : IRequest
{
    public int Id { get; init; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Biography { get; set; }
    public IFormFile PhotoFile { get; set; }
}