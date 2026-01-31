using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.Domains.Exceptions
{
    public sealed class CompanyCollectionBadRequest : BadRequestException
    {
        public CompanyCollectionBadRequest()
: base("Company collection sent from a client is null.")
        {

        }
    }
}
