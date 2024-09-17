using System;
using Xunit;
using Chirp.CLI;

namespace Chirp.CLI.Client.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void TestUnixToDate()
        {
            long unixTime = 1633039200; // Example Unix timestamp (2021-10-01 00:00:00 UTC)
            string expectedDate = "10/01/21 00:00:00"; // Expected output in "MM/dd/yy HH:mm:ss" format

            string result = UserInterface.UnixToDate(unixTime);

            Assert.Equal(expectedDate, result);
        }

        [Fact]
        public void TestPrintCheeps(){
            
            string msg = "Author,Message,Timestamp
        ropf,"Hello, BDSA students!",1690891760
        adho,"Welcome to the course!",1690978778
        adho,"I hope you had a good summer.",1690979858
        ropf,"Cheeping cheeps on Chirp :)",1690981487";
            
            CsvDatabase<Cheep> db = new CsvDatabase<Cheep>();
            string dbresult = UserInterface.ReadCheeps();

        }
    
    }
}
