using MvDb.Application.TodoLists.Queries.ExportTodos;

namespace MvDb.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
