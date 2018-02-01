using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuroTrim.api.Entities
{
    public class EuroTrimContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        //public DbSet<Category> Categories { get; set; }
        
        public EuroTrimContext(DbContextOptions<EuroTrimContext> options)
           : base(options)
        {
            //Database.EnsureCreated();
            Database.Migrate();
        }

    }
}
