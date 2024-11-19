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

    [BindProperty] public string NewCheepText { get; set; }

    public ActionResult OnGet([FromQuery] int page)
    {
        if (page == 0) page = 1;
        Cheeps = _service.GetCheepsFromPage(page);
        return Page();
    }

    public ActionResult OnPost()
    {
        if (!User.Identity.IsAuthenticated) return RedirectToPage("/Account/Login");

        if (string.IsNullOrWhiteSpace(NewCheepText))
        {
            ModelState.AddModelError("NewCheepText", "Cheep text is required.");
            return Page();
        }
        _service.AddCheep(NewCheepText, _authorRepository.GetAuthorID(User.Identity.Name));
        return RedirectToPage();
    }
}