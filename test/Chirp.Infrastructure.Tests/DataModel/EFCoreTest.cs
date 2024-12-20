using Chirp.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure.Tests;

public class EFCoreTest
{
    [Fact]
    public void CheepTest()
    {
        var author = new Author
        {
            UserName = "John Doe",
            Email = "",
            Id = "1",
            Cheeps = new List<Cheep>(),
            Follows = new List<Author>()
        };
        var timestamp = DateTime.UtcNow;
        var cheep = new Cheep { Text = "Hello", TimeStamp = timestamp, Author = author, AuthorId = "1", CheepId = 1 };
        Assert.Equal("Hello", cheep.Text);
        Assert.Equal(timestamp, cheep.TimeStamp);
        Assert.Equal("1", cheep.AuthorId);
        Assert.Equal(1, cheep.CheepId);
    }

    [Fact]
    public void AuthorTest()
    {
        var author = new Author
        {
            UserName = "John Doe",
            Email = "jd@gmail.com", Id = "1",
            Cheeps = new List<Cheep>(),
            Follows = new List<Author>()
        };
        Assert.Equal("John Doe", author.UserName);
        Assert.Equal("jd@gmail.com", author.Email);
        Assert.Equal("1", author.Id);
    }

    [Fact]
    public void AuthorCheepTest()
    {
        var timestamp = DateTime.UtcNow;
        var author = new Author { UserName = "John Doe", Email = "jd@gmail.com", Id = "1", Cheeps = new List<Cheep>(), Follows = new List<Author>() };
        var cheep = new Cheep { AuthorId = "1", CheepId = 1, Author = author, Text = "Hello", TimeStamp = timestamp };
        author.Cheeps.Add(cheep);
        Assert.Single(author.Cheeps);
        Assert.Equal(cheep, author.Cheeps[0]);
    }

    [Fact]
    public void DBTest()
    {
        var options = new DbContextOptionsBuilder<ChirpDBContext>()
            .Options;
        using (var context = new ChirpDBContext(options))
        {
        }
    }
}