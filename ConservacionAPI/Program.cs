using Conservacion.AccesoDatos;
using Conservacion_LogicaNegocio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
var cadenaConexion = builder.Configuration
    .GetConnectionString("ConservacionDB")
    ?? throw new InvalidOperationException(
        "No se encontró la cadena de conexión.");

builder.Services.AddScoped(
    _ => new Conexion(cadenaConexion));

builder.Services.AddScoped<AreaConservacionAD>();
builder.Services.AddScoped<AreaConservacionLN>();

builder.Services.AddScoped<EspecieAD>();
builder.Services.AddScoped<EspecieLN>();

builder.Services.AddScoped<PersonalAD>();
builder.Services.AddScoped<PersonalLN>();

builder.Services.AddScoped<RegistroEspecieAD>();
builder.Services.AddScoped<RegistroEspecieLN>();

builder.Services.AddScoped<ActividadConservacionAD>();
builder.Services.AddScoped<ActividadConservacionLN>();

builder.Services.AddScoped<MonitoreoAmbientalAD>();
builder.Services.AddScoped<MonitoreoAmbientalLN>();

builder.Services.AddScoped<AmenazaAD>();
builder.Services.AddScoped<AmenazaLN>();


builder.Services.AddScoped<UsuarioAD>();
builder.Services.AddScoped<AutenticacionLN>();





// Agrega srvicios al contanedor.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();



var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException(
        "No se configuró la clave JWT.");

var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtIssuer,
                ValidAudience = jwtAudience,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtKey)),

                ClockSkew = TimeSpan.Zero
            };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

//Prueba de conexión a la base de datos

app.MapGet("/api/prueba-conexion",
    async (Conexion conexion) =>
    {
        try
        {
            using var cn = conexion.ObtenerConexion();
            await cn.OpenAsync();

            return Results.Ok(new
            {
                mensaje = "Conexión exitosa",
                baseDatos = cn.Database
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                "Error de conexión: " + ex.ToString());

            return Results.Problem(
                "No se pudo conectar a SQL Server. " +
                "Revisar la consola de Visual Studio.");
        }
    });


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
