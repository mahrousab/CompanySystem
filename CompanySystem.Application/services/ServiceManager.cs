using AutoMapper;
using CompanySystem.Application.DTOS;
using CompanySystem.Application.Interfaces;
using CompanySystem.Domains.ConfigurationModels;
using CompanySystem.Domains.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager
        logger, IMapper mapper, IDataShaper<EmployeeDto> dataShaper
            ,UserManager<User> userManager,IConfiguration configuration, JwtConfiguration _jwtConfiguration)
        {
            _companyService = new Lazy<ICompanyService>(() => new
            CompanyService(repositoryManager, logger,mapper));
            _employeeService = new Lazy<IEmployeeService>(() => new
            EmployeeService(repositoryManager, logger,mapper,dataShaper));
            
            _authenticationService = new Lazy<IAuthenticationService>(() =>
                new AuthenticationService(logger, mapper, userManager, configuration, _jwtConfiguration)); 
        }
        public ICompanyService CompanyService => _companyService.Value;
        public IEmployeeService EmployeeService => _employeeService.Value;
        public IAuthenticationService AuthenticationService =>
          _authenticationService.Value;
    }
}
