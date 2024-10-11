using Chirp.Core.DTO;

namespace Chirp.Core.RepositoryInterfaces;

public interface ICheepRepository
{
    public List<CheepDTO> GetCheeps(int skip = 0, string? authorUsername = null);
}