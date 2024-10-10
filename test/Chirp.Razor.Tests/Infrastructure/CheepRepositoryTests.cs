using Chirp.Razor.Chirp.Core;
using Chirp.Razor.Chirp.Infrastructure.Chirp.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Chirp.Razor.Tests.Infrastructure;

public class CheepRepositoryTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private ICheepRepository _repository;

    public CheepRepositoryTests(ITestOutputHelper testOutputHelper)
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

        _repository = new CheepRepository(context);
        DbInitializer.SeedDatabase(context);
    }

    [Fact]
    public void CreateCheepTest()
    {
        _repository.CreateCheep("Hello World", 1);
        var lastCheep = _repository.GetLastCheep();
        Assert.NotNull(lastCheep);
        Assert.Equal("Hello World", lastCheep.Text);
        _testOutputHelper.WriteLine("Cheep 'Hello World' was found in DB with id " + lastCheep.CheepId);
        Assert.Equal(1, lastCheep.AuthorId);
    }
}