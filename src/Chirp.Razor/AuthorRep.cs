namespace Chirp.Razor;

public interface IAuthorRep
{
}

public class AuthorRep : IAuthorRep
{
    private readonly ChirpDBContext _dbContext;
    public AuthorRep(ChirpDBContext dbContext)
    {
        _dbContext = dbContext;
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