using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.Domains.Exceptions
{
    public sealed class CompanyNotFoundException : NotFoundException
    {
        public CompanyNotFoundException(Guid companyId) :
            base($"The company with id: {companyId} doesn't exist in the database.")
        {
        }
    }
}
