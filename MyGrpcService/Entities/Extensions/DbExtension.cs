using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.Infrastructure.Extensions;

public static class DbExtension
{
    public static IHost MigrateDatabase<TContext>(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var config = services.GetRequiredService<IConfiguration>();
            var logger = services.GetRequiredService<ILogger<TContext>>();
            try
            {
                logger.LogInformation("Discount DB Migration Started");
                ApplyMigrations(config);
                logger.LogInformation("Discount DB Migration Completed");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        return host;
    }

    private static void ApplyMigrations(IConfiguration config)
    {
        using var connection = new NpgsqlConnection(config.GetValue<string>("DatabaseSettings:ConnectionString"));
        connection.Open();
        using var cmd = new NpgsqlCommand()
        {
            Connection = connection
        };
        cmd.CommandText = "DROP TABLE IF EXISTS Coupon";
        cmd.ExecuteNonQuery();
        cmd.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                ProductName VARCHAR(500) NOT NULL,
                                                Description TEXT,
                                                Amount INT)";
        cmd.ExecuteNonQuery();
        
        cmd.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Lessons from History', 'Book Discount', 59);";
        cmd.ExecuteNonQuery();

        cmd.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Over the clouds ', 'New offer !!!', 37);";
        cmd.ExecuteNonQuery();
    }
}