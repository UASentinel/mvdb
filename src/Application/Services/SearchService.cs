using MvDb.Application.Common.Interfaces;

namespace MvDb.Application.Services;

public class SearchService : ISearchService
{
    public SearchService() { }

    public bool CheckKeyWords(string target, string source)
    {
        var titleKeyWords = source.Split(" ").ToList();
        var normalizedTarget = target.ToLower();

        foreach (var titleKeyWord in titleKeyWords)
        {
            if (normalizedTarget.Contains(titleKeyWord.ToLower()))
                return true;
        }

        return false;
    }
}
