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
            DBFacade dbFacade = new DBFacade();
            var cheeps = dbFacade.GetCheepsFromPage(1);
            Assert.Equal(32, cheeps.Count);
        }
        
        [Fact]
        public void PaginationUserTest()
        {
            DBFacade dbFacade = new DBFacade();
            var cheeps = dbFacade.GetCheepsFromPage(1);
            Assert.Equal("Jacqualine Gilcoine", cheeps[0].username);
        }
        [Fact]
        public void PaginationSecondPageTest()
        {
            DBFacade dbFacade = new DBFacade();
            var cheeps = dbFacade.GetCheepsFromPage(2);
            Assert.Equal("Nathan Sirmon", cheeps[1].username);
        }
        
        [Fact]
        public void DBPathNullToDefaultTest()
        {
            DBFacade dbFacade = new DBFacade();
            Assert.Equal("/tmp/chirp.db", dbFacade.getDbPath());
        }
        
    }