using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TalentoPlus.Application;
using TalentoPlus.Application.Interfaces;
using TalentoPlus.Application.Services;
using TalentoPlus.Infrastructure;
using TalentoPlus.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// ===== LOGGING PARA DEBUG =====
Console.WriteLine($"🔍 Ambiente: {builder.Environment.EnvironmentName}");
Console.WriteLine($"🔍 ConnectionString: {!string.IsNullOrEmpty(builder.Configuration.GetConnectionString("DefaultConnection"))}");

// ===== CONFIGURAR NPGSQL =====
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// ===== SERVICIOS =====
builder.Services.AddRazorPages();

// Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity Configuration
builder.Services.AddDefaultIdentity<IdentityUser>(options => 
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// Application and Infrastructure Layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Application Services
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IExcelImportService, ExcelImportService>();
builder.Services.AddScoped<IPdfService, PdfService>();

var app = builder.Build();

// ===== PIPELINE =====
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

// ===== INICIALIZACIÓN DE BASE DE DATOS =====
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    
    try
    {
        var dbContext = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        
        // 1. Verificar conexión a DB
        var canConnect = await dbContext.Database.CanConnectAsync();
        Console.WriteLine($"✅ Database connection: {(canConnect ? "SUCCESS" : "FAILED")}");
        
        // 2. Aplicar migraciones automáticamente
        await dbContext.Database.MigrateAsync();
        Console.WriteLine("✅ Migraciones aplicadas");
        
        // 3. Crear usuario admin si no existe
        var adminEmail = "admin@talentoplus.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "Administrador TalentoPlus",
                EmailConfirmed = true
            };
            
            var result = await userManager.CreateAsync(adminUser, "Admin123!");
            if (result.Succeeded)
            {
                Console.WriteLine($"✅ Usuario admin creado: {adminEmail} / Admin123!");
            }
            else
            {
                Console.WriteLine($"❌ Error creando admin: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
        else
        {
            Console.WriteLine($"✅ Usuario admin ya existe");
        }
        
        // 4. Verificar datos de empleados
        var employeesExist = await dbContext.Employees.AnyAsync();
        Console.WriteLine($"✅ Employees table: {(employeesExist ? "CON DATOS" : "VACÍA")}");
        
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error en inicialización: {ex.Message}");
    }
}

app.Run();