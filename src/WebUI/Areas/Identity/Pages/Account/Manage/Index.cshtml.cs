using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvDb.Application.Common.Interfaces;
using MvDb.Domain.Entities;

namespace WebUI.Areas.Identity.Pages.Account.Manage;

public partial class IndexModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IImageService _imageService;

    public IndexModel(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IImageService imageService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _imageService = imageService;
    }

    //public string Username { get; set; }

    public ApplicationUser CurrentUser { get; set; }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [DataType(DataType.Upload)]
        [Display(Name = "Change profile photo")]
        public IFormFile ProfilePhoto { get; set; }
    }

    //private async Task LoadAsync(ApplicationUser user)
    //{
    //    var user = await _userManager.GetUserAsync(User);
    //    if (user == null)
    //    {
    //        return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
    //    }

    //    CurrentUser = user;
    //}

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        //await LoadAsync(user);
        CurrentUser = user;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }
        CurrentUser = user;

        if (CurrentUser != null)
        {
            CurrentUser.ProfilePhotoLink = await _imageService.UploadProfilePhoto(Input.ProfilePhoto, CurrentUser.Id);
            await _userManager.UpdateAsync(CurrentUser);

            await _signInManager.RefreshSignInAsync(CurrentUser);

            StatusMessage = "Your profile has been updated";
        }

        return RedirectToPage();
    }
}
