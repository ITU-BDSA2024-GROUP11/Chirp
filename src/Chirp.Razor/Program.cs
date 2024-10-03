var builder = WebApplication.CreateBuilder(args);

// This sets the app to listen on port 8080 (you can replace with your port)
//builder.WebHost.UseUrls("http://localhost:8080");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<ICheepService, CheepService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();
