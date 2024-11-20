using Chirp.Infrastructure;
using Chirp.Infrastructure.Chirp.Repositories;
using Chirp.Infrastructure.DataModel;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Chirp.Razor.Tests;

public class PaginationTests
{
    private readonly CheepRepository _repository;
    private readonly Mock<IServiceProvider> _serviceProvider;

    public PaginationTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var options = new DbContextOptionsBuilder<ChirpDBContext>().UseSqlite(connection).Options;
        var context = new ChirpDBContext(options);
        context.Database.EnsureCreated();

        _serviceProvider = new Mock<IServiceProvider>();

        DbInitializer.SeedDatabase(context, _serviceProvider.Object);
        _repository = new CheepRepository(context);
    }

    [Fact]
    public void PaginationTest()
    {
        var cheeps = _repository.GetCheeps(1);
        Assert.Equal(32, cheeps.Count);
    }

    [Fact]
    public void PaginationUserTest()
    {
        var cheeps = _repository.GetCheeps(1, "Jacqualine Gilcoine");
        Assert.Equal("Jacqualine Gilcoine", cheeps[0].Author);
    }

    [Fact]
    public void PaginationUserPageTest()
    {
        var cheeps = _repository.GetCheeps(2, "Jacqualine Gilcoine");
        Assert.Equal("Jacqualine Gilcoine", cheeps[0].Author);
        Assert.Equal("What a relief it was the place examined.", cheeps[0].Text);
    }

    [Fact]
    public void PaginationSecondPageTest()
    {
        var cheeps = _repository.GetCheeps(2);
        Assert.Equal("Nathan Sirmon", cheeps[1].Author);
    }

}