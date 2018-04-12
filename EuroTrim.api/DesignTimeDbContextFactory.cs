using EuroTrim.api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EuroTrim.api
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EuroTrimContext>
    {
        public EuroTrimContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<EuroTrimContext>();
            var connectionString = configuration.GetConnectionString("euroTrimDBConnectionString");
            builder.UseSqlServer(connectionString);
            return new EuroTrimContext(builder.Options);
        }
    }
}
