using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using SandraConfecciones.Models;
using SandraConfecciones.Services.Interfaces;
using SandraConfecciones.Services.Implementation;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? builder.Configuration.GetConnectionString("connString");

builder.Services.AddDbContext<SandraContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// This way we'll be able to use this service in every controller
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.AccessDeniedPath = "/Home/Privacy";
    });

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("JefePolicy", policy => policy.RequireRole("Jefe"));
//    options.AddPolicy("EmpleadoPolicy", policy => policy.RequireRole("Empleado"));
//});

//builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
//                .AddEntityFrameworkStores<SandraContext>()
//                .AddDefaultTokenProviders()
//                .AddDefaultUI();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddRoles<IdentityRole>() // Añadir soporte para roles
//    .AddEntityFrameworkStores<SandraContext>(); // Configurar el almacenamiento de datos

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
//    options.AddPolicy("Manager", policy => policy.RequireRole("Manager"));
//    options.AddPolicy("Member", policy => policy.RequireRole("Member"));
//});

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(
        new ResponseCacheAttribute
        {
            NoStore = true,
            Location = ResponseCacheLocation.None,
        }
        );
});

RotativaConfiguration.Setup(builder.Environment.WebRootPath, "Rotativa");

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//    var roles = new[] { "Admin", "Manager", "Member" };

//    foreach (var role in roles)
//    {
//        if (!await roleManager.RoleExistsAsync(role))
//        {
//            await roleManager.CreateAsync(new IdentityRole(role));
//        }
//    }
//};

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
    pattern: "{controller=Auth}/{action=Login}/{id?}");


app.Run();
