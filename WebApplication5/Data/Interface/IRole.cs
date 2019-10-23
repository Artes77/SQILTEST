using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Data.Interface
{
    public interface IRole : IDisposable
    {
        List<Role> GetRoles();
        Role GetRoleById(int id);
        Role CreateRole(Role item);
        void UpdateRole(Role item);
        bool DeleteRole(Role item);
    }
}
