using CompanySystem.Application.DTOS;
using CompanySystem.Domains.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.Application.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(bool trackChanges);
        Task<CompanyDto> GetCompanyAsync(Guid companyId, bool trackChanges);
        Task<CompanyDto> CreateCompanyAsync(CompanyForCreationDto company);
        Task<IEnumerable<CompanyDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool
        trackChanges);
        Task<(IEnumerable<CompanyDto> companies, string ids)>
        CreateCompanyCollectionAsync
        (IEnumerable<CompanyForCreationDto> companyCollection);
        Task DeleteCompanyAsync(Guid companyId, bool trackChanges);
        Task UpdateCompanyAsync(Guid companyid, CompanyForUpdateDto companyForUpdate,
        bool trackChanges);
    }
}
