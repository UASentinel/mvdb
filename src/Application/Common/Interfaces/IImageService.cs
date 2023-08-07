using Microsoft.AspNetCore.Http;
using MvDb.Application.Common.Models;

namespace MvDb.Application.Common.Interfaces;

public interface IImageService
{
    Task<string> UploadProfilePhoto(IFormFile formFile, string userId);
    Task<string> UploadActorPhoto(IFormFile formFile, int actorId);
    Task<bool> DeleteActorPhoto(int actorId);
    Task<string> UploadDirectorPhoto(IFormFile formFile, int directorId);
    Task<bool> DeleteDirectorPhoto(int directorId);
    Task<string> UploadSeasonPoster(IFormFile formFile, int seasonId, int mediaId);
    Task<bool> DeleteSeasonPoster(int seasonId, int mediaId);
    Task<string> UploadMediaPoster(IFormFile formFile, int mediaId);
    Task<bool> DeleteMediaPoster(int mediaId);
}
