using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnionEmailApp.Application.Interfaces;
using OnionEmailApp.Application.Services;
using OnionEmailApp.Domain.Repositories;
using OnionEmailApp.Infrastructure.Data;
using OnionEmailApp.Infrastructure.Repositories;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Configure DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Dependencies
builder.Services.AddScoped<IUserRepository, UserRepository>();









//var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())  // Ensure it reads from the project root
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);  // Force loading
// Register SMTP settings
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();


var app = builder.Build();
app.UseRouting();
app.UseEndpoints(endpoints =>
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Email}/{action=Compose}/{id?}"));
app.UseAuthorization();
app.MapControllers();
app.Run();
