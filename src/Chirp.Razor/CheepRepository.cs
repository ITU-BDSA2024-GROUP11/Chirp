namespace Chirp.Razor;

public interface ICheepRepository
{
}

public class CheepRepository : ICheepRepository
{
    private readonly ChirpDBContext _dbContext;

    public CheepRepository(ChirpDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public static CheepDTO CheepToDTO(Cheep cheep)
    {
        return new CheepDTO
        {
            Text = cheep.Text,
            Author = cheep.Author.Name,
            TimeStamp = cheep.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss")
        };
    }

    public void AddCheep(string text, int authorId)
    {
        // Create a cheep object
        var newCheep = new Cheep
        {
            AuthorId = authorId,
            Author = FindAutoherById(authorId),
            Text = text,
            TimeStamp = DateTime.Now
        };

        // Add the new Cheep to the DbSet
        _dbContext.Cheeps.Add(newCheep);

        // Save changes to the database
        _dbContext.SaveChanges();
    }
}

public class CheepDTO
{
    public string Text { get; set; }
    public string Author { get; set; }
    public string TimeStamp { get; set; }
}