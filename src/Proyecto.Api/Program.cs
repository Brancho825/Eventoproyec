using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Proyecto.Application.Interfaces;
using Proyecto.Application.Mappings;
using Proyecto.Application.Services;
using Proyecto.Infrastructure.ExternalServices.Geolocation;
using Proyecto.Infrastructure.ExternalServices.Notification;
using Proyecto.Infrastructure.ExternalServices.Payments;
using Proyecto.Infrastructure.ExternalServices.Weather;
using Proyecto.Infrastructure.Persistence.Context;
using Proyecto.Infrastructure.Repositories;
using Proyecto.Infrastructure.Security;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ====================================================================
// 1. CONFIGURACIÓN DE LA BASE DE DATOS (MySQL)
// ====================================================================

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 32)); 

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(connectionString, serverVersion,
        mysqlOptions =>
        {
            mysqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
        });
});

// ====================================================================
// 2. CONFIGURACIÓN DE LA ARQUITECTURA (Inyección de Dependencias)
// ====================================================================

builder.Services.AddAutoMapper(typeof(MapeoPerfil).Assembly);

// --- 2.1. Repositorios (Data Access) ---
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();

// --- 2.2. Servicios de Aplicación (Business Logic / Use Cases) ---
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<IReporteService, ReporteService>();

// --- 2.3. Servicios de Infraestructura (Seguridad y Hash) ---
builder.Services.AddSingleton<IHashService, HashService>();
builder.Services.AddScoped<IJwtService, JwtService>();

// --- 2.4. Servicios de Infraestructura (Integraciones Externas - RF04) ---
builder.Services.AddScoped<IGeolocalizacionService, GoogleMapsService>();
builder.Services.AddScoped<IPaymentGatewayService, StripePaymentService>();
builder.Services.AddScoped<IExternalNotificationService, SendGridNotificationService>();
builder.Services.AddScoped<IWeatherService, OpenWeatherService>();


// ====================================================================
// 3. CONFIGURACIÓN DE SEGURIDAD JWT (RNF01)
// ====================================================================

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JwtSettings:SecretKey no configurada.");
var key = Encoding.ASCII.GetBytes(secretKey);

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
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false, 
        ClockSkew = TimeSpan.Zero 
    };
});

// ====================================================================
// 4. CONFIGURACIÓN DE LA API Y MIDDLEWARE
// ====================================================================

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// RNF04: Documentación con OpenAPI 3.0 (CONFIGURACIÓN DE SEGURIDAD AÑADIDA)
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new() { Title = "Sistema de Gestión de Eventos y Reservas (v1)", Version = "v1" });

    // -----------------------------------------------------------
    // SOLUCIÓN PARA EL CANDADO: AGREGAR EL ESQUEMA DE SEGURIDAD
    // -----------------------------------------------------------
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Ingrese el token JWT con el formato: Bearer {su token}",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            },
            new List<string>()
        }
    });
    // -----------------------------------------------------------
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Eventos y Reservas V1");
    });
    // ----------------------------------------------------------------------
    // ¡SOLUCIÓN FINAL DE REDIRECCIÓN!
    // Si la solicitud llega a la raíz ('/'), redirigirla a '/swagger'.
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = string.Empty; // Hace que Swagger esté disponible en la raíz (ej: http://localhost:5000/)
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Eventos y Reservas V1");
    });
    // ----------------------------------------------------------------------
}

// MIDDLEWARE DE AUTENTICACIÓN Y AUTORIZACIÓN (DEBEN IR EN ESTE ORDEN)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();