using System;
using Xunit;
using Chirp.Razor;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Chirp.Razor.Tests;

public class CheepDTOTest{
    [Fact]
    public void DtoConversion(){
        Author Gunner = new Author{AuthorId = 1, Name = "Gunner Nielsen", Email = "Yourmom@gmail.com", 
            Cheeps = new List<Cheep>()};
        Cheep cheep = new Cheep{CheepId = 1, AuthorId = 1, Author = Gunner, 
            Text = "It just do be like it is", TimeStamp = DateTime.Parse("2024-08-01 15:50:32")};
        
        CheepDTO dto = CheepRepository.CheepToDTO(cheep);
        Assert.Equal("Gunner Nielsen", dto.Author);
        Assert.Equal("It just do be like it is", dto.Text);
        Assert.Equal("2024-08-01 15:50:32", dto.TimeStamp);
    }

}