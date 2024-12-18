using Chirp.Core.DTO;
using Chirp.Core.RepositoryInterfaces;
using Chirp.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure.Chirp.Repositories;

/// <summary>
/// Repository class for managing author data in the database.
/// </summary>
public class AuthorRepository : IAuthorRepository
{
    private readonly ChirpDBContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The database context to be used.</param>
    public AuthorRepository(ChirpDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Gets the list of authors followed by the specified user.
    /// </summary>
    /// <param name="userName">The username of the user.</param>
    /// <returns>A list of followed authors as <see cref="AuthorDTO"/>.</returns>
    public List<AuthorDTO> GetFollowedAuthors(string userName)
    {
        var user = _dbContext.Authors
            .Where(a => a.UserName == userName)
            .Include(a => a.Follows)
            .First(a => a.UserName == userName);
        var authors = user.Follows;
        var authorDTOs = authors.Select(AuthorToDTO).ToList();
        return authorDTOs;
    }

    /// <summary>
    /// Gets the author by their name.
    /// </summary>
    /// <param name="name">The name of the author.</param>
    /// <returns>The author as <see cref="AuthorDTO"/>.</returns>
    public AuthorDTO GetAuthorByName(string name)
    {
        var result = FindAuthorByName(name);
        var authorDTO = AuthorToDTO(result);
        return authorDTO;
    }

    /// <summary>
    /// Creates a new author with the specified name and email.
    /// </summary>
    /// <param name="name">The name of the author.</param>
    /// <param name="email">The email of the author.</param>
    public void CreateAuthor(string name, string email)
    {
        var author = new Author
        {
            UserName = name,
            Email = email,
            Cheeps = new List<Cheep>(),
            Follows = new List<Author>()
        };
        _dbContext.Authors.Add(author);
        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Gets the ID of the author by their username.
    /// </summary>
    /// <param name="username">The username of the author.</param>
    /// <returns>The ID of the author.</returns>
    public string GetAuthorID(string username)
    {
        var result = FindAuthorByName(username);
        return result.Id;
    }

    /// <summary>
    /// Follows an author.
    /// </summary>
    /// <param name="userId">The ID of the user who wants to follow.</param>
    /// <param name="followId">The ID of the author to be followed.</param>
    public void FollowAuthor(string userId, string followId)
    {
        var user = _dbContext.Authors
            .Where(a => a.Id == userId)
            .Include(a => a.Follows)
            .First(a => a.Id == userId);
        var follow = FindAuthorById(followId);

        // Check if already following
        user.Follows.Add(follow);
        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Unfollows an author.
    /// </summary>
    /// <param name="userId">The ID of the user who wants to unfollow.</param>
    /// <param name="followId">The ID of the author to be unfollowed.</param>
    public void UnfollowAuthor(string userId, string followId)
    {
        var user = FindAuthorById(userId);
        var follow = FindAuthorById(followId);
        user.Follows.Remove(follow);
        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Finds an author by their ID.
    /// </summary>
    /// <param name="id">The ID of the author.</param>
    /// <returns>The author entity.</returns>
    public Author FindAuthorById(string id)
    {
        var query = from author in _dbContext.Authors
            where author.Id == id
            select author;
        var result = query.First();
        return result;
    }

    /// <summary>
    /// Finds an author by their name.
    /// </summary>
    /// <param name="name">The name of the author.</param>
    /// <returns>The author entity.</returns>
    public Author FindAuthorByName(string name)
    {
        var query = from author in _dbContext.Authors
            where author.UserName == name
            select author;
        var result = query.First();
        return result;
    }

    /// <summary>
    /// Converts an author entity to a DTO.
    /// </summary>
    /// <param name="author">The author entity.</param>
    /// <returns>The author as <see cref="AuthorDTO"/>.</returns>
    public static AuthorDTO AuthorToDTO(Author author)
    {
        return new AuthorDTO
        {
            Name = author.UserName ?? throw new InvalidOperationException("Username cannot be null"),
            Email = author.Email ?? throw new InvalidOperationException("Email cannot be null"),
            Id = author.Id,
            Follows = author.Follows.Select(AuthorToDTO).ToList()
        };
    }

    /// <summary>
    /// Removes all follows for a user.
    /// </summary>
    /// <param name="userName">The username of the user.</param>
    public void RemoveFollows(string userName)
    {
        var userId = GetAuthorID(userName);
        var user = FindAuthorById(userId);
        foreach (var follow in user.Follows)
        {
            UnfollowAuthor(userId, follow.Id);
        }
    }
}