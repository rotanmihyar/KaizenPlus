using System;
using System.Collections.Generic;
using kaizenplus.Attributes;
using kaizenplus.Enums;

namespace kaizenplus.Security
{
    [ScopedInjectable]
    public interface ISecurityManager
    {
        Guid GetUserId();
        List<string> GetUserRoles();

        bool HasRole(params Roles[] roles);
    }
}