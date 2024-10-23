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

    public ActionResult OnGet([FromQuery] int page, string author)
    {
        if (page == 0) page = 1;
        Cheeps = _service.GetCheepsFromAuthor(author, page);
        return Page();
    }
}