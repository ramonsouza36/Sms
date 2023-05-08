using System;
using System.Threading.Tasks;
using SchoolManagerSystem.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

public static class DatabaseUtility
{
    /// <summary>
    /// Method to see the database. Should not be used in production: demo purposes only.
    /// </summary>
    /// <param name="options">The configured options.</param>
    /// <param name="count">The number of contacts to seed.</param>
    /// <returns>The <see cref="Task"/>.</returns>
    public static async Task EnsureDbCreatedAndSeedWithCountOfAsync(DbContextOptions<ApplicationDbContext> options, int count)
    {
        // empty to avoid logging while inserting (otherwise will flood console)
        var factory = new LoggerFactory();
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>(options)
            .UseLoggerFactory(factory);

        using var context = new ApplicationDbContext(builder.Options);
        // result is true if the database had to be created
        if (await context.Database.EnsureCreatedAsync())
        {
            Debug.WriteLine($"context created.");
        }
    }
}