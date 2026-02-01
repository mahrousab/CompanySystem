using CompanySystem.Domains.Models;
using CompanySystem.Infrastructure.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.Infrastructure.Data
{
    public class CompanySystemDbContext : IdentityDbContext<User>
    {
        public CompanySystemDbContext(DbContextOptions<CompanySystemDbContext> options): base (options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            // add by using assembly scanning
            //  modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompanySystemDbContext).Assembly);
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }

        public DbSet<Company> companies { get; set; }
        public DbSet<Employee> employees { get; set; }
    }
}
