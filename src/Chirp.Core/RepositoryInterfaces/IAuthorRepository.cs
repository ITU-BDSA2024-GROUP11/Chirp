using Chirp.Core.DTO;

namespace Chirp.Core.RepositoryInterfaces;

public interface IAuthorRepository
{
    AuthorDTO GetAuthorByName(string name);
    AuthorDTO GetAuthorByEmail(string email);
    void CreateAuthor(string name, string email);
    string GetAuthorID(string username);
    void FollowAuthor(string userId, string followId);
    void UnfollowAuthor(string userId, string followId);
    List<AuthorDTO> GetFollowedAuthors(string userId);
}