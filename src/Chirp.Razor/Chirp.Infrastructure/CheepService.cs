namespace Chirp.Razor.Chirp.Infrastructure;

public record Message(int message_id, int author_id, string message, int pub_date);

public record User(int user_id, string username, string email);

public record CheepViewModel(string username, string message, string pub_date);

public interface ICheepService
{
    public List<CheepViewModel> GetCheeps();
    public List<CheepViewModel> GetCheepsFromAuthor(string author, int page);
    public List<CheepViewModel> GetCheepsFromPage(int page);
}

public class CheepService : ICheepService
{
    public List<CheepViewModel> GetCheeps()
    {
        var dbFacade = new DBFacade();
        return dbFacade.GetCheepsFromPage(1);
    }

    public List<CheepViewModel> GetCheepsFromAuthor(string author, int page)
    {
        var dbFacade = new DBFacade();
        return dbFacade.GetCheepsFromAuthorPage(author, page);
    }

    public List<CheepViewModel> GetCheepsFromPage(int page)
    {
        var dbFacade = new DBFacade();
        return dbFacade.GetCheepsFromPage(page);
    }
}