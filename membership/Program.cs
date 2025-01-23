using membership.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

//builder.Services.AddDbContext<MembershipDbContext>(options =>
//{
//    var connectionString = builder.Configuration.GetConnectionString("LocalConnection");
//    options.UseSqlServer(connectionString);
//});

builder.Services.AddDbContext<MembershipDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb"));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); // Set session timeout duration
    options.Cookie.HttpOnly = true; // Secure session cookie for HTTP requests only
    options.Cookie.IsEssential = true; // Make sure the cookie is essential for the app to function
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Homepage}/{action=Index}"

);

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MembershipDbContext>();
    SeedData.Initialize(context);
}

// Add services to the container.



app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
