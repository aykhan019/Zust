using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Zust.Business.Abstract;
using Zust.Business.Concrete;
using Zust.Core.Concrete.EntityFramework;
using Zust.DataAccess.Abstract;
using Zust.DataAccess.Concrete;
using Zust.DataAccess.Concrete.EFEntityFramework;
using Zust.Entities.Models;
using Zust.Web.Helpers.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the database context
var connectionString = builder.Configuration.GetConnectionString(Constants.ConnectionStringName);
builder.Services.AddDbContext<ZustDbContext>(opt =>
{
    opt.UseSqlServer(connectionString, b => b.MigrationsAssembly(Constants.MigrationsAssembly));
});

// Dependency injection configuration
builder.Services.AddScoped<IUserDal, EFUserDal>();
builder.Services.AddScoped<IUserService, UserService>();

// Register Session
builder.Services.AddSession();

// Register Identity
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<ZustDbContext>()
    .AddSignInManager<SignInManager<User>>()
    .AddDefaultTokenProviders();


// Register AutoMapper 
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();

// Configure the expiration time span for the authentication cookie
builder.Services.Configure<CookieAuthenticationOptions>(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromDays(Constants.CookieExpireTimeSpan); 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Use Session
app.UseSession();

// Other middleware
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Configure default route
app.MapControllerRoute(
    name: "default",    
    pattern: "{controller=Account}/{action=Login}");

// Uncomment the following line if you want to redirect the root URL to a specific route
app.UseRewriter(new RewriteOptions().AddRedirect("^$", "/home/index"));

app.Run();