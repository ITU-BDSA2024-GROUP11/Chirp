using Chirp.Core.DTO;

namespace Chirp.Core.CheepServiceInterface;

public interface ICheepService
{
    public List<CheepDTO> GetCheepsFromAuthor(string author, int page);
    public List<CheepDTO> GetCheepsFromPage(int page);
    public void AddCheep(string cheepText, string authorid);
    public string GetAuthorID(string username);
    public int GetCheepCount();
    public int GetCheepCountByAuthorId(string authorid);
}