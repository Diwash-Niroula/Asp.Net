using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using BookExchange.Context;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Set up the database context
builder.Services.AddDbContext<ApplicationDbcontext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Set up authentication with Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbcontext>()
    .AddDefaultTokenProviders();

// Configure authentication cookie (if you're using Identity)
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

var app = builder.Build();

// Middleware setup
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Authentication & Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Map routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Books}/{action=Index}/{id?}"
);

app.Run();
