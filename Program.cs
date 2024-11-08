using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SysCoco._0.Models;
using SysCoco._0.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configurar el contexto de base de datos
builder.Services.AddDbContext<syscocoContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar IHttpContextAccessor para acceder al contexto HTTP
builder.Services.AddHttpContextAccessor();

// Configurar autenticación basada en cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/api/Login/IniciarSesion"; // Ruta para el inicio de sesión
            options.LogoutPath = "/api/Login/CerrarSesion"; // Ruta para cerrar sesión
            options.AccessDeniedPath = "/AccesoDenegado"; // Ruta en caso de acceso denegado
        });

// Registrar el servicio de usuarios
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// Agregar Distributed Memory Cache para las sesiones
builder.Services.AddDistributedMemoryCache();

// Configurar sesiones
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(50);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


// Construir la aplicación
var app = builder.Build();  // Esta es la única llamada a Build()

app.UseHttpsRedirection();

// Sirve archivos estáticos como CSS, imágenes, etc. (si aplicable)
app.UseStaticFiles();

// Habilitar sesiones
app.UseSession();

// Habilitar autenticación y autorización
app.UseAuthentication(); // Primero la autenticación
app.UseAuthorization();  // Luego la autorización

// Habilitar el enrutamiento
app.MapControllers();

// Ejecutar la aplicación
app.Run();