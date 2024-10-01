using System;
using Xunit;
using Chirp.Razor;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Chirp.Razor.Tests;

    public class UnitTest1
    {
        [Fact]
        public void PaginationTest()
        {
            AuthorFacade authorFacade = new AuthorFacade();
            var cheeps = authorFacade.GetCheepsFromPage(1);
            Assert.Equal(32, cheeps.Count);
        }
        
        [Fact]
        public void PaginationUserTest()
        {
            AuthorFacade authorFacade = new AuthorFacade();
            var cheeps = authorFacade.GetCheepsFromPage(1);
            Assert.Equal("Jacqualine Gilcoine", cheeps[0].username);
        }
        [Fact]
        public void PaginationSecondPageTest()
        {
            AuthorFacade authorFacade = new AuthorFacade();
            var cheeps = authorFacade.GetCheepsFromPage(2);
            Assert.Equal("Nathan Sirmon", cheeps[1].username);
        }
        
        [Fact]
        public void DBPathNullToDefaultTest()
        {
            AuthorFacade authorFacade = new AuthorFacade();
            Assert.Equal("/tmp/chirp.db", authorFacade.getDbPath());
        }
        
    }