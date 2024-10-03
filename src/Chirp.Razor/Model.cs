using Microsoft.EntityFrameworkCore;

namespace Chirp.Razor;

public class Cheep
{
    public string Text { get; set; }
    public DateTime TimeStamp { get; set; }
    public int AuthorId { get; set; }
    public int Id { get; set; }
}

public class Author
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int Id { get; set; }
    public List<Cheep> Cheeps { get; set; }
    public void addCheep(Cheep cheep)
    {
        Cheeps.Add(cheep);
    }
}
