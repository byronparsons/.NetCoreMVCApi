using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData
            (
               new Employee
               {
                   Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                   Name = "Joanna Parsons",
                   Age = 26,
                   Position = "Software developer",
                   CompanyId = new Guid("C80F00EE-7E3B-4244-96B8-1E0E17397866")
               },
                 new Employee
                 {
                     Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                     Name = "Byron Parsons",
                     Age = 30,
                     Position = "Software developer",
                     CompanyId = new Guid("C80F00EE-7E3B-4244-96B8-1E0E17397866")
                 },
                 new Employee
                 {
                     Id = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                     Name = "Kane Miller",
                     Age = 35,
                     Position = "Administrator",
                     CompanyId = new Guid("051E8C69-3D56-4172-B03D-A7D9A37DC330")
                 }
             );
         }
    }
}
