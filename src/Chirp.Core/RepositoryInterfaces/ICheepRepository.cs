using Chirp.Core.DTO;

namespace Chirp.Core.RepositoryInterfaces;

public interface ICheepRepository
{
    public List<CheepDTO> GetCheeps(int page, string authorUsername, int pageSize);
    public List<CheepDTO> GetCheeps(int page);
    public void AddCheep(string cheepText, string authorid);
    public int GetCheepCount();
    public int GetCheepCountByAuthorId(string authorid);
    public List<CheepDTO> GetCheepsFromAuthors(List<AuthorDTO> authors, int page);
    public CheepDTO EditCheep(CheepDTO cheep, string text);
    void DeleteCheep(CheepDTO cheep);
}