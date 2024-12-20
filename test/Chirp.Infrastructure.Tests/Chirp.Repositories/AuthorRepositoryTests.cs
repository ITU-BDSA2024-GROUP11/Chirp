using Chirp.Core.RepositoryInterfaces;
using Chirp.Infrastructure.Chirp.Repositories;
using Chirp.Infrastructure.DataModel;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit.Abstractions;

namespace Chirp.Infrastructure.Tests;

public class AuthorRepositoryTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private IAuthorRepository _repository;
    private Mock<IServiceProvider> _serviceProvider;

    public AuthorRepositoryTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        Initializer();
    }

    private async void Initializer()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        await connection.OpenAsync();
        var builder = new DbContextOptionsBuilder<ChirpDBContext>().UseSqlite(connection);

        var context = new ChirpDBContext(builder.Options);
        await context.Database.EnsureCreatedAsync();

        _serviceProvider = new Mock<IServiceProvider>();
        _repository = new AuthorRepository(context);
        DbInitializerTest.SeedDatabase(context, _serviceProvider.Object);
    }

    [Fact]
    public void FindAuthorByIDTest()
    {
        // var result = _repository.FindAuthorById(1);
        // var result2 = _repository.FindAuthorById(11);
        //
        // Assert.Equal(1, result.AuthorId);
        // Assert.Equal("Roger Histand", result.Name);
        // Assert.Equal("Roger+Histand@hotmail.com", result.Email);
        //
        // Assert.Single(result2.Cheeps);
    }

    [Fact]
    public void GetAuthorByNameTest()
    {
        var result = _repository.GetAuthorByName("Roger Histand");
        var result2 = _repository.GetAuthorByName("Helge");

        Assert.Equal("Roger Histand", result.Name);
        Assert.Equal("Roger+Histand@hotmail.com", result.Email);

        Assert.Equal("Helge", result2.Name);
        Assert.Equal("ropf@itu.dk", result2.Email);
    }

    [Fact]
    public void CreateAuthorTest()
    {
        try
        {
            var result = _repository.GetAuthorByName("Jon Lehmann");
        }
        catch (Exception e)
        {
            _testOutputHelper.WriteLine(e.Message + " with value Jon Lehmann");
        }

        _repository.CreateAuthor("Jon Lehmann", "jble@itu.dk");
        // var result2 = _repository.FindAuthorByName("Jon Lehmann");
        // Assert.Equal("Jon Lehmann", result2.Name);
        // _testOutputHelper.WriteLine("Author Jon Lehmann was found in DB with id " + result2.AuthorId);
        // Assert.Equal(13, result2.AuthorId);
    }
}