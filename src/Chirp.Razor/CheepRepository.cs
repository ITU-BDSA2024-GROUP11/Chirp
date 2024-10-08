using Microsoft.EntityFrameworkCore;

namespace Chirp.Razor;

public interface ICheepRepository
{
    public IEnumerable<Cheep> GetCheeps(int skip = 0, string? authorUsername = null);
}
public class CheepRepository : ICheepRepository
{
    private readonly ChirpDBContext _dbContext;
    private int size = 32;
    public CheepRepository(ChirpDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Cheep> GetCheeps(int skip = 0, string? authorUsername = null)
    {
        var query = _dbContext.Cheeps.Include(cheep => cheep.Author)
            .AsQueryable();

        // Apply author filtering if the username is provided
        if (!string.IsNullOrEmpty(authorUsername))
        {
            query = query.Where(cheep => cheep.Author.Name == authorUsername);
        }

        // Skip and take based on the parameters
        query = query.Skip(skip);
       

        return query.ToList();
    }



    

}