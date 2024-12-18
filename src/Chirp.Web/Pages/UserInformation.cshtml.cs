using Chirp.Core.CheepServiceInterface;
using Chirp.Core.DTO;
using Chirp.Core.RepositoryInterfaces;
using Chirp.Infrastructure.DataModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Razor.Pages;

/// <summary>
/// Page model for the user information page.
/// </summary>
public class AboutMeModel : PageModel
{
    private readonly ICheepService _cheepService;
    private readonly IAuthorRepository _authorRepository;
    private readonly SignInManager<Author> _signInManager;
    private readonly UserManager<Author> _userManager;
    private readonly ChirpDBContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="AboutMeModel"/> class.
    /// </summary>
    public AboutMeModel(ICheepService cheepService, IAuthorRepository authorRepository, SignInManager<Author> signInManager, UserManager<Author> userManager, ChirpDBContext context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _dbContext = context;
        _cheepService = cheepService;
        _authorRepository = authorRepository;
    }

    public List<CheepDTO> Cheeps { get; set; }

    /// <summary>
    /// Handles GET requests to the user information page.
    /// </summary>
    /// <param name="page">The page number.</param>
    /// <param name="author">The author name.</param>
    /// <returns>The page result.</returns>
    public ActionResult OnGet([FromQuery] int page, string author)
    {
        if (page == 0) page = 1;
        Cheeps = _cheepService.GetCheepsFromAuthor(User.Identity.Name, page, 10);
        return Page();
    }

    /// <summary>
    /// Handles POST requests to delete the current user's account.
    /// </summary>
    /// <returns>The page result.</returns>
    public async Task<IActionResult> OnPostDeleteUserAsync()
    {
        Console.WriteLine("Call to delete user");
        var author = await _userManager.GetUserAsync(User);
        if (author == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        try
        {
            await DeleteUserRelatedData(author);

            var result = await _userManager.DeleteAsync(author);
            Console.WriteLine("User deleted");
            if (!result.Succeeded)
            {
                return Redirect("~/");
            }

            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while deleting the user.");
            return Redirect("~/");
        }
    }

    /// <summary>
    /// Deletes user-related data.
    /// </summary>
    /// <param name="user">The user to delete data for.</param>
    public async Task DeleteUserRelatedData(IdentityUser user)
    {
        Console.WriteLine("Deleting user related data");
        var cheepsToRemove = _dbContext.Cheeps.Where(c => c.Author.Id == user.Id).ToList();
        _dbContext.Cheeps.RemoveRange(cheepsToRemove);
        Console.WriteLine("Deleted Cheeps");
        _authorRepository.RemoveFollows(User.Identity.Name);
        _dbContext.Authors.Where(c => c.Id == user.Id).Include(x => x.Follows).ToList();
        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Gets the list of authors the current user is following.
    /// </summary>
    /// <returns>The list of followed authors as <see cref="AuthorDTO"/>.</returns>
    public List<AuthorDTO> Following()
    {
        return _authorRepository.GetFollowedAuthors(User.Identity.Name);
    }

    /// <summary>
    /// Gets the email of the current user.
    /// </summary>
    /// <returns>The email of the user.</returns>
    public string GetEmail()
    {
        return _authorRepository.GetAuthorByName(User.Identity.Name).Email;
    }
}