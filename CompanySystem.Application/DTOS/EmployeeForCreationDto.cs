using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.Application.DTOS
{
    public record EmployeeForCreationDto(string Name, int Age, string Position);

}
