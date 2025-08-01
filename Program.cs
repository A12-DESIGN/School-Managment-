using Crud.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoCrudApp.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// MongoDB config
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<StudentService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "your-app",
            ValidAudience = "your-app",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("your-secret-key"))
        };
    });




// MVC & API support                                                                                                            
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseAuthentication();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllers(); // ← This is important for API routes like /api/studentapi

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
