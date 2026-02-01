using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.Application.DTOS
{
    public record TokenDto(string AccessToken, string RefreshToken);
}
