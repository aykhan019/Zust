using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Zust.Business.Abstract;
using Zust.Business.Concrete;
using Zust.Core.Concrete.EntityFramework;
using Zust.DataAccess.Abstract;
using Zust.DataAccess.Concrete.EFEntityFramework;
using Zust.Entities.Models;
using Zust.Web.Helpers.ConstantHelpers;
using Zust.Web.Hubs;

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
builder.Services.AddScoped<IFriendRequestDal, EFFriendRequestDal>();
builder.Services.AddScoped<IFriendRequestService, FriendRequestService>();
builder.Services.AddScoped<INotificationDal, EFNotificationDal>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IFriendshipDal,EFFriendshipDal>();
builder.Services.AddScoped<IFriendshipService, FriendshipService>();

// Register Session
builder.Services.AddSession();

// Register Identity
builder.Services.AddIdentity<User, Zust.Entities.Models.Role>()
                .AddEntityFrameworkStores<ZustDbContext>()
                .AddSignInManager<SignInManager<User>>()
                .AddDefaultTokenProviders();

// Register AutoMapper 
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();

// Register SignalR
builder.Services.AddSignalR();

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

// Configure routes
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name : "Default", pattern: "{controller=Home}/{action=Index}");
    endpoints.MapHub<UserHub>("/userhub");
});

// Uncomment the following line if you want to redirect the root URL to a specific route
app.UseRewriter(new RewriteOptions().AddRedirect("^$", "/home/index"));

app.Run();