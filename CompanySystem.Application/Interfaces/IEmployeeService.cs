using CompanySystem.Application.DTOS;
using CompanySystem.Domains.Helper;
using CompanySystem.Domains.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace CompanySystem.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<(IEnumerable<ExpandoObject> employees, MetaData metaData)> GetEmployeesAsync(Guid
   companyId, EmployeeParameters employeeParameters, bool trackChanges);

        Task< EmployeeDto> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges);

       Task< EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto
employeeForCreation, bool trackChanges);

        Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid id, bool trackChanges);
        Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id,
EmployeeForUpdateDto employeeForUpdate, bool compTrackChanges, bool
empTrackChanges);
       Task< (EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(
Guid companyId, Guid id, bool compTrackChanges, bool empTrackChanges);
        Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee
        employeeEntity);
    }
}
