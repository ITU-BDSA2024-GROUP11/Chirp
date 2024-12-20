using Chirp.Core.CheepServiceInterface;
using Chirp.Core.RepositoryInterfaces;
using Chirp.Infrastructure;
using Chirp.Infrastructure.Chirp.Repositories;
using Chirp.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<ICheepService, CheepService>();
builder.Services.AddScoped<ICheepRepository, CheepRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ChirpDBContext>(options => options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<Author>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = false;
    })
    .AddEntityFrameworkStores<ChirpDBContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
});

var clientId = builder.Configuration["authentication_github_clientid"]
    ?? throw new InvalidOperationException("GitHub ClientId is not configured.");
var clientSecret = builder.Configuration["authentication_github_clientsecret"]
    ?? throw new InvalidOperationException("GitHub ClientSecret is not configured.");

var adminPassword = builder.Configuration["ADMIN_PASSWORD"]
                   ?? "Admin123!";

builder.Services.AddAuthentication()
    .AddGitHub(o =>
    {
        o.ClientId = clientId;
        o.ClientSecret = clientSecret;
        o.Scope.Add("user:email");
    });

var app = builder.Build();
app.UseDeveloperExceptionPage();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Migrate Database and Seed
using (var scope = app.Services.CreateScope())
{
    using var context = scope.ServiceProvider.GetRequiredService<ChirpDBContext>();
    context.Database.Migrate();
    DbInitializer.SeedDatabase(context, scope.ServiceProvider, adminPassword);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

namespace Chirp.Razor
{
}