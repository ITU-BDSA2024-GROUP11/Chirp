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
            
        }
    
    }
}
