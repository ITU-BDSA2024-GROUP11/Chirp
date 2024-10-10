using System.ComponentModel.DataAnnotations;
using Chirp.Razor.Pages;
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
    public int CheepId { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTime TimeStamp { get; set; }
    
}

public class Author
{
    public int AuthorId { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public List<Cheep> Cheeps { get; set; } = null!;
    public void AddCheep(Cheep cheep)
    {
        Cheeps.Add(cheep);
    }
    
}
