using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure.DataModel;

[PrimaryKey(nameof(AuthorId), nameof(FollowsId))]
public class AuthorFollows
{
    public string AuthorId { get; set; } = null!;
    public Author Author { get; set; } = null!;

    public string FollowsId { get; set; } = null!;
    public Author Follows { get; set; } = null!;
}