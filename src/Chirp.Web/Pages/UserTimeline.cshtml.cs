using Chirp.Core.CheepServiceInterface;
using Chirp.Core.DTO;
using Chirp.Core.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Razor.Pages;

/// <summary>
/// Page model for the user timeline page.
/// </summary>
public class UserTimelineModel : PageModel
{
    private readonly IAuthorRepository _authorRepository;
    private readonly ICheepService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserTimelineModel"/> class.
    /// </summary>
    /// <param name="service">The cheep service to be used.</param>
    /// <param name="authorService">The author repository to be used.</param>
    public UserTimelineModel(ICheepService service, IAuthorRepository authorService)
    {
        _service = service;
        _authorRepository = authorService;
    }

    public List<CheepDTO> Cheeps { get; set; } = new();

    [BindProperty] public required string NewCheepText { get; set; }

    /// <summary>
    /// Handles GET requests to the user timeline page.
    /// </summary>
    /// <param name="page">The page number.</param>
    /// <param name="author">The author name.</param>
    /// <returns>The page result.</returns>
    public ActionResult OnGet([FromQuery] int page, string author)
    {
        if (page == 0) page = 1;

        if (author == User.Identity?.Name)
        {
            var followedAuthors = _authorRepository.GetFollowedAuthors(User.Identity.Name);
            followedAuthors.Add(_authorRepository.GetAuthorByName(User.Identity.Name));
            Cheeps = _service.GetCheepsFromAuthors(followedAuthors, page);
        }
        else
        {
            Cheeps = _service.GetCheepsFromAuthor(author, page);
        }

        return Page();
    }

    /// <summary>
    /// Handles POST requests to send a new cheep.
    /// </summary>
    /// <returns>The page result.</returns>
    public ActionResult OnPostSendCheep()
    {
        if (string.IsNullOrWhiteSpace(NewCheepText))
        {
            ModelState.AddModelError("NewCheepText", "Cheep text is required.");
            return RedirectToPage();
        }

        var username = User.Identity?.Name;
        if (string.IsNullOrEmpty(username))
        {
            throw new InvalidOperationException("User is not authenticated.");
        }

        _service.AddCheep(NewCheepText, _authorRepository.GetAuthorID(username));

        return RedirectToPage(new { author = User.Identity?.Name });
    }

    /// <summary>
    /// Handles POST requests to follow an author.
    /// </summary>
    /// <param name="authorName">The name of the author to follow.</param>
    /// <returns>The page result.</returns>
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

    /// <summary>
    /// Handles POST requests to unfollow an author.
    /// </summary>
    /// <param name="authorName">The name of the author to unfollow.</param>
    /// <returns>The page result.</returns>
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

    /// <summary>
    /// Checks if the current user follows the specified author.
    /// </summary>
    /// <param name="authorName">The name of the author.</param>
    /// <returns>True if the user follows the author, otherwise false.</returns>
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

    /// <summary>
    /// Handles POST requests to submit an edited cheep.
    /// </summary>
    /// <param name="cheep">The cheep DTO.</param>
    /// <returns>The page result.</returns>
    public ActionResult OnPostSubmitEdit(CheepDTO cheep)
    {
        var text = Request.Form["text"];
        if (!string.IsNullOrWhiteSpace(text)){
            _service.EditCheep(cheep, text);
        }
        return RedirectToPage();
    }

    /// <summary>
    /// Handles POST requests to delete a cheep.
    /// </summary>
    /// <param name="cheep">The cheep DTO.</param>
    /// <returns>The page result.</returns>
    public ActionResult OnPostDeleteCheep(CheepDTO cheep)
    {
        _service.DeleteCheep(cheep);
        return RedirectToPage();
    }
}