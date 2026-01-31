using CompanySystem.Domains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.Infrastructure.SeedData
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData
            (
            new Company
            {
                Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                Name = "IT_Solutions Ltd",
                Address = "583 Wall Dr. Gwynn Oak, MD 21207",
                Country = "USA"
            },
            new Company
            {
                Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                Name = "Admin_Solutions Ltd",
                Address = "312 Forest Avenue, BF 923",
                Country = "USA"
            },new Company
            {
                Id = new Guid("{6D76ECE9-C72E-475F-B72B-D3266240F182}"),
                Name = "Vodafon-Egypt",
                Address = "Shipin-Elqanter-Qalueb"
               , Country= "Egypt"
            },
            new Company
            {
                Id= new Guid("{4D43715F-1691-4AA8-9A82-AAC0389C7ABF}"),
                Name = "Orange-Egypt",
                Address = "medan-eltahrer"
                , Country ="Egy"
            }
            );
        }
    }
}
