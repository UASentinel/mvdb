using Microsoft.AspNetCore.Http;
using MvDb.Application.Common.Models;

namespace MvDb.Application.Common.Interfaces;

public interface IImageService
{
    Task<string> UploadProfilePhoto(IFormFile formFile, string userId);
}
