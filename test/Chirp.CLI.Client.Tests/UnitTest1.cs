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

            // Convert expected date to DateTime and ensure it's in UTC
            DateTime expectedDateTime = DateTime.ParseExact(expectedDate, "MM/dd/yy HH:mm:ss", null).ToUniversalTime();

            // Call the method from UserInterface and convert its result to UTC DateTime
            DateTime resultDateTime = DateTime.ParseExact(UserInterface.UnixToDate(unixTime), "MM/dd/yy HH:mm:ss", null).ToUniversalTime();

            Assert.Equal(expectedDateTime, resultDateTime);
        }
    }
}
