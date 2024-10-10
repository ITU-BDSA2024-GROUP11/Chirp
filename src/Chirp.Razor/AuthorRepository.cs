namespace Chirp.Razor;

public interface IAuthorRepository
{
    AuthorDTO FindAuthorByName(string name);
    AuthorDTO FindAuthorByEmail(string email);

    Author FindAuthorById(int id);
}

public class AuthorRepository : IAuthorRepository
{
    private readonly ChirpDBContext _dbContext;

    public AuthorRepository(ChirpDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Author FindAuthorById(int id)
    {
        var query = from author in _dbContext.Authors
            where author.AuthorId == id
            select author;
        var result = query.First();
        return result;
    }

    public AuthorDTO FindAuthorByName(string name)
    {
        var query = from author in _dbContext.Authors
            where author.Name == name
            select author;
        var result = query.First();
        var authorDTO = AuthorToDTO(result);
        return authorDTO;
    }

    public AuthorDTO FindAuthorByEmail(string email)
    {
        var query = from author in _dbContext.Authors
            where author.Email == email
            select author;
        var result = query.First();
        var authorDTO = AuthorToDTO(result);
        return authorDTO;
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