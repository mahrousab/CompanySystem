using AutoMapper;
using CompanySystem.Application.DTOS;
using CompanySystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.Application.services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICompanyService> _companyService;
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly IMapper _mapper;
        private readonly IDataShaper<EmployeeDto> _dataShaper;
        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager
        logger, IMapper mapper, IDataShaper<EmployeeDto> dataShaper)
        {
            _companyService = new Lazy<ICompanyService>(() => new
            CompanyService(repositoryManager, logger,mapper));
            _employeeService = new Lazy<IEmployeeService>(() => new
            EmployeeService(repositoryManager, logger,mapper,dataShaper));

        }
        public ICompanyService CompanyService => _companyService.Value;
        public IEmployeeService EmployeeService => _employeeService.Value;
    }
}
