using Chirp.Core.CheepServiceInterface;
using Chirp.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Razor.Pages;

public class PublicModel : PageModel
{
    private readonly ICheepService _service;

    public PublicModel(ICheepService service)
    {
        _service = service;
    }

    public List<CheepDTO> Cheeps { get; set; }

    public ActionResult OnGet([FromQuery] int page)
    {
        if (page == 0) page = 1;
        Cheeps = _service.GetCheepsFromPage(page);
        return Page();
    }
}