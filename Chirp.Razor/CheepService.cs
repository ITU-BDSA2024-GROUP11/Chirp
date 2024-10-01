public record Message(int message_id, int author_id, string message, int pub_date);
public record User(int user_id, string username, string email);
public record Cheeps(string username, string message, string pub_date);

public interface ICheepService
{
    public List<Cheeps> GetCheeps();
    public List<Cheeps> GetCheepsFromAuthor(string author);
}

public class CheepService : ICheepService
{
    // These would normally be loaded from a database for example
    private static readonly List<Cheeps> _cheeps = new()
        {
            new Cheeps("Helge", "Hello, BDSA students!", UnixTimeStampToDateTimeString(1690892208)),
            new Cheeps("Adrian", "Hej, velkommen til kurset.", UnixTimeStampToDateTimeString(1690895308)),
        };

    public List<Cheeps> GetCheeps()
    {
        AuthorFacade authorFacade = new AuthorFacade();
        return authorFacade.GetCheeps();
    }

    public List<Cheeps> GetCheepsFromAuthor(string author)
    {
        // filter by the provided author name
        //return _cheeps.Where(x => x.Author == author).ToList();
        AuthorFacade authorFacade = new AuthorFacade();
        return authorFacade.GetCheepsFromAuthor(author);
    }

    private static string UnixTimeStampToDateTimeString(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp);
        return dateTime.ToString("MM/dd/yy H:mm:ss");
    }

}
