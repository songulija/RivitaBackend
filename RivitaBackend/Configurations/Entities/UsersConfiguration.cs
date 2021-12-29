using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RivitaBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RivitaBackend.Configurations.Entities
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var id = Guid.Parse("c9490c27-1b89-4e39-8f2e-99b48dcc709e");
            var id2 = Guid.Parse("b9490c27-1b89-4e39-8f2e-99b48dcc901d");
            builder.HasData(
                new User
                {
                    Id = id,
                    Username = "jevgenijrivita",
                    PhoneNumber = "+37061816214",
                    Password = BCrypt.Net.BCrypt.HashPassword("Kazahas@123"),
                    CompanyId = 1,
                    TypeId = 1
                },
                new User
                {
                    Id = id2,
                    Username = "lukasrivita",
                    PhoneNumber = "+37060855183",
                    Password = BCrypt.Net.BCrypt.HashPassword("Password@12"),
                    CompanyId = 1,
                    TypeId = 1
                }
            );
        }
    }
}
