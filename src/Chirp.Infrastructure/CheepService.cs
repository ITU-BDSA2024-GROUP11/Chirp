using Chirp.Core.CheepServiceInterface;
using Chirp.Core.DTO;
using Chirp.Core.RepositoryInterfaces;

namespace Chirp.Infrastructure;

public class CheepService : ICheepService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly ICheepRepository _cheepRepository;

    public CheepService(ICheepRepository cheepRepository, IAuthorRepository authorRepository)
    {
        _cheepRepository = cheepRepository;
        _authorRepository = authorRepository;
    }

    public List<CheepDTO> GetCheepsFromAuthor(string author, int page, int pageSize = 32)
    {
        return _cheepRepository.GetCheeps(page, author, pageSize);
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

    public void FollowAuthor(string userId, string followId)
    {
        _authorRepository.FollowAuthor(userId, followId);
    }

    public void UnfollowAuthor(string userId, string followId)
    {
        _authorRepository.UnfollowAuthor(userId, followId);
    }

    public List<CheepDTO> GetCheepsFromAuthors(List<AuthorDTO> authors, int page)
    {
        return _cheepRepository.GetCheepsFromAuthors(authors, page);
    }
}