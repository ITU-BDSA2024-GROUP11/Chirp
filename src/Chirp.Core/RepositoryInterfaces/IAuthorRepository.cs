using Chirp.Core.DTO;

namespace Chirp.Core.RepositoryInterfaces;

public interface IAuthorRepository
{
    AuthorDTO GetAuthorByName(string name);
    AuthorDTO GetAuthorByEmail(string email);
    void CreateAuthor(string name, string email);
    int GetAuthorID(string username);
}