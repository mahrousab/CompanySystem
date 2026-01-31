
using CompanySystem.Application.Interfaces;
using CompanySystem.Infrastructure;
using CompanySystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.Infrastructure.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly CompanySystemDbContext _repositoryContext;
        private readonly Lazy<ICompanyRepository> _companyRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        public RepositoryManager(CompanySystemDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _companyRepository = new Lazy<ICompanyRepository>(() => new
            CompanyRepository(repositoryContext));
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new
            EmployeeRepository(repositoryContext));
        }
        public ICompanyRepository Company => _companyRepository.Value;
        public IEmployeeRepository Employee => _employeeRepository.Value;
        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();

    }
}
