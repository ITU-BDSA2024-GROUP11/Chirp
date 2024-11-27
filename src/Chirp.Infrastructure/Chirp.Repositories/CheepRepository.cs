using System.Globalization;
using Chirp.Core.DTO;
using Chirp.Core.RepositoryInterfaces;
using Chirp.Infrastructure.DataModel;

namespace Chirp.Infrastructure.Chirp.Repositories;

public class CheepRepository : ICheepRepository
{
    private readonly AuthorRepository _authorRepository;
    private readonly ChirpDBContext _dbContext;
    private readonly int size = 32;

    public CheepRepository(ChirpDBContext dbContext)
    {
        _dbContext = dbContext;
        _authorRepository = new AuthorRepository(dbContext);
    }

    public List<CheepDTO> GetCheeps(int page, string authorUsername, int pageSize)
    {
        var query = _dbContext.Cheeps
            .Where(cheep => cheep.Author.UserName == authorUsername)
            .OrderByDescending(cheep => cheep.TimeStamp)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        var result = query;
        var list = new List<CheepDTO>();
        foreach (var cheep in result)
        {
            _authorRepository.FindAuthorById(cheep.AuthorId);
            list.Add(CheepToDTO(cheep));
        }

        return list;
    }

    public List<CheepDTO> GetCheeps(int page)
    {
        var query = _dbContext.Cheeps
            .OrderByDescending(cheep => cheep.TimeStamp)
            .Skip((page - 1) * size)
            .Take(size)
            .ToList();
        var result = query;
        var list = new List<CheepDTO>();
        foreach (var cheep in result)
        {
            _authorRepository.FindAuthorById(cheep.AuthorId);
            list.Add(CheepToDTO(cheep));
        }

        return list;
    }

    public void AddCheep(string text, string authorId)
    {
        if (text.Length > 160) return;
        // Create a cheep object
        var newCheep = new Cheep
        {
            AuthorId = authorId,
            Author = _authorRepository.FindAuthorById(authorId),
            Text = text,
            TimeStamp = DateTime.Now
        };

        // Add the new Cheep to the DbSet
        _dbContext.Cheeps.Add(newCheep);

        // Save changes to the database
        _dbContext.SaveChanges();
    }

    public int GetCheepCount()
    {
        return _dbContext.Cheeps.Count();
    }

    public int GetCheepCountByAuthorId(string authorId)
    {
        return _dbContext.Cheeps.Where(cheep => cheep.AuthorId == authorId).Count();
    }

    public static CheepDTO CheepToDTO(Cheep cheep)
    {
        return new CheepDTO
        {
            Text = cheep.Text,
            Author = cheep.Author.UserName!,
            TimeStamp = cheep.TimeStamp.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
        };
    }

    public List<CheepDTO> GetCheepsFromAuthors(List<AuthorDTO> authors, int page)
    {
        var authorIds = authors.Select(author => author.Id).ToList();
        var query = _dbContext.Cheeps
            .Where(cheep => authorIds.Contains(cheep.AuthorId))
            .OrderByDescending(cheep => cheep.TimeStamp)
            .Skip((page - 1) * size)
            .Take(size)
            .ToList();
        var result = query;
        var list = new List<CheepDTO>();
        foreach (var cheep in result)
        {
            _authorRepository.FindAuthorById(cheep.AuthorId);
            list.Add(CheepToDTO(cheep));
        }

        return list;
    }

}