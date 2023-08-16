namespace MvDb.Application.Common.Interfaces;

public interface ISearchService
{
    bool CheckKeyWords(string target, string source);
}
