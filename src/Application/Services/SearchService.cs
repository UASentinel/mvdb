using System.Security.AccessControl;
using MediatR;
using Microsoft.AspNetCore.Http;
using MvDb.Application.Actions.Directors.Queries.Search;
using MvDb.Application.Actions.Medias.DataTransferObjects;
using MvDb.Application.Actions.Medias.Queries.Search;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Interfaces.EntityServices;
using MvDb.Application.Common.Interfaces.Repositories;
using MvDb.Application.Common.Models;
using MvDb.Domain.Entities;
using MvDb.Domain.Enums;

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
