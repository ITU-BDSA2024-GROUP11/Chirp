using System;
using Xunit;
using Chirp.Razor;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Chirp.Razor.Tests;

public class CheepLengthConstraintsTest {
    [Fact]

    public void cheepLength() {
        Author Lisa = new Author{AuthorId = 1, Name = "Lisa Hauge", Email = "Yourdad@gmail.com", 
            Cheeps = new List<Cheep>()};
        Cheep cheep1 = new Cheep{CheepId = 1, AuthorId = 1, Author = Lisa, 
            Text = "This one is allowed here", TimeStamp = DateTime.Parse("2024-09-01 14:58:00")};

        Author Oliver = new Author{AuthorId = 2, Name = "Oliver Brinch", Email = "Yourmom@gmail.com", 
            Cheeps = new List<Cheep>()};
        Cheep cheep2 = new Cheep{CheepId = 2, AuthorId = 2, Author = Oliver, 
            Text = "This cheep right here is definetly not allowed, it is too fucking long, i will not accept it, it is plain stupid, but i mean what else could you expect, typical behaviour", 
            TimeStamp = DateTime.Parse("2024-08-01 15:00:00")};

        CheepDTO dto = CheepRepository.CheepToDTO(cheep1);
        CheepDTO dto = CheepRepository.CheepToDTO(cheep2);
        AssertTrue(DbInitializer.contains(cheep1));
        AssertFalse(DbInitializer.contains(cheep2));
    }
}