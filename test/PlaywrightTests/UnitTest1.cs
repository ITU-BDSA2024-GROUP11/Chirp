using System.Diagnostics;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Data.SqlClient;
using System.Data.SQLite;




[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class ExampleTest : PageTest
{

    private const string baseUrl = "http://localhost:5273";
    private Process? _serverProcess;
    private string _startupProjectPath;

    [OneTimeSetUp]
    public async Task SetupLocalServer()
    {
        // Base directory of the test assembly
        var baseDirectory = AppContext.BaseDirectory;

        // Adjust the relative path to your solution root
        var solutionDirectory = Path.GetFullPath(Path.Combine(baseDirectory, "..", "..", "..", "..", ".."));

        if (!Directory.Exists(solutionDirectory))
        {
            throw new DirectoryNotFoundException($"Solution directory not found: {solutionDirectory}");
        }

        // Construct the path to your project
        _startupProjectPath = Path.Combine(solutionDirectory, "src", "Chirp.Web", "Chirp.Web.csproj");

        if (!File.Exists(_startupProjectPath))
        {
            throw new FileNotFoundException($"Startup project file not found: {_startupProjectPath}");
        }
        

        
        // Set environment variable
        Environment.SetEnvironmentVariable("ASPNETCORE_URLS", baseUrl);

        // Start the local server
        _serverProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = $"run --project \"{_startupProjectPath}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
            }
        };

        _serverProcess.Start();

        // Wait for the application to start
        await Task.Delay(15000); // Adjust delay if necessary
    }



    //Tear down does not work
    [OneTimeTearDown]
    public void TeardownLocalServer()
    {
        if (_serverProcess != null && !_serverProcess.HasExited)
        {
            _serverProcess.Kill();
            _serverProcess.WaitForExit();  // Optionally wait for the process to terminate
            _serverProcess.Dispose();
        }
        try
        {
            DeleteDatabase();
            Console.WriteLine("Database deleted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to delete the database: {ex.Message}");
        }
    }
    private void DeleteDatabase()
    {
        // Get the base directory of the test assembly (your UnitTest1.cs file)
        var testBaseDirectory = AppContext.BaseDirectory;

        // Construct the relative path to the chirp.db-wal file
        var dbWalPath = Path.Combine(testBaseDirectory, "..", "..", "..", "src", "Chirp.Web", "chirp.db-wal");

        // Normalize the path (this is optional but helps in case of directory traversal issues)
        dbWalPath = Path.GetFullPath(dbWalPath);

        File.Delete("../../../src/Chirp.Web/chirp.db-shm");
        //File.Delete("C:/Users/basti/Downloads/thirdSemester/Chirp.CLI/src/Chirp.Web/chirp.db-shm");
        
        // Delete the file if it exists
        //File.Delete(dbWalPath);
    }

    private void DeleteFileIfExists(string filePath)
    {
        if (File.Exists(filePath))
        {
            try
            {
                File.Delete(filePath);
                Console.WriteLine($"File {filePath} deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete {filePath}: {ex.Message}");
            }
        }
    }




    [Test]
    public async Task GetStartedLink()
    {
        await Page.GotoAsync($"{baseUrl}/");
        await Expect(Page).ToHaveTitleAsync(new Regex("Public Timeline"));
    }


    [Test]
    public async Task ClickOnUser()
    {
        await Page.GotoAsync($"{baseUrl}/");
        await Page.Locator("p").Filter(new() { HasText = "Jacqualine Gilcoine Seems to" })
            .GetByRole(AriaRole.Link)
            .ClickAsync();
        Assert.AreEqual($"{baseUrl}/Jacqualine%20Gilcoine", Page.Url);
    }

    [Test]
    public async Task Registerbutton()
    {
        await Page.GotoAsync($"{baseUrl}/");
        await Page.GetByRole(AriaRole.Link, new() { Name = "register" }).ClickAsync();
        Assert.AreEqual($"{baseUrl}/Identity/Account/Register", Page.Url);
    }
    [Test]
    public async Task LoginButton()
    {
        await Page.GotoAsync($"{baseUrl}/");
        await Page.GetByRole(AriaRole.Link, new() { Name = "login" }).ClickAsync();
        Assert.AreEqual($"{baseUrl}/Identity/Account/Login", Page.Url);
    }

    [Test]
    public async Task NextPageButton()
    {
        await Page.GotoAsync($"{baseUrl}/");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Next" }).ClickAsync();
        Assert.AreEqual($"{baseUrl}/?page=2", Page.Url);
    }
    [Test]
    public async Task PreviousPageButton()
    {
        await Page.GotoAsync($"{baseUrl}/?page=2");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Previous" }).ClickAsync();
        Assert.AreEqual($"{baseUrl}/?page=1", Page.Url);
    }

    [Test]
    public async Task NextPageButtonOnLastPage()
    {
        await Page.GotoAsync($"{baseUrl}/?page=21");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Next" }).ClickAsync();
        Assert.AreEqual($"{baseUrl}/?page=21", Page.Url);
    }
    [Test]
    public async Task PreviousPageButtonOnPage1()
    {
        await Page.GotoAsync($"{baseUrl}/?page=1");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Previous" }).ClickAsync();
        Assert.AreEqual($"{baseUrl}/?page=1", Page.Url);
    }
    
    [Test]
    public async Task FirstPageButton()
    {
        await Page.GotoAsync($"{baseUrl}/?page=21");
        await Page.GetByRole(AriaRole.Button, new() { Name = "First" }).ClickAsync();
        Assert.AreEqual($"{baseUrl}/?page=1", Page.Url);
    }
    [Test]
    public async Task LastPageButton()
    {
        await Page.GotoAsync($"{baseUrl}/");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Last" }).ClickAsync();
        Assert.AreEqual($"{baseUrl}/?page=21", Page.Url);
    }
    
    [Test]
    public async Task PublictimelineButton()
    {
        await Page.GotoAsync($"{baseUrl}/?page=5");
        await Page.GetByRole(AriaRole.Link, new() { Name = "public timeline" }).ClickAsync();
        Assert.AreEqual($"{baseUrl}/", Page.Url);
    }

    /*
    //This test requires the users to not already be registered
    [Test]
    public async Task RegisterAcount()
    {
        await Page.GotoAsync($"{baseUrl}/");
        await Page.GetByRole(AriaRole.Link, new() { Name = "register" }).ClickAsync();
        await Page.GetByLabel("UserName").ClickAsync();
        await Page.GetByLabel("UserName").FillAsync("TestUser");
        await Page.GetByLabel("UserName").PressAsync("Tab");
        await Page.GetByPlaceholder("name@example.com").FillAsync("TestUser@testmail.com");
        await Page.GetByPlaceholder("name@example.com").PressAsync("Tab");
        await Page.GetByLabel("Password", new() { Exact = true }).FillAsync("TestPassword1!");
        await Page.GetByLabel("Password", new() { Exact = true }).PressAsync("Tab");
        await Page.GetByLabel("Confirm Password").FillAsync("TestPassword1!");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "Click here to confirm your" }).ClickAsync();

        //Assert.AreEqual($"{baseUrl}/Identity/Account/ConfirmEmail?userId=53398aa4-e617-4cf6-be4b-47e8544806f3&code=Q2ZESjhDZ0tEZFFiUTQ5Qm1uK2JTVDFsTlU3YTZvS1kvNEJ1MmV3dVFmZUZsMWl5KzJnQUZxbEJ0aHpzZHpNV0Yyc2ZqQ3o0c21SYThKaEw4MHE1VWVhTWVqZkFVSjU0K2VJaVNFYTdCS2hQb1p6SndqN0J0b0Y1Wk5uVERFT1VzSmNxY3RiZ25LazJKWGxLeXM4QVVMeWVMYktFTk9MeFA1UXh1dVNYZUV1M2tjLzhlU0J0VC8xNWp4WU9MZEtjMUNwUzd0VU5PeC9aRmZYV3hsMHMwS0pldjhJNW1PUlpDOWZ3U3hXNEUzRG0yVDBabGJZZURWTnR5SlkxdFRqNkExZjkrZz09&returnUrl=%2F", Page.Url);
        //await Page.GetByRole(AriaRole.Link, new() { Name = "Click here to confirm your" }).ClickAsync();
        //Console.WriteLine(await Page.GetByText("Thank you for confirming your").TextContentAsync());
    }
    */

    /*
    public async Task Login
    */
    /*
    public async Task MytimelineButton
    */
    
    
}
