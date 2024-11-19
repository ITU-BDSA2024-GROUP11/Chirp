using Chirp.Infrastructure;
using Chirp.Core.RepositoryInterfaces;
using Chirp.Infrastructure.Chirp.Repositories;
using Chirp.Infrastructure.DataModel;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq; // "dotnet add package Moq" i ./test


namespace Chirp.Infrastructure.Tests;

public class CheepServiceTest
{
    private readonly Mock<ICheepRepository> _mockCheepRepository;
    private readonly Mock<IAuthorRepository> _mockAuthorRepository;
    private readonly CheepRepository _cheeprepository;
    private readonly AuthorRepository _authorrepository;
    private readonly CheepService _realcheepService;
    private readonly CheepService _mockcheepservice;

    public CheepServiceTest()
    {

        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var options = new DbContextOptionsBuilder<ChirpDBContext>().UseSqlite(connection).Options;
        var context = new ChirpDBContext(options);
        context.Database.EnsureCreated();

        DbInitializer.SeedDatabase(context);

        _mockCheepRepository = new Mock<ICheepRepository>();
        _mockAuthorRepository = new Mock<IAuthorRepository>();
        _mockcheepservice = new CheepService(_mockCheepRepository.Object, _mockAuthorRepository.Object);

        _cheeprepository = new CheepRepository(context);
        _authorrepository = new AuthorRepository(context);
        _realcheepService = new CheepService(_cheeprepository, _authorrepository);
    }

    [Fact]
    public void GetAuthorID()
    {
        var username = "Adrian";
        var expected_id = _realcheepService.GetAuthorID(username);
        Assert.Equal(12, expected_id);
        _authorrepository.CreateAuthor("Oliver", "Oliver@mail.com");
        var expected_id2 = _realcheepService.GetAuthorID("Oliver");
        Assert.Equal(13, expected_id2);
    }

    [Fact]
    public void AddCheep()
    {
        var cheepText = "Hello fellas";
        var authorId = 13;
        _mockcheepservice.AddCheep(cheepText, authorId);

        _mockCheepRepository.Verify(repo => repo.AddCheep(cheepText, authorId), Times.Once);

    }

}