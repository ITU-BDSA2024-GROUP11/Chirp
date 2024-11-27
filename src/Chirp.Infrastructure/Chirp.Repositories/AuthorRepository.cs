using Chirp.Core.DTO;
using Chirp.Core.RepositoryInterfaces;
using Chirp.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure.Chirp.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly ChirpDBContext _dbContext;

    public AuthorRepository(ChirpDBContext dbContext)
    {
        _dbContext = dbContext;
    }

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

    public AuthorDTO GetAuthorByName(string name)
    {
        var result = FindAuthorByName(name);
        var authorDTO = AuthorToDTO(result);
        return authorDTO;
    }

    public AuthorDTO GetAuthorByEmail(string email)
    {
        var result = FindAuthorByEmail(email);
        var authorDTO = AuthorToDTO(result);
        return authorDTO;
    }

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

    public string GetAuthorID(string username)
    {
        var result = FindAuthorByName(username);
        return result.Id;
    }

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

    public void UnfollowAuthor(string userId, string followId)
    {
        var user = FindAuthorById(userId);
        var follow = FindAuthorById(followId);
        user.Follows.Remove(follow);
        _dbContext.SaveChanges();
    }

    public Author FindAuthorById(string id)
    {
        var query = from author in _dbContext.Authors
            where author.Id == id
            select author;
        var result = query.First();
        return result;
    }

    public Author FindAuthorByName(string name)
    {
        var query = from author in _dbContext.Authors
            where author.UserName == name
            select author;
        var result = query.First();
        return result;
    }

    private Author FindAuthorByEmail(string email)
    {
        var query = from author in _dbContext.Authors
            where author.Email == email
            select author;
        var result = query.FirstOrDefault();
        return result!;
    }

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
}