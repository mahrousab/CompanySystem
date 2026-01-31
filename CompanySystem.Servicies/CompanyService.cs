using CompanySystem.Contract;
using CompanySystem.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.Servicies
{
    public sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public CompanyService(IRepositoryManager repository, ILoggerManager
logger)
        {
            _repository = repository;
            _logger = logger;
        }
    }
}
