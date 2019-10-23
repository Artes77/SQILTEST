using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Data.Interface
{
    public interface IPermission : IDisposable
    {
        List<Permission> GetPermissions();
        Permission GetPermissionById(int id);
        Permission CreatePermission(Permission item);
        void UpdatePermission(Permission item);
        void DeletePermission(Permission item);
    }
}
