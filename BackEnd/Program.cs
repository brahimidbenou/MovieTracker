using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using BackEnd.Models;
using BackEnd.Services;

namespace BackEnd;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<UserContext>();
        builder.Services.AddDbContext<FilmContext>();
        builder.Services.AddDbContext<FavorisContext>();
        builder.Services.AddHttpClient<OmdbService>();
        builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
        builder.Services.AddScoped<JwtService>();

        
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "TrackerDeFavorisApi", Version = "v1" });

            // Ajouter un schéma d'authentification JWT dans Swagger
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Veuillez entrer le token jwt en commencat avec 'Bearer '",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        var secretKey = builder.Configuration["Jwt:SecretKey"] 
            ?? throw new InvalidOperationException("SecretKey is missing from configuration.");

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.FromMinutes(10),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)), // Utilisation correcte
                    RoleClaimType = ClaimTypes.Role
                };
            });

        builder.Services.AddAuthorization();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.ConfigObject.AdditionalItems.Add("persistAuthorization","true");
            });
        }

        app.UseAuthorization();
        app.UseAuthentication();

        app.MapControllers();

        app.Run();
    }
}
