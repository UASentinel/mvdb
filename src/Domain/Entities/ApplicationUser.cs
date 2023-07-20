using Microsoft.AspNetCore.Identity;

namespace MvDb.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string? ProfilePhotoLink { get; set; }
    public List<Review> Reviews { get; set; } = new List<Review> { };
}
