using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using DAL.Data;
using DAL.Persistence.Seeding;
using BLL.Services; // Add reference to BLL project
using Microsoft.Extensions.Configuration;
using DALApplicationDbContext = DAL.Data.ApplicationDbContext;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using System.Configuration;
using BLL.Interfaces;
using DAL.Persistence.Repositories;
using Microsoft.Extensions.Options;
using BLL.Services.BLL.Services;



var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Configure PayPal settings
builder.Services.Configure<PayPalSettings>(configuration.GetSection("PayPal"));


builder.Services.AddAuthentication().AddGoogle(options =>
{
    options.ClientId = configuration["Authentication:Google:ClientId"];
    options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
});

builder.Services.AddAuthentication().AddMicrosoftAccount(options =>
{
options.ClientId = configuration["Authentication:Microsoft:ClientId"];
options.ClientSecret = configuration["Authentication:Microsoft:ClientSecret"];
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<DALApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configure Identity with roles and email confirmation
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true; // Require email confirmation for sign in
})
.AddRoles<IdentityRole>() // Add role support
.AddEntityFrameworkStores<DAL.Data.ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = true; // Ensure email confirmation is required
});

// Register EmailSender service with credentials from configuration
builder.Services.AddTransient<IEmailSender, EmailSender>(serviceProvider =>
{
    var config = serviceProvider.GetRequiredService<IConfiguration>();
    var email = config["EmailSettings:Email"]; // Fetch from config
    var password = config["EmailSettings:Password"]; // Fetch from config
    var emailSender = new EmailSender(email, password); // Pass credentials to EmailSender constructor
    return emailSender;
});

// Register PayPalService
builder.Services.AddScoped<PayPalService>();


builder.Services.AddScoped<ICourseModuleService, CourseModuleService>();


builder.Services.AddScoped<ICourseService, CourseService>();


builder.Services.AddScoped<ILessonFileService, LessonFileService>();

builder.Services.AddScoped<ILessonFileRepository, LessonFileRepository>();

builder.Services.AddScoped<ICourseEnrollmentService, CourseEnrollmentService>();

builder.Services.AddScoped<ICourseEnrollmentRepository, CourseEnrollmentRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
//  Register the Assignment Submission service

builder.Services.AddScoped<IAssignmentSubmissionService, AssignmentSubmissionService>();
// the assignment service
builder.Services.AddScoped<IAssignmentService, AssignmentService>();

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IGoogleSheetsRepository, GoogleSheetsRepository>();
builder.Services.AddScoped<GoogleSheetsService>();
var googleCredentialsPath = builder.Configuration["GoogleApi:CredentialsPath"];

var app = builder.Build();

// Seed roles here before the app runs
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        // Seed roles using RoleSeeder
        await RoleSeeder.SeedRoles(services);
    }
    catch (Exception ex)
    {
        // Log or handle any errors during role seeding
        Console.WriteLine($"Error occurred seeding roles: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
