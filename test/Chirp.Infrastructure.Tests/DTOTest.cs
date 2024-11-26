using Chirp.Infrastructure.Chirp.Repositories;
using Chirp.Infrastructure.DataModel;

namespace Chirp.Core.Tests;

public class DTOTest
{
    [Fact]
    public void CheepDtoConversion()
    {
        var Gunner = new Author
        {
            Id = "1", UserName = "Gunner Nielsen", Email = "Yourmom@gmail.com",
            Cheeps = new List<Cheep>(),
            Follows = new List<Author>()
        };
        var cheep = new Cheep
        {
            CheepId = 1, AuthorId = "1", Author = Gunner,
            Text = "It just do be like it is", TimeStamp = DateTime.Parse("2024-08-01 15:50:32")
        };

        var dto = CheepRepository.CheepToDTO(cheep);
        Assert.Equal("Gunner Nielsen", dto.Author);
        Assert.Equal("It just do be like it is", dto.Text);
        Assert.Equal("2024-08-01 15:50:32", dto.TimeStamp);
    }

    [Fact]
    public void AuthorDTOConversion()
    {
        var Mike = new Author
        {
            Id = "2", UserName = "Mike Oxlong", Email = "MikeOxlong@gmail.com",
            Cheeps = new List<Cheep>(),
            Follows = new List<Author>()
        };

        var dto = AuthorRepository.AuthorToDTO(Mike);
        Assert.Equal("Mike Oxlong", dto.Name);
        Assert.Equal("MikeOxlong@gmail.com", dto.Email);
    }
}