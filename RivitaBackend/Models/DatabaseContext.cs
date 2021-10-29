using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RivitaBackend.Configurations.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RivitaBackend.Models
{
   
    public class DatabaseContext : IdentityDbContext<ApiUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Transportation> Transportations { get; set; }
        public DbSet<Wagon> Wagons { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // applying configurations. Seed data for IdentityRoles, Comapnies
            builder.ApplyConfiguration(new CompaniesConfiguration());
            builder.ApplyConfiguration(new RolesConfiguration());
        }
    }
}
