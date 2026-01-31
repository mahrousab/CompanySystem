using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.Infrastructure.Data
{
    public class CompanySystemDbContextFactory : IDesignTimeDbContextFactory<CompanySystemDbContext>
    {
        public CompanySystemDbContext CreateDbContext(string[] args) {

            DbContextOptionsBuilder<CompanySystemDbContext> optionsBuilder = new DbContextOptionsBuilder<CompanySystemDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CompanySystem;Trusted_Connection=True;TrustServerCertificate=True");
            return new CompanySystemDbContext(optionsBuilder.Options);
        }
    }
}
