namespace Chirp.Infrastructure.DataModel;

public class Author
{
    public int AuthorId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required List<Cheep> Cheeps { get; set; }
    public void AddCheep(Cheep cheep)
    {
        Cheeps.Add(cheep);
    }
}