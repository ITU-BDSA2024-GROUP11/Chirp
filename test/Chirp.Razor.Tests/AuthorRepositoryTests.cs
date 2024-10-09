using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Razor.Tests;

public class AuthorRepositoryTests
{
    private IAuthorRepository _repository;

    public AuthorRepositoryTests()
    {
        Initializer();
    }

    private async void Initializer()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        await connection.OpenAsync();
        var builder = new DbContextOptionsBuilder<ChirpDBContext>().UseSqlite(connection);

        var context = new ChirpDBContext(builder.Options);
        await context.Database.EnsureCreatedAsync();

        _repository = new AuthorRepository(context);
        DbInitializer.SeedDatabase(context);
    }

    [Fact]
public async void FindAuthorByIDTest()
    {
        var result = _repository.FindAuthorById(1);
        var result2 = _repository.FindAuthorById(11);

        Assert.Equal(1, result.AuthorId);
        Assert.Equal("Roger Histand", result.Name);
        Assert.Equal("Roger+Histand@hotmail.com", result.Email);

        Assert.Single(result2.Cheeps);
    }
}