using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TalentoPlus.Application;
using TalentoPlus.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Logging para debug
Console.WriteLine($"🔍 API - Ambiente: {builder.Environment.EnvironmentName}");
Console.WriteLine($"🔍 API - ConnectionString configurada: {!string.IsNullOrEmpty(builder.Configuration.GetConnectionString("DefaultConnection"))}");

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar PostgreSQL
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Add Application and Infrastructure layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// JWT Authentication (versión SIMPLIFICADA para demo)
var jwtKey = builder.Configuration["Jwt:Key"] ?? "supersecretkey12345supersecretkey12345";
var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Test database connection
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        var canConnect = await dbContext.Database.CanConnectAsync();
        Console.WriteLine($"✅ API Database connection: {(canConnect ? "SUCCESS" : "FAILED")}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ API Database error: {ex.Message}");
    }
}

app.Run();