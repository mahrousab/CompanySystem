using CompanySystem.Application.Interfaces;
using CompanySystem.Domains.Helper;
using CompanySystem.Domains.Models;
using CompanySystem.Infrastructure.Data;
using CompanySystem.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.Infrastructure.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(CompanySystemDbContext employeecontext) : base(employeecontext)
        {

        }

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee)
            => Delete(employee);

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges)
            => await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges)
        {
            var employees = await FindByCondition(e => e.CompanyId.Equals(companyId),
 trackChanges)
  .FilterEmployees(employeeParameters.MinAge, employeeParameters.MaxAge)
  .Search(employeeParameters.SearchTerm)
  .OrderBy(e => e.Name)
  .ToListAsync();
            return PagedList<Employee>
            .ToPagedList(employees, employeeParameters.PageNumber,
           employeeParameters.PageSize);

        }
    }
}