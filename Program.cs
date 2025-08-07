using Crud.Models;
using Crud.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoCrudApp.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ------------------ MongoDB Config ------------------
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDB"));

// ------------------ JWT Config ------------------
var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettingsSection);

// Bind JwtSettings values to object
var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
builder.Services.AddSingleton(jwtSettings);

// ------------------ Service Registrations ------------------
builder.Services.AddSingleton<StudentService>();
builder.Services.AddScoped<JwtService>();

// ------------------ JWT Authentication ------------------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };
    });

// ------------------ MVC & API Support ------------------
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ------------------ Middleware Pipeline ------------------
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // Enable JWT authentication
app.UseAuthorization();  // Enable role/claims authorization

// ------------------ Endpoints ------------------
app.MapControllers(); // API Controllers

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
