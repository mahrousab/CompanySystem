using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.Domains.Exceptions
{
    public abstract class NotFoundException : Exception
    {
        protected NotFoundException(string message)
: base(message)
        { }
    }
}
