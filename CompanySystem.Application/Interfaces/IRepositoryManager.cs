using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.Application.Interfaces
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        Task SaveAsync();
    }
}
