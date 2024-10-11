using Chirp.Core.DTO;
using Chirp.Core.RepositoryInterfaces;
using Chirp.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure.Chirp.Repositories;

public class CheepRepository : ICheepRepository
{
    private readonly ChirpDBContext _dbContext;
    private int size = 32;

    public CheepRepository(ChirpDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<CheepDTO> GetCheeps(int skip = 0, string? authorUsername = null)
    {
        var query = _dbContext.Cheeps.Include(cheep => cheep.Author)
            .AsQueryable();

        // Apply author filtering if the username is provided
        if (!string.IsNullOrEmpty(authorUsername)) query = query.Where(cheep => cheep.Author.Name == authorUsername);

        // Skip and take based on the parameters
        query = query.Skip(skip);

        query.ToList();
        var result = new List<CheepDTO>();
        foreach (var cheep in query) result.Add(CheepToDTO(cheep));

        return result;
    }

    public static CheepDTO CheepToDTO(Cheep cheep)
    {
        return new CheepDTO
        {
            Text = cheep.Text,
            Author = cheep.Author.Name,
            TimeStamp = cheep.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss")
        };
    }

    public void AddCheep(string text, int authorId)
    {
        var authorRepository = new AuthorRepository(_dbContext);
        // Create a cheep object
        var newCheep = new Cheep
        {
            AuthorId = authorId,
            Author = authorRepository.FindAuthorById(authorId),
            Text = text,
            TimeStamp = DateTime.Now
        };

        // Add the new Cheep to the DbSet
        _dbContext.Cheeps.Add(newCheep);

        // Save changes to the database
        _dbContext.SaveChanges();
    }
}