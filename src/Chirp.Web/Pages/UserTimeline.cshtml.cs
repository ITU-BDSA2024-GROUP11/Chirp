using Chirp.Core.CheepServiceInterface;
using Chirp.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Razor.Pages;

public class UserTimelineModel : PageModel
{
    private readonly ICheepService _service;

    public UserTimelineModel(ICheepService service)
    {
        _service = service;
    }

    public List<CheepDTO> Cheeps { get; set; } = new List<CheepDTO>();

    [BindProperty] public string NewCheepText { get; set; }

    public ActionResult OnGet([FromQuery] int page, string author)
    {
        if (page == 0) page = 1;
        Cheeps = _service.GetCheepsFromAuthor(author, page);
        return Page();
    }

    public ActionResult OnPostSendCheep()
    {
        if (!User.Identity.IsAuthenticated) return RedirectToPage("/Account/Login");

        if (string.IsNullOrWhiteSpace(NewCheepText))
        {
            ModelState.AddModelError("NewCheepText", "Cheep text is required.");
            return Page();
        }

        var authorId = _service.GetAuthorID(User.Identity.Name); // Adjust this as needed
        _service.AddCheep(NewCheepText, authorId);

        return RedirectToPage(new { author = User.Identity.Name });
    }
}
