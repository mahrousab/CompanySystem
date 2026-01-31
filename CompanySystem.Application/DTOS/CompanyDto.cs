using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.Application.DTOS
{
    public record CompanyDto(Guid Id, string Name, string FullAddress);

}
