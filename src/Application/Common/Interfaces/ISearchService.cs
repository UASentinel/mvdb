using Microsoft.AspNetCore.Http;
using MvDb.Application.Common.Models;

namespace MvDb.Application.Common.Interfaces;

public interface ISearchService
{
    bool CheckKeyWords(string target, string source);
}
