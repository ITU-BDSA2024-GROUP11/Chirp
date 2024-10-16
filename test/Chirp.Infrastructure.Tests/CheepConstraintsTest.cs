using System;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using Chirp.Infrastructure.DataModel;
using Chirp.Infrastructure.Chirp.Repositories;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace Chirp.Infrastructure.Tests;

public class CheepConstraintsTest {
    private CheepRepository _cheepRepository;
    private AuthorRepository _authorRepository;
    private readonly ITestOutputHelper _testOutputHelper;
    
    public CheepConstraintsTest(ITestOutputHelper testOutputHelper)
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

        _authorRepository = new AuthorRepository(context);
        _cheepRepository = new CheepRepository(context);
        DbInitializer.SeedDatabase(context);
    }

    [Fact]
    public void cheepLength() 
    {
        _authorRepository.CreateAuthor("Lisa Hauge", "Yourdad@gmail.com");
        _cheepRepository.AddCheep("This one is allowed here", 13);

        _authorRepository.CreateAuthor("Oliver Brinch", "Yourmom@gmail.com");
        _cheepRepository.AddCheep("This cheep right here is definetly not allowed, it is too fucking long, i will not accept it, it is plain stupid, but i mean what else could you expect, typical behaviour", 14);

        var list = _cheepRepository.GetCheeps(1, "Oliver Brinch");
        foreach (var cheep in list)
        {
            _testOutputHelper.WriteLine(cheep.Text);
        }
        Assert.Equal(1, _cheepRepository.GetCheeps(1, "Lisa Hauge").Count());
        Assert.Equal(0, _cheepRepository.GetCheeps(1, "Oliver Brinch").Count());
    }
}