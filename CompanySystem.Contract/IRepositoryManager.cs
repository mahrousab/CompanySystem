using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.Contract
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        void Save();
    }
}
