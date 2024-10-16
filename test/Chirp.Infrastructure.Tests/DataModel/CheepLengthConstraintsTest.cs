/*using System;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using Chirp.Infrastructure.DataModel;
using Chirp.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace Chirp.Infrastructure.Tests;

public class CheepLengthConstraintsTest {
    [Fact]

    public void cheepLength() {
        Author Lisa = new Author{AuthorId = 1, Name = "Lisa Hauge", Email = "Yourdad@gmail.com", 
            Cheeps = new List<Cheep>()};
        AddCheep("This one is allowed here", 1);

        Author Oliver = new Author{AuthorId = 2, Name = "Oliver Brinch", Email = "Yourmom@gmail.com", 
            Cheeps = new List<Cheep>()};
        AddCheep("This cheep right here is definetly not allowed, it is too fucking long, i will not accept it, it is plain stupid, but i mean what else could you expect, typical behaviour", 2);

        var connection = new SqliteConnection("Filename=:memory:");
        await connection.OpenAsync();
        var builder = new DbContextOptionsBuilder<ChirpDBContext>().UseSqlite(connection);

        var context = new ChirpDBContext(builder.Options);
        await context.Database.EnsureCreatedAsync();

        _repository = new AuthorRepository(context);
        DbInitializer.SeedDatabase(context);

        Assert.Equal("This one is allowed here", DbInitializer.SeedDatabase("This one is allowed here"));
        //Assert.Equal();
    }
}*/