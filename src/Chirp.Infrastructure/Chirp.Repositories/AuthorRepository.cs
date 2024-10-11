namespace Chirp.Infrastructure.Chirp.Repositories;

public interface IAuthorRepository
{
    AuthorDTO GetAuthorByName(string name);
    AuthorDTO GetAuthorByEmail(string email);
    Author FindAuthorById(int id);
    void CreateAuthor(string name, string email);
    Author FindAuthorByName(string name);
}

public class AuthorRepository : IAuthorRepository
{
    private readonly ChirpDBContext _dbContext;

    public AuthorRepository(ChirpDBContext dbContext)
    {
        _dbContext = dbContext;
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

    public Author FindAuthorById(int id)
    {
        var query = from author in _dbContext.Authors
            where author.AuthorId == id
            select author;
        var result = query.First();
        return result;
    }

    public void CreateAuthor(string name, string email)
    {
        var author = new Author
        {
            Name = name,
            Email = email,
            Cheeps = new List<Cheep>()
        };
        _dbContext.Authors.Add(author);
        _dbContext.SaveChanges();
    }

    public Author FindAuthorByName(string name)
    {
        var query = from author in _dbContext.Authors
            where author.Name == name
            select author;
        var result = query.First();
        return result;
    }

    private Author FindAuthorByEmail(string email)
    {
        var query = from author in _dbContext.Authors
            where author.Email == email
            select author;
        var result = query.First();
        return result;
    }

    public static AuthorDTO AuthorToDTO(Author author)
    {
        return new AuthorDTO
        {
            Name = author.Name,
            Email = author.Email
        };
    }
}

public class AuthorDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
}