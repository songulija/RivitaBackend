using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RivitaBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RivitaBackend.Configurations.Entities
{
    public class CompaniesConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData(
                new Company
                {
                    Id = 1,
                    Name = "Rivita"
                },
                new Company
                {
                    Id = 2,
                    Name = "Linas Agro"
                },
                new Company
                {
                    Id = 3,
                    Name = "Mestilla"
                }
            );
        }
    }
}
