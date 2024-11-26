using Microsoft.AspNetCore.Identity;

namespace Chirp.Infrastructure.DataModel;

public class Author : IdentityUser
{
    public required List<Cheep> Cheeps { get; set; } = [];
    public List<Author> Follows { get; set; } = [];
}