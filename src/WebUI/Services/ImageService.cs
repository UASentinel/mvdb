using System.Security.Claims;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Models;
using MvDb.Domain.Common;
using MvDb.Domain.Entities;

namespace MvDb.WebUI.Services;

public class ImageService : IImageService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string _webRootPath;
    private readonly string _hostPath;

    public ImageService(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
    {
        _webHostEnvironment = webHostEnvironment;
        _webRootPath = _webHostEnvironment.WebRootPath;

        _httpContextAccessor = httpContextAccessor;
        var request = _httpContextAccessor.HttpContext.Request;
        _hostPath = $"{request.Scheme}://{request.Host}{request.PathBase}";
    }

    public async Task<string> UploadProfilePhoto(IFormFile imageFile, string userId)
    {
        if (imageFile == null)
            return null;

        return await UploadPhoto(imageFile: imageFile,
            folderName: userId,
            fileName: Constants.ProfilePhotoFileName,
            folderPathLocal: Constants.UsersFolderPathLocal,
            folderPathHost: Constants.UsersFolderPathHost);
    }

    public async Task<string> UploadActorPhoto(IFormFile imageFile, int actorId)
    {
        if (imageFile == null)
            return null;

        return await UploadPhoto(imageFile: imageFile,
            folderName: actorId.ToString(),
            fileName: Constants.ActorPhotoFileName,
            folderPathLocal: Constants.ActorsFolderPathLocal,
            folderPathHost: Constants.ActorsFolderPathHost);
    }

    public async Task<bool> DeleteActorPhoto(int actorId)
    {
        return await DeletePhoto(folderName: actorId.ToString(),
            fileName: Constants.ActorPhotoFileName,
            folderPathLocal: Constants.ActorsFolderPathLocal,
            folderPathHost: Constants.ActorsFolderPathHost);
    }

    private async Task<string> UploadPhoto(
        IFormFile imageFile,
        string folderName,
        string fileName,
        string folderPathLocal,
        string folderPathHost
        )
    {
        var directoryPath = $"{_webRootPath}\\{folderPathLocal}\\{folderName}";

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        var imagePath = $"{directoryPath}\\{fileName}";

        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }

        using (FileStream fs = File.Create(imagePath))
        {
            await imageFile.CopyToAsync(fs);
        }

        var imageWebPath = $"{_hostPath}/{folderPathHost}/{folderName}/{fileName}";

        return imageWebPath;
    }

    private async Task<bool> DeletePhoto(
        string folderName,
        string fileName,
        string folderPathLocal,
        string folderPathHost
        )
    {
        var directoryPath = $"{_webRootPath}\\{folderPathLocal}\\{folderName}";

        if (!Directory.Exists(directoryPath))
            return false;

        var imagePath = $"{directoryPath}\\{fileName}";

        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
            return true;
        }

        return false;
    }
}
