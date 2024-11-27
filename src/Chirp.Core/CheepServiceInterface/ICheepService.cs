using Chirp.Core.DTO;

namespace Chirp.Core.CheepServiceInterface;

public interface ICheepService
{
    public List<CheepDTO> GetCheepsFromAuthor(string author, int page, int pageSize = 32);
    public List<CheepDTO> GetCheepsFromPage(int page);
    public void AddCheep(string cheepText, string authorid);
    public string GetAuthorID(string username);
    public int GetCheepCount();
    public int GetCheepCountByAuthorId(string authorid);
    public void FollowAuthor(string userId, string followId);
    public void UnfollowAuthor(string userId, string followId);
    public List<CheepDTO> GetCheepsFromAuthors(List<AuthorDTO> authors, int page);
}