using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFDataAccess
{
    public class CustomerDbContextFactory : IDesignTimeDbContextFactory<CustomerDbContext>
    {
        public CustomerDbContext CreateDbContext(string[] args)
        {
            // IConfigurationRoot configuration = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json")
            //.Build();

            var builder = new DbContextOptionsBuilder<CustomerDbContext>();
            //var connectionString = configuration.GetConnectionString("DefaultConnection");
            var connectionString = "Server=SANGEETA-PC\\SQLEXPRESS;Database=CustomerDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            builder.UseSqlServer(connectionString);
            //builder.UseNpgsql(connectionString);

            return new CustomerDbContext(builder.Options);
        }
    }
}
