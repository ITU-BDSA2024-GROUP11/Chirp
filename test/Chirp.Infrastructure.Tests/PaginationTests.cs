// namespace Chirp.Razor.Tests;
//
// public class PaginationTests
// {
//     [Fact]
//     public void PaginationTest()
//     {
//         var dbFacade = new DBFacade();
//         var cheeps = dbFacade.GetCheepsFromPage(1);
//         Assert.Equal(32, cheeps.Count);
//     }
//
//     [Fact]
//     public void PaginationUserTest()
//     {
//         var dbFacade = new DBFacade();
//         var cheeps = dbFacade.GetCheepsFromPage(1);
//         Assert.Equal("Jacqualine Gilcoine", cheeps[0].username);
//     }
//
//     [Fact]
//     public void PaginationUserPageTest()
//     {
//         var dBFacade = new DBFacade();
//         var cheeps = dBFacade.GetCheepsFromAuthorPage("Jacqualine Gilcoine", 2);
//         Assert.Equal("Jacqualine Gilcoine", cheeps[0].username);
//         Assert.Equal("What a relief it was the place examined.", cheeps[0].message);
//     }
//
//     [Fact]
//     public void PaginationSecondPageTest()
//     {
//         var dbFacade = new DBFacade();
//         var cheeps = dbFacade.GetCheepsFromPage(2);
//         Assert.Equal("Nathan Sirmon", cheeps[1].username);
//     }
//
//     [Fact]
//     public void DBPathNullToDefaultTest()
//     {
//         var dbFacade = new DBFacade();
//         Assert.Equal("/tmp/chirp.db", dbFacade.getDbPath());
//     }
// }

namespace Chirp.Infrastructure.Tests;