using Microsoft.EntityFrameworkCore;

namespace Chirp.Razor;

public class ChirpDBContext : DbContext
{
    public DbSet<Cheep> Cheeps { get; set; }
    public DbSet<Author> Authors { get; set; }
    public ChirpDBContext(DbContextOptions<ChirpDBContext> options) : base(options)
    {
    }
}
public class Cheep
{ 
    public string Text { get; set; }
    public DateTime TimeStamp { get; set; }
    public int AuthorId { get; set; }
    public int CheepId { get; set; }
    public Author Author { get; set; }
}

public class Author
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int AuthorId { get; set; }
    public List<Cheep> Cheeps { get; set; }
    public void AddCheep(Cheep cheep)
    {
        Cheeps.Add(cheep);
    }
    
}
