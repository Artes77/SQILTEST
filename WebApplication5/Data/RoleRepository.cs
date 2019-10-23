using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data.Interface;

namespace WebApplication5.Data
{
    public class RoleRepository: IRole
    {
        private readonly AppDbContext context;

        public RoleRepository(AppDbContext context)
        {
            this.context = context;
        }

        public List<Role> GetRoles()
        {
            return context.Role.OrderBy(x => x.Id).ToList();
        }

        public Role GetRoleById(int id)
        {
            return context.Role.Single(x => x.Id == id);
        }

        public void UpdateRole(Role entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public Role CreateRole(Role entity)
        {
            context.Role.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public bool DeleteRole(Role entity)
        {
            try
            {
                context.Role.Remove(entity);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
