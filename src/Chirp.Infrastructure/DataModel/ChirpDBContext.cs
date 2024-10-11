using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure.DataModel;

public class ChirpDBContext : DbContext
{
    public ChirpDBContext(DbContextOptions<ChirpDBContext> options) : base(options)
    {
    }

    public DbSet<Cheep> Cheeps { get; set; }
    public DbSet<Author> Authors { get; set; }
}