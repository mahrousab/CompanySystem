using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.Infrastructure.SeedData
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData
            (
                new IdentityRole
                {
                   
                    Id = "c3f93bcd-02bb-414b-892e-b2bbdab8cda1",
                    Name = "Manager",
                    NormalizedName = "MANAGER",
                   
                    ConcurrencyStamp = "c39f1e6c-7b19-4b1c-9e9a-995bb97b6ed5"
                },
                new IdentityRole
                {
                    Id = "88aafa7a-598c-4d09-b7ec-4ef48fd0a350",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    ConcurrencyStamp = "ec69ae60-f6c4-448c-832e-61d43c176b24"
                }
            );
        }
    }
}