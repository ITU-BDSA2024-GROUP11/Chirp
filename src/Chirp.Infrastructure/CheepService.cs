using Chirp.Core.CheepServiceInterface;
using Chirp.Core.DTO;
using Chirp.Core.RepositoryInterfaces;

namespace Chirp.Infrastructure;

public record Message(int message_id, int author_id, string message, int pub_date);

public record User(int user_id, string username, string email);

public class CheepService : ICheepService
{
    private readonly ICheepRepository _cheepRepository;
    private readonly IAuthorRepository _authorRepository;

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
    
    public void AddCheep(string cheepText, int authorid)
    {
        _cheepRepository.AddCheep(cheepText, authorid);
    }
    
    public int GetAuthorID(string username)
    {
        return _authorRepository.GetAuthorID(username);
    }
}