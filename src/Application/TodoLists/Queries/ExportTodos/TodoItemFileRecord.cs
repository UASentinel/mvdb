using MvDb.Application.Common.Mappings;
using MvDb.Domain.Entities;

namespace MvDb.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; init; }

    public bool Done { get; init; }
}
