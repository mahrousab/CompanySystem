using CompanySystem.Domains.Models;
using CompanySystem.Infrastructure.SeedData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.Infrastructure.Data
{
    public class CompanySystemDbContext : DbContext
    {
        public CompanySystemDbContext(DbContextOptions<CompanySystemDbContext> options): base (options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        }

        public DbSet<Company> companies { get; set; }
        public DbSet<Employee> employees { get; set; }
    }
}
