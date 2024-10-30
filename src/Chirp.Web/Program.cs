using Chirp.Core.CheepServiceInterface;
using Chirp.Core.RepositoryInterfaces;
using Chirp.Infrastructure;
using Chirp.Infrastructure.Chirp.Repositories;
using Chirp.Infrastructure.DataModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<ICheepService, CheepService>();
builder.Services.AddScoped<ICheepRepository, CheepRepository>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ChirpDBContext>(options => options.UseSqlite(connectionString));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
        options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ChirpDBContext>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = "GitHub";
    })
    .AddCookie()
    .AddGitHub(o =>
    {
        o.ClientId = builder.Configuration["authentication:github:clientid"];
        o.ClientSecret = builder.Configuration["authentication:github:clientsecret"];
        o.CallbackPath = "/signin-github";
    });

var app = builder.Build();
app.UseDeveloperExceptionPage();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Migrate Database and Seed
using (var scope = app.Services.CreateScope())
{
    using var context = scope.ServiceProvider.GetRequiredService<ChirpDBContext>();
    context.Database.Migrate();
    DbInitializer.SeedDatabase(context);
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.UseRouting();

app.MapRazorPages();

app.Run();

namespace Chirp.Razor
{
}