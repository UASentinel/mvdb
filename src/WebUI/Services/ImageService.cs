using System.Security.Claims;

using MvDb.Application.Common.Interfaces;
using MvDb.Application.Common.Models;

namespace MvDb.WebUI.Services;

public class ImageService : IImageService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string _webRootPath;

    public ImageService(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
    {
        _webHostEnvironment = webHostEnvironment;
        _httpContextAccessor = httpContextAccessor;
        _webRootPath = _webHostEnvironment.WebRootPath;
    }

    public async Task<string> UploadProfilePhoto(IFormFile imageFile, string userId)
    {
        var directoryPath = _webRootPath + "\\images\\users\\" + userId;

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        var imagePath = directoryPath + "\\profile_photo.png";

        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }

        using (FileStream fs = System.IO.File.Create(imagePath))
        {
            await imageFile.CopyToAsync(fs);
        }

        var request = _httpContextAccessor.HttpContext.Request;
        var hostPath = $"{request.Scheme}://{request.Host}{request.PathBase}/";

        var imageWebPath = hostPath + "images/users/" + userId + "/profile_photo.png";

        return imageWebPath;
    }
}
