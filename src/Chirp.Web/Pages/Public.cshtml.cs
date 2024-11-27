using Chirp.Core.CheepServiceInterface;
using Chirp.Core.DTO;
using Chirp.Core.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Razor.Pages;

public class PublicModel : PageModel
{
    private readonly IAuthorRepository _authorRepository;
    private readonly ICheepService _service;

    public PublicModel(ICheepService service, IAuthorRepository authorService)
    {
        _service = service;
        _authorRepository = authorService;
    }

    public List<CheepDTO> Cheeps { get; set; } = new();

    [BindProperty] public required string NewCheepText { get; set; }

    public ActionResult OnGet([FromQuery] int page)
    {
        if (page == 0) page = 1;
        Cheeps = _service.GetCheepsFromPage(page);
        return Page();
    }

    public ActionResult OnPost()
    {

        if (string.IsNullOrWhiteSpace(NewCheepText))
        {
            ModelState.AddModelError("NewCheepText", "Cheep text is required.");
            return Page();
        }

        var username = User.Identity?.Name;
        if (string.IsNullOrEmpty(username))
        {
            throw new InvalidOperationException("User is not authenticated.");
        }

        _service.AddCheep(NewCheepText, _authorRepository.GetAuthorID(username));
        return RedirectToPage();
    }

    public ActionResult OnPostFollow(string authorName)
    {
        var authorId = _authorRepository.GetAuthorID(authorName);

        var username = User.Identity?.Name;
        if (string.IsNullOrEmpty(username))
        {
            throw new InvalidOperationException("User is not authenticated.");
        }

        _service.FollowAuthor(_authorRepository.GetAuthorID(username), authorId);
        return RedirectToPage();
    }

    public ActionResult OnPostUnfollow(string authorName)
    {
        var authorId = _authorRepository.GetAuthorID(authorName);

        var username = User.Identity?.Name;
        if (string.IsNullOrEmpty(username))
        {
            throw new InvalidOperationException("User is not authenticated.");
        }

        _service.UnfollowAuthor(_authorRepository.GetAuthorID(username), authorId);
        return RedirectToPage();
    }

    public bool FollowsAuthor(string authorName)
    {
        var username = User.Identity?.Name;
        if (string.IsNullOrEmpty(username))
        {
            throw new InvalidOperationException("User is not authenticated.");
        }

        var followedAuthors = _authorRepository.GetFollowedAuthors(username);
        foreach (var author in followedAuthors)
            if (author.Name == authorName)
                return true;

        return false;
    }
}