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
}
public class CheepDTO
{
    public required string Text { get; set; }
    public required string Author { get; set; }
    public required string TimeStamp { get; set; }
};