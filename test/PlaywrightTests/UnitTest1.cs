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
        await Task.Delay(10000); // Adjust delay if necessary
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
        // Base directory of the test assembly
        var baseDirectory = AppContext.BaseDirectory;

        var solutionDirectory = Path.GetFullPath(Path.Combine(baseDirectory, "..", "..", "..", "..", ".."));
        
        var dbwalFilepath = Path.Combine(solutionDirectory, "src", "Chirp.Web", "chirp.db-wal");
        var dbshmlFilepath = Path.Combine(solutionDirectory, "src", "Chirp.Web", "chirp.db-shm");
        var dbFilepath = Path.Combine(solutionDirectory, "src", "Chirp.Web", "chirp.db");
        DeleteFileIfExists(dbwalFilepath);
        DeleteFileIfExists(dbshmlFilepath);
        DeleteFileIfExists(dbFilepath);
        
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
        await Page.Locator("li").Filter(new() { HasText = "Jacqualine Gilcoine Save Starbuck now is what we hear the worst. — 01/08/2023" }).GetByRole(AriaRole.Link).ClickAsync();
        Assert.That(Page.Url,Is.EqualTo($"{baseUrl}/Jacqualine%20Gilcoine"));
    }

    [Test]
    public async Task Registerbutton()
    {
        await Page.GotoAsync($"{baseUrl}/");
        await Page.GetByRole(AriaRole.Link, new() { Name = "register" }).ClickAsync();
        Assert.That(Page.Url, Is.EqualTo($"{baseUrl}/Identity/Account/Register"));
    }
    [Test]
    public async Task LoginButton()
    {
        await Page.GotoAsync($"{baseUrl}/");
        await Page.GetByRole(AriaRole.Link, new() { Name = "login" }).ClickAsync();
        Assert.That(Page.Url, Is.EqualTo($"{baseUrl}/Identity/Account/Login"));
    }

    [Test]
    public async Task NextPageButton()
    {
        await Page.GotoAsync($"{baseUrl}/");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Next" }).ClickAsync();
        Assert.That(Page.Url, Is.EqualTo($"{baseUrl}/?page=2"));
    }
    [Test]
    public async Task PreviousPageButton()
    {
        await Page.GotoAsync($"{baseUrl}/?page=2");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Previous" }).ClickAsync();
        Assert.That(Page.Url, Is.EqualTo($"{baseUrl}/?page=1"));
    }

    [Test]
    public async Task NextPageButtonOnLastPage()
    {
        await Page.GotoAsync($"{baseUrl}/?page=21");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Next" }).ClickAsync();
        Assert.That(Page.Url, Is.EqualTo($"{baseUrl}/?page=21"));
    }
    [Test]
    public async Task PreviousPageButtonOnPage1()
    {
        await Page.GotoAsync($"{baseUrl}/?page=1");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Previous" }).ClickAsync();
        Assert.That(Page.Url, Is.EqualTo($"{baseUrl}/?page=1"));
    }
    
    [Test]
    public async Task FirstPageButton()
    {
        await Page.GotoAsync($"{baseUrl}/?page=21");
        await Page.GetByRole(AriaRole.Button, new() { Name = "First" }).ClickAsync();
        Assert.That(Page.Url, Is.EqualTo($"{baseUrl}/?page=1"));
    }
    [Test]
    public async Task LastPageButton()
    {
        await Page.GotoAsync($"{baseUrl}/");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Last" }).ClickAsync();
        Assert.That(Page.Url, Is.EqualTo($"{baseUrl}/?page=21"));
    }
    
    [Test]
    public async Task PublictimelineButton()
    {
        await Page.GotoAsync($"{baseUrl}/?page=5");
        await Page.GetByRole(AriaRole.Link, new() { Name = "public timeline" }).ClickAsync();
        Assert.That(Page.Url, Is.EqualTo($"{baseUrl}/"));
    }

    
    //This test requires the users to not already be registered
    [Test]
    public async Task End2endtest()
    {
        await Page.GotoAsync("http://localhost:5273/");
        await Page.GetByRole(AriaRole.Link, new() { Name = "register" }).ClickAsync();
        Assert.That(Page.Url, Is.EqualTo($"{baseUrl}/Identity/Account/Register"));
        await Page.GetByPlaceholder("user name").ClickAsync();
        await Page.GetByPlaceholder("user name").FillAsync("TestUser");
        await Page.GetByPlaceholder("user name").PressAsync("Tab");
        await Page.GetByPlaceholder("name@example.com").FillAsync("test@mail.dk");
        await Page.GetByPlaceholder("name@example.com").PressAsync("Tab");
        await Page.GetByLabel("Password", new() { Exact = true }).FillAsync("Test123!");
        await Page.GetByLabel("Password", new() { Exact = true }).PressAsync("Tab");
        await Page.GetByLabel("Confirm Password").FillAsync("Test123!");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();
        await Page.Locator("#cheepText").ClickAsync();
        await Page.Locator("#cheepText").FillAsync("I do not have a mind, I am simply a test user");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Share" }).ClickAsync();
        await Page.Locator("li").Filter(new() { HasText = "Jacqualine Gilcoine Follow Save Starbuck now is what we hear the worst. — 01/08/2023" }).GetByRole(AriaRole.Link).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "my timeline" }).ClickAsync();
        Assert.That(Page.Url, Is.EqualTo($"{baseUrl}/TestUser"));
        await Page.GetByRole(AriaRole.Link, new() { Name = "public timeline" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "logout [TestUser]" }).ClickAsync();
        await Task.Delay(1000); // Adjust delay if necessary
        Assert.That(Page.Url, Is.EqualTo($"{baseUrl}/"));
    }

    [Test]
    public async Task LoginAndMyTimelineButton()
    {
        await Page.GotoAsync($"{baseUrl}/");
        await Page.GetByRole(AriaRole.Link, new() { Name = "login" }).ClickAsync();
        Assert.That(Page.Url, Is.EqualTo($"{baseUrl}/Identity/Account/Login"));
        await Page.GetByPlaceholder("user name").ClickAsync();
        await Page.GetByPlaceholder("user name").FillAsync("TestUser");
        await Page.GetByPlaceholder("user name").PressAsync("Tab");
        await Page.GetByPlaceholder("password").FillAsync("Test123!");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
        Assert.That(Page.Url, Is.EqualTo($"{baseUrl}/"));

    }
    
}
