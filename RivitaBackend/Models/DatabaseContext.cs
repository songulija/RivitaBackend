using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApiUser> _userManager;
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

            //add companies first. then user
            // applying configurations. Seed data for IdentityRoles, Companies
            builder.ApplyConfiguration(new CompaniesConfiguration());

            string ADMIN_ID = "c9490c27-1b89-4e39-8f2e-99b48dcc709e";
            string ROLE_ID = "b75243f9-b3ba-4bb2-b1a7-7cfe4028f95e";

            //seed admin role
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    Id = ROLE_ID,
                    ConcurrencyStamp = ROLE_ID
                }, new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }
            );
            //create user
            var appUser = new ApiUser
            {
                Id = ADMIN_ID,
                Email = "rivitaadmin@gmail.com",
                EmailConfirmed = true,
                UserName = "rivitaadmin@gmail.com",
                CompanyId = 1,
                NormalizedUserName = "RIVITAADMIN@GMAIL.COM"
            };

            //set user password
            PasswordHasher<ApiUser> ph = new PasswordHasher<ApiUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, "P@ssword1");

            //seed user
            builder.Entity<ApiUser>().HasData(appUser);

            //set user role to admin
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });



            builder.Entity<Transportation>(entity => { entity.HasIndex(e => e.TransportationNumber).IsUnique(); });

            
       
        }
    }
}
