using Api_GestionDeTurnos.Application.DTOs;
using Api_GestionDeTurnos.Application.Interfaces;
using Api_GestionDeTurnos.Application.Services;
using Api_GestionDeTurnos.Application.Validators;
using Api_GestionDeTurnos.Domain.Interfaces;
using Api_GestionDeTurnos.Infrastructure.Authtentication;
using Api_GestionDeTurnos.Infrastructure.Persistence;
using Api_GestionDeTurnos.Middlewares;
using Api_Turnos.Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GestionDeTurnosDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
// Bind y registro de JwtSettings (usar la misma sección en appsettings.json)
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>()
    ?? throw new InvalidOperationException("Sección 'Jwt' ausente en configuración");
builder.Services.AddSingleton(jwtSettings);
// JWT Authentication (compartido)
var keyBytes = Encoding.UTF8.GetBytes(jwtSettings.Secret ?? throw new InvalidOperationException("Jwt: Secret es null"));
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
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
        };
    });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
// agregar servicios personalizados
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ITokenService, JwtTokenService>();
//contratos
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ITurnoService, TurnoService>();
//Implementaciones
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ITurnoRepository, TurnoRepository>();

builder.Services.AddScoped<IValidator<UsuarioDTO>, CrearUsuarioValidator>();
builder.Services.AddScoped<IValidator<LoginDTO>, LoginValidator>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors();
//Manejo de excepciones globales
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
