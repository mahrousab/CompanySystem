using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.Application.DTOS
{
    public record CompanyForCreationDto(string Name, string Address, string Country, IEnumerable<EmployeeForCreationDto> Employees);

}
