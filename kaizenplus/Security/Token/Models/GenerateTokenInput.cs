using System;
using System.Collections.Generic;

namespace kaizenplus.Security.Token.Models
{
    public class GenerateTokenInput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? CompanyId { get; set; }
        public List<string> Roles { get; set; }
    }
}