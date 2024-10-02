public record Message(int message_id, int author_id, string message, int pub_date);
public record User(int user_id, string username, string email);
public record CheepViewModel(string username, string message, string pub_date);

public interface ICheepService
{
    public List<CheepViewModel> GetCheeps();
    public List<CheepViewModel> GetCheepsFromAuthor(string author);
    public List<CheepViewModel> GetCheepsFromPage(int page);
}

public class CheepService : ICheepService
{
    public List<CheepViewModel> GetCheeps()
    {
        DBFacade dbFacade = new DBFacade();
        return dbFacade.GetCheepsFromPage(1);
    }

    public List<CheepViewModel> GetCheepsFromAuthor(string author)
    {
        // filter by the provided author name
        //return _cheeps.Where(x => x.Author == author).ToList();
        DBFacade dbFacade = new DBFacade();
        return dbFacade.GetCheepsFromAuthor(author);
    }

    public List<CheepViewModel> GetCheepsFromPage(int page)
    {
        AuthorFacade authorFacade = new AuthorFacade();
        return authorFacade.GetCheepsFromPage(page);
    }
}
