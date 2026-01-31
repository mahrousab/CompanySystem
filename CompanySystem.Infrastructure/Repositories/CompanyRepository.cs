
using CompanySystem.Application.Interfaces;
using CompanySystem.Domains.Models;
using CompanySystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.Infrastructure.Repositories
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(CompanySystemDbContext context) : base(context)
        {
            
        }

        public void CreateCompany(Company company)
        =>Create(company);

        public void DeleteCompany(Company company)
        =>Delete(company);

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges) =>
  await FindAll(trackChanges)
  .OrderBy(c => c.Name)
  .ToListAsync();

        public async Task< IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
         =>
await FindByCondition(x => ids.Contains(x.Id), trackChanges)
 .ToListAsync();


        public async Task< Company> GetCompanyAsync(Guid companyId, bool trackChanges)
       =>await  FindByCondition(x => x.Equals(companyId), trackChanges).SingleOrDefaultAsync();
    }
}
