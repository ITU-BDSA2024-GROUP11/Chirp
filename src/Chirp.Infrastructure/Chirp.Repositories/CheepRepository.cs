using System.Globalization;
using Chirp.Core.DTO;
using Chirp.Core.RepositoryInterfaces;
using Chirp.Infrastructure.DataModel;

namespace Chirp.Infrastructure.Chirp.Repositories;

/// <summary>
/// Repository class for managing cheep data in the database.
/// </summary>
public class CheepRepository : ICheepRepository
{
    private readonly AuthorRepository _authorRepository;
    private readonly ChirpDBContext _dbContext;
    private readonly int size = 32;

    /// <summary>
    /// Initializes a new instance of the <see cref="CheepRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context to be used.</param>
    public CheepRepository(ChirpDBContext dbContext)
    {
        _dbContext = dbContext;
        _authorRepository = new AuthorRepository(dbContext);
    }

    /// <summary>
    /// Gets a paginated list of cheeps by the specified author.
    /// </summary>
    /// <param name="page">The page number.</param>
    /// <param name="authorUsername">The username of the author.</param>
    /// <param name="pageSize">The number of cheeps per page.</param>
    /// <returns>A list of cheeps as <see cref="CheepDTO"/>.</returns>
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

    /// <summary>
    /// Gets a paginated list of all cheeps.
    /// </summary>
    /// <param name="page">The page number.</param>
    /// <returns>A list of cheeps as <see cref="CheepDTO"/>.</returns>
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

    /// <summary>
    /// Adds a new cheep.
    /// </summary>
    /// <param name="text">The text of the cheep.</param>
    /// <param name="authorId">The ID of the author.</param>
    public void AddCheep(string text, string authorId)
    {
        if (text.Length > 160) return;
        // Create a cheep object
        var newCheep = new Cheep
        {
            AuthorId = authorId,
            Author = _authorRepository.FindAuthorById(authorId),
            Text = text,
            TimeStamp = DateTime.UtcNow
        };

        // Add the new Cheep to the DbSet
        _dbContext.Cheeps.Add(newCheep);

        // Save changes to the database
        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Gets the total count of cheeps.
    /// </summary>
    /// <returns>The total count of cheeps.</returns>
    public int GetCheepCount()
    {
        return _dbContext.Cheeps.Count();
    }

    /// <summary>
    /// Gets the count of cheeps by the specified author.
    /// </summary>
    /// <param name="authorId">The ID of the author.</param>
    /// <returns>The count of cheeps by the author.</returns>
    public int GetCheepCountByAuthorId(string authorId)
    {
        return _dbContext.Cheeps.Where(cheep => cheep.AuthorId == authorId).Count();
    }

    /// <summary>
    /// Gets a list of cheeps from the specified authors.
    /// </summary>
    /// <param name="authors">The list of authors.</param>
    /// <param name="page">The page number.</param>
    /// <returns>A list of cheeps as <see cref="CheepDTO"/>.</returns>
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

    /// <summary>
    /// Edits the text of an existing cheep.
    /// </summary>
    /// <param name="cheepDTO">The cheep DTO.</param>
    /// <param name="text">The new text of the cheep.</param>
    /// <returns>The updated cheep as <see cref="CheepDTO"/>.</returns>
    public CheepDTO EditCheep(CheepDTO cheepDTO, string text)
    {
        if (text.Length > 160) return cheepDTO;
        var cheep = _dbContext.Cheeps.Find(cheepDTO.CheepId);
        cheep.Text = text;
        cheepDTO.Text = text;
        cheep.EditedTimeStamp = DateTime.UtcNow;
        cheepDTO.EditedTimeStamp = DateTime.UtcNow;
        _dbContext.SaveChanges();
        return cheepDTO;
    }

    /// <summary>
    /// Converts a cheep entity to a DTO.
    /// </summary>
    /// <param name="cheep">The cheep entity.</param>
    /// <returns>The cheep as <see cref="CheepDTO"/>.</returns>
    public static CheepDTO CheepToDTO(Cheep cheep)
    {
        return new CheepDTO
        {
            CheepId = cheep.CheepId,
            Text = cheep.Text,
            Author = cheep.Author.UserName!,
            TimeStamp = cheep.TimeStamp,
            EditedTimeStamp = cheep.EditedTimeStamp
        };
    }

    /// <summary>
    /// Deletes a cheep.
    /// </summary>
    /// <param name="cheep">The cheep DTO.</param>
    public void DeleteCheep(CheepDTO cheep)
    {
        var cheepToDelete = _dbContext.Cheeps.Find(cheep.CheepId);
        _dbContext.Cheeps.Remove(cheepToDelete);
        _dbContext.SaveChanges();
    }
}