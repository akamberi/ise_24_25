using BusPortal.BLL.Domain.Models;
using BusPortal.BLL.Services;
using BusPortal.BLL.Services.Interfaces;
using BusPortal.BLL.Services.Scoped;
using BusPortal.DAL.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using BusPortal.DAL.Persistence.Entities;
using BusPortal.BLL.Mapping;
using BusPortal.DAL.Persistence;
using BusPortal.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.RegisterBLLServices(builder.Configuration);
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeSettings"));


builder.Services.AddAuthentication()
    .AddCookie(options =>
    {
        options.LoginPath = "/Clients/Login";
        options.AccessDeniedPath = "/Shared/Error";
    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<EmailService>();


builder.Services.AddHttpContextAccessor();

//Add authentication configuration
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Clients/Login";
    options.LogoutPath = "/Clients/Logout";
    options.AccessDeniedPath = "/Clients/AccessDenied";
    options.Cookie.Name = "BusPortalAuth";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(24);
    options.SlidingExpiration = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); 
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
