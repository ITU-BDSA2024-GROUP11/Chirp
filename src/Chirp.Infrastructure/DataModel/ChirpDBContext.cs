using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure.DataModel;

public class ChirpDBContext : DbContext
{
    
/*
    public ChirpDBContext()
    {

    }
*/

    public ChirpDBContext(DbContextOptions<ChirpDBContext> options) : base(options)
    {

    }

/*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=chirp.db");
    }
*/
    public DbSet<Cheep> Cheeps { get; set; }
    public DbSet<Author> Authors { get; set; }
}