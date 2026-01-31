using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.Domains.Exceptions
{
    public sealed class MaxAgeRangeBadRequestException : BadRequestException
    {
        public MaxAgeRangeBadRequestException()
        : base("Max age can't be less than min age.")
        {
        }
    }
}
