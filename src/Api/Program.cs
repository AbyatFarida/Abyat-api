using Abyat.Api.Middlewares;
using Abyat.Api.Services.General;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace Abyat.Api;

public class Program
{
    private static async Task Main(string[] args)
    {
        WebApplicationBuilder? builder = WebApplication.CreateBuilder(args).AddApplicationServices();

        #region Cors

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend",
        builder =>
        {
            builder
                .WithOrigins("https://elite-client-hazel.vercel.app", "https://adminelite.netlify.app", "http://localhost:5173")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });

            options.AddPolicy("AllowAll",
    builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
        });

        #endregion

        #region JsonApiFormat

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        });

        #endregion

        builder.Services.AddEndpointsApiExplorer();

        #region Swagger

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "EliteUK API",
                Description = "API to access EliteUK",
            });

            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

            options.UseAllOfToExtendReferenceSchemas();
        });

        #endregion

        #region JWT

        string jwtSecretKey = builder.Configuration.GetValue<string>("Jwt:Key")
         ?? throw new InvalidOperationException("JWT secret key is missing.");

        string jwtIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer")
            ?? throw new InvalidOperationException("JWT issuer is missing.");

        string jwtAudience = builder.Configuration.GetValue<string>("Jwt:Audience")
            ?? throw new InvalidOperationException("JWT audience is missing.");

        byte[] key = Encoding.ASCII.GetBytes(jwtSecretKey);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtIssuer,
                ValidAudience = jwtAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),

                RoleClaimType = ClaimTypes.Role,
                NameClaimType = ClaimTypes.Name
            };
        });

        #endregion

        WebApplication? app = builder.Build();
        await app.UseApplicationStartup();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors("AllowFrontend");

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseMiddleware<ErrorHandlingMiddleware>();

        app.MapControllers();

        app.Run();
    }
}