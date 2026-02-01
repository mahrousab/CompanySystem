using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.Domains.ConfigurationModels
{
    public class JwtConfiguration
    {
        public string Section { get; set; } = "JwtSettings";
        public string? ValidIssuer { get; set; }
        public string? ValidAudience { get; set; }
        public string? Expires { get; set; } // تأكد إنك بتحوله لـ Double أو Int وقت الاستخدام
        public string? Key { get; set; } // 👈 لازم تضيف السطر ده
    }
}
