using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure.DataModel;

public class ChirpDBContext : IdentityDbContext<ApplicationUser>
{
    public ChirpDBContext(DbContextOptions<ChirpDBContext> options) : base(options)
    {
    }

    public DbSet<Cheep> Cheeps { get; set; }
    public DbSet<Author> Authors { get; set; }
}