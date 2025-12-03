using Abyat.Api.Middlewares;
using Abyat.Bl.Contracts.Auth;
using Abyat.Bl.Contracts.Senders;
using Abyat.Bl.Contracts.User;
using Abyat.Bl.Mapping;
using Abyat.Bl.Services.Senders;
using Abyat.Bl.Services.User;
using Abyat.Bl.Settings;
using Abyat.Da.Context;
using Abyat.Da.Identity;
using Abyat.Da.Repos.Base;
using Abyat.Domains.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

namespace Abyat.Api.Services.General;

public static class RegisterHelper
{
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        #region ILogger

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();
        builder.Host.UseSerilog();

        #endregion

        builder.Services.AddAuthorization();
        builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

        string? connStr = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContextFactory<AbyatDbContext>(options =>
        {
            options.UseSqlServer(connStr);
        });

        #region Identity

        builder.Services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.SignIn.RequireConfirmedEmail = true;
            options.User.RequireUniqueEmail = true;
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            options.Lockout.AllowedForNewUsers = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireDigit = true;
            options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
        }).AddEntityFrameworkStores<AbyatDbContext>().AddDefaultTokenProviders();

        #endregion

        #region DI

        #region Middlewares

        builder.Services.AddTransient<ErrorHandlingMiddleware>();

        #endregion

        #region General

        builder.Services.AddScoped(typeof(ITableQryRepo<>), typeof(TableQryRepo<>));
        builder.Services.AddScoped(typeof(ITableCmdRepo<>), typeof(TableCmdRepo<>));
        builder.Services.AddScoped<IUserQry, UserQryService>();
        builder.Services.AddScoped<IUserCmd, UserCmdService>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IRefreshTokens, RefreshTokenService>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IEmailSender, EmailSender>();

        #endregion

        #region Bl

        builder.Services.Configure<AppSettings>(builder.Configuration);

        builder.Services.AddSingleton(resolver =>
            resolver.GetRequiredService<IOptions<AppSettings>>().Value);

        #endregion

        #endregion

        return builder;
    }

    public static async Task<WebApplication> UseApplicationStartup(this WebApplication app)
    {
        using IServiceScope? scope = app.Services.CreateScope();
        IServiceProvider? services = scope.ServiceProvider;

        AbyatDbContext? dbContext = services.GetRequiredService<AbyatDbContext>();
        UserManager<AppUser>? userManager = services.GetRequiredService<UserManager<AppUser>>();
        RoleManager<AppRole>? roleManager = services.GetRequiredService<RoleManager<AppRole>>();

        try
        {
            await dbContext.Database.MigrateAsync();

            IOptions<AppSettings>? emailOptions = services.GetRequiredService<IOptions<AppSettings>>();

            await ContextConfig.SeedDataAsync(dbContext, userManager, roleManager, emailOptions);
        }
        catch (Exception ex)
        {
            try
            {
                ILogger<Program>? logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Error during migration/seed.");
            }
            finally
            {
                Console.WriteLine($"[Startup] Migration failed: {ex}");
            }
        }

        return app;
    }


}