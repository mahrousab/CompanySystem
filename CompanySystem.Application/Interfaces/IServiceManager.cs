using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.Application.Interfaces
{
    public interface IServiceManager
    {
        ICompanyService CompanyService { get; }
        IEmployeeService EmployeeService { get; }
        IAuthenticationService AuthenticationService { get; }

    }
}
