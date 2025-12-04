using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Abyat.Da.Context;

public class AbyatDbContextFactory : IDesignTimeDbContextFactory<AbyatDbContext>
{
    public AbyatDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: false)
            .Build();

        DbContextOptionsBuilder<AbyatDbContext>? optionsBuilder = new DbContextOptionsBuilder<AbyatDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        return new AbyatDbContext(optionsBuilder.Options);
    }
}
