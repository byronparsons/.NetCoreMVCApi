using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData
                (
                    new Company
                    {
                        Id = new Guid("C80F00EE-7E3B-4244-96B8-1E0E17397866"),
                        Name = "Byron Home Learning",
                        Address = "6 Dunstans Road, London, SE22 0HQ",
                        Country = "UK"
                    },
                    new Company
                    {
                        Id = new Guid("051E8C69-3D56-4172-B03D-A7D9A37DC330"),
                        Name = "Dunstans Cooking CC",
                        Address = "66 Dunstans Roadd, London, SE22 0HQ",
                        Country = "UK"
                    }

                );

        }
    }
}
