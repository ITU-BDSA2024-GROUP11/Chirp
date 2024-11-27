using Chirp.Core.CheepServiceInterface;
using Chirp.Core.DTO;
using Chirp.Core.RepositoryInterfaces;
using Chirp.Infrastructure.DataModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Razor.Pages;

public class AboutMeModel : PageModel
{
    private readonly ICheepService _cheepService;
    private readonly IAuthorRepository _authorRepository;
    private readonly SignInManager<Author> _signInManager;
    private readonly UserManager<Author> _userManager;
    private readonly ChirpDBContext _dbContext;

    public AboutMeModel(ICheepService cheepService, IAuthorRepository authorRepository, SignInManager<Author> signInManager, UserManager<Author> userManager, ChirpDBContext context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _dbContext = context;
        _cheepService = cheepService;
        _authorRepository = authorRepository;
    }
    
    public List<CheepDTO> Cheeps { get; set; }
    
    public ActionResult OnGet([FromQuery] int page, string author)
    {
        if (page == 0) page = 1;
        Cheeps = _cheepService.GetCheepsFromAuthor(User.Identity.Name, page, 10);
        return Page();
    }
    
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

        return Redirect("~/");

        /*
        var Author = _userManager.GetUserAsync(User).Result;
        if (Author == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        await DeleteUserRelatedData(Author);

        var result = await _userManager.DeleteAsync(Author);
        if (!result.Succeeded)
        {
            return Page();
        }

        await _signInManager.SignOutAsync();
        return Redirect("/Index");
        */
    }
    
    public async Task DeleteUserRelatedData(IdentityUser user)
    {
        Console.WriteLine("Deleting user related data");
        var cheepsToRemove = _dbContext.Cheeps.Where(c => c.Author.Id == user.Id).ToList();
        _dbContext.Cheeps.RemoveRange(cheepsToRemove);
        Console.WriteLine("Deleted Cheeps");
        //_authorRepository.RemoveFollows(User.Identity.Name);
        Console.WriteLine("Removed Follows");
        
        
        //_cheepService.RemoveCheeps(User.Identity.Name);
        //_authorRepository.RemoveFollows(User.Identity.Name);
        //_dbContext.Cheeps.RemoveRange(_dbContext.Cheeps.Where(c => c.Author.UserName == user.UserName));
        //_dbContext.AuthorFollows.RemoveRange(_dbContext.AuthorFollows.Where(f => f.AuthorId == user.Id));
    }

    public List<AuthorDTO> Following()
    {
        return _authorRepository.GetFollowedAuthors(User.Identity.Name);
    }

    public string GetEmail()
    {
        return _authorRepository.GetAuthorByName(User.Identity.Name).Email;
    }
}