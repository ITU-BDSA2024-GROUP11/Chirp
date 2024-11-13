using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class ExampleTest : PageTest
{
    [Test]
    public async Task GetStartedLink()
    {
        await Page.GotoAsync("http://localhost:5273/");
        await Expect(Page).ToHaveTitleAsync(new Regex("Chirp!"));
    }

    [Test]
    public async Task ClickOnUser()
    {
        await Page.GotoAsync("http://localhost:5273/");
        await Page.Locator("p").Filter(new() { HasText = "Jacqualine Gilcoine Seems to" })
            .GetByRole(AriaRole.Link)
            .ClickAsync();
        Assert.AreEqual("http://localhost:5273/Jacqualine%20Gilcoine", Page.Url);
    }

    [Test]
    public async Task Registerbutton()
    {
        await Page.GotoAsync("http://localhost:5273/");
        await Page.GetByRole(AriaRole.Link, new() { Name = "register" }).ClickAsync();
        Assert.AreEqual("http://localhost:5273/Identity/Account/Register", Page.Url);
    }
    [Test]
    public async Task LoginButton()
    {
        await Page.GotoAsync("http://localhost:5273/");
        await Page.GetByRole(AriaRole.Link, new() { Name = "login" }).ClickAsync();
        Assert.AreEqual("http://localhost:5273/Identity/Account/Login", Page.Url);
    }

    [Test]
    public async Task NextPageButton()
    {
        await Page.GotoAsync("http://localhost:5273/");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Next" }).ClickAsync();
        Assert.AreEqual("http://localhost:5273/?page=2", Page.Url);
    }
    [Test]
    public async Task PreviousPageButton()
    {
        await Page.GotoAsync("http://localhost:5273/?page=2");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Previous" }).ClickAsync();
        Assert.AreEqual("http://localhost:5273/?page=1", Page.Url);
    }

    [Test]
    public async Task NextPageButtonOnLastPage()
    {
        await Page.GotoAsync("http://localhost:5273/?page=21");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Next" }).ClickAsync();
        Assert.AreEqual("http://localhost:5273/?page=21", Page.Url);
    }
    [Test]
    public async Task PreviousPageButtonOnPage1()
    {
        await Page.GotoAsync("http://localhost:5273/?page=1");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Previous" }).ClickAsync();
        Assert.AreEqual("http://localhost:5273/?page=1", Page.Url);
    }
    
    [Test]
    public async Task FirstPageButton()
    {
        await Page.GotoAsync("http://localhost:5273/?page=21");
        await Page.GetByRole(AriaRole.Button, new() { Name = "First" }).ClickAsync();
        Assert.AreEqual("http://localhost:5273/?page=1", Page.Url);
    }
    [Test]
    public async Task LastPageButton()
    {
        await Page.GotoAsync("http://localhost:5273/");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Last" }).ClickAsync();
        Assert.AreEqual("http://localhost:5273/?page=21", Page.Url);
    }
    
    [Test]
    public async Task PublictimelineButton()
    {
        await Page.GotoAsync("http://localhost:5273/?page=5");
        await Page.GetByRole(AriaRole.Link, new() { Name = "public timeline" }).ClickAsync();
        Assert.AreEqual("http://localhost:5273/", Page.Url);
    }

    //This test requires the users to not already be registered
    [Test]
    public async Task RegisterAcount()
    {
        await Page.GotoAsync("http://localhost:5273/");
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

        //Assert.AreEqual("http://localhost:5273/Identity/Account/ConfirmEmail?userId=53398aa4-e617-4cf6-be4b-47e8544806f3&code=Q2ZESjhDZ0tEZFFiUTQ5Qm1uK2JTVDFsTlU3YTZvS1kvNEJ1MmV3dVFmZUZsMWl5KzJnQUZxbEJ0aHpzZHpNV0Yyc2ZqQ3o0c21SYThKaEw4MHE1VWVhTWVqZkFVSjU0K2VJaVNFYTdCS2hQb1p6SndqN0J0b0Y1Wk5uVERFT1VzSmNxY3RiZ25LazJKWGxLeXM4QVVMeWVMYktFTk9MeFA1UXh1dVNYZUV1M2tjLzhlU0J0VC8xNWp4WU9MZEtjMUNwUzd0VU5PeC9aRmZYV3hsMHMwS0pldjhJNW1PUlpDOWZ3U3hXNEUzRG0yVDBabGJZZURWTnR5SlkxdFRqNkExZjkrZz09&returnUrl=%2F", Page.Url);
        //await Page.GetByRole(AriaRole.Link, new() { Name = "Click here to confirm your" }).ClickAsync();
        //Console.WriteLine(await Page.GetByText("Thank you for confirming your").TextContentAsync());
    }
    
    /*
    public async Task Login
    */
    /*
    public async Task MytimelineButton
    */
    
    
}
