using System.Security.Claims;
using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Models;
using MvDb.Domain.Common;

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
        var directoryPath = $"{_webRootPath}\\{Constants.UsersFolderPathLocal}\\{userId}";

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        var imagePath = $"{directoryPath}\\{Constants.ProfilePhotoFileName}";

        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }

        using (FileStream fs = File.Create(imagePath))
        {
            await imageFile.CopyToAsync(fs);
        }

        var imageWebPath = $"{_hostPath}/{Constants.UsersFolderPathHost}/{userId}/{Constants.ProfilePhotoFileName}";

        return imageWebPath;
    }
}
