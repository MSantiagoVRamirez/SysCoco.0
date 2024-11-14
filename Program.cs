using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SysCoco._0.Models;
using SysCoco._0.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllersWithViews();

// Configurar el contexto de base de datos
builder.Services.AddDbContext<syscocoContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar IHttpContextAccessor para acceder al contexto HTTP
builder.Services.AddHttpContextAccessor();

// Configurar autenticaci�n basada en cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/api/Login/IniciarSesion";      // Ruta para el inicio de sesi�n
            options.LogoutPath = "/api/Login/CerrarSesion";      // Ruta para cerrar sesi�n
            options.AccessDeniedPath = "/AccesoDenegado";        // Ruta en caso de acceso denegado
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

var app = builder.Build();

// Configuraci�n del pipeline de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Habilitar sesiones
app.UseSession();

// Habilitar autenticaci�n y autorizaci�n
app.UseAuthentication();
app.UseAuthorization();

// Cambia el controlador y acci�n predeterminados a Login y IniciarSesion
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=IniciarSesion}/{id?}");

app.Run();
