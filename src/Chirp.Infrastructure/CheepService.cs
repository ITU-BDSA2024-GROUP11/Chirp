using Chirp.Core.CheepServiceInterface;
using Chirp.Core.DTO;
using Chirp.Core.RepositoryInterfaces;

namespace Chirp.Infrastructure;

public record Message(int message_id, int author_id, string message, int pub_date);

public record User(int user_id, string username, string email);

public class CheepService : ICheepService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly ICheepRepository _cheepRepository;

    public CheepService(ICheepRepository cheepRepository, IAuthorRepository authorRepository)
    {
        _cheepRepository = cheepRepository;
        _authorRepository = authorRepository;
    }

    public List<CheepDTO> GetCheepsFromAuthor(string author, int page)
    {
        return _cheepRepository.GetCheeps(page, author);
    }

    public List<CheepDTO> GetCheepsFromPage(int page)
    {
        return _cheepRepository.GetCheeps(page);
    }

    public void AddCheep(string cheepText, string authorid)
    {
        _cheepRepository.AddCheep(cheepText, authorid);
    }

    public string GetAuthorID(string username)
    {
        return _authorRepository.GetAuthorID(username);
    }

    public int GetCheepCount()
    {
        return _cheepRepository.GetCheepCount();
    }

    public int GetCheepCountByAuthorId(string authorId)
    {
        return _cheepRepository.GetCheepCountByAuthorId(authorId);
    }
}