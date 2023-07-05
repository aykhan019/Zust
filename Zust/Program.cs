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

// Register database
var connectionString = builder.Configuration.GetConnectionString(Constants.ConnectionStringName);
builder.Services.AddDbContext<ZustDbContext>(opt =>
{
    opt.UseSqlServer(connectionString, b => b.MigrationsAssembly(Constants.MigrationsAssembly));
});

// Register Interfaces
builder.Services.AddScoped<IUserDal, EFUserDal>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();


// Register Session
builder.Services.AddSession();

// Register Identity
builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ZustDbContext>()
                .AddSignInManager<SignInManager<User>>()
                .AddDefaultTokenProviders();


// Register Token
var section = builder.Configuration.GetSection(TokenConstants.TokenSection).Value;
var key = Encoding.ASCII.GetBytes(section);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
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

// Other things to use
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

// For redirecting to https://www._____.com/home
app.UseRewriter(new RewriteOptions().AddRedirect("^$", "/account/login"));
app.Run();
