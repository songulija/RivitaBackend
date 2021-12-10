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
            builder.HasData(
                new User
                {
                    Id = id,
                    Username = "jevgenijrivita",
                    PhoneNumber = "+37061816214",
                    Password = BCrypt.Net.BCrypt.HashPassword("Kazahas@123"),
                    CompanyId = 1,
                    TypeId = 1
                }
            );
        }
    }
}
