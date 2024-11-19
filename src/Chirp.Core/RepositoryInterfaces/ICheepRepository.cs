using Chirp.Core.DTO;

namespace Chirp.Core.RepositoryInterfaces;

public interface ICheepRepository
{
    public List<CheepDTO> GetCheeps(int page, string authorUsername);
    public List<CheepDTO> GetCheeps(int page);
    public void AddCheep(string cheepText, int authorid);
    public int GetCheepCount();
    public int GetCheepCountByAuthorId(int authorid);
}