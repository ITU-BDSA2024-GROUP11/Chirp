namespace Chirp.Razor;

public interface IAuthorRepository
{
    //AuthorDTO FindAuthorByName(string name);
    //AuthorDTO FindAuthorByEmail(string email);
    Author FindAuthorById(int id);
}

public class AuthorRepository : IAuthorRepository
{
    private readonly ChirpDBContext _dbContext;

    public AuthorRepository(ChirpDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    // public AuthorDTO FindAuthorByName(string name)
    // {
    //     var query = from author in _dbContext.Authors
    //         where author.Name == name
    //         select author;
    //     var result = query.First();
    //     var AuthorDTO = new AuthorDTO()
    //     {
    //         //???
    //     }
    //     return result;
    // }
    //
    // public AuthorDTO FindAuthorByEmail(string email)
    // {
    //     var query = from author in _dbContext.Authors
    //         where author.Email == email
    //         select author;
    //     var result = query.First();
    //     var AuthorDTO = new AuthorDTO()
    //     {
    //         //???
    //     }
    //     return result;
    // }

    public Author FindAuthorById(int id)
    {
        var query = from author in _dbContext.Authors
            where author.AuthorId == id
            select author;
        var result = query.First();
        return result;
    }
}