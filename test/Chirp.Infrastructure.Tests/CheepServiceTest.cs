using Chirp.Infrastructure;
using Chirp.Core.RepositoryInterfaces;
using Chirp.Infrastructure.Chirp.Repositories;
using Chirp.Infrastructure.DataModel;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
    private readonly Mock<IServiceProvider> _serviceProvider;

    public CheepServiceTest()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var options = new DbContextOptionsBuilder<ChirpDBContext>().UseSqlite(connection).Options;
        var context = new ChirpDBContext(options);
        _serviceProvider = new Mock<IServiceProvider>();
        context.Database.EnsureCreated();

        DbInitializerTest.SeedDatabase(context, _serviceProvider.Object);

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
        Assert.Equal("915ae556-b0d8-4c90-982f-ad0fa74ec85b", expected_id);

    }

    [Fact]
    public void AddCheep()
    {
        var cheepText = "Hello fellas";
        var authorId = "testId";
        _mockcheepservice.AddCheep(cheepText, authorId);

        _mockCheepRepository.Verify(repo => repo.AddCheep(cheepText, authorId), Times.Once);

    }

}