using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data.Interface;

namespace WebApplication5.Data
{
    public class PermissionRepository : IPermission
    {
        private readonly AppDbContext context;

        public PermissionRepository(AppDbContext context)
        {
            this.context = context;
        }

        public List<Permission> GetPermissions()
        {
            return context.Permission.OrderBy(x => x.PermissionName).ToList();
        }

        public Permission GetPermissionById(int id)
        {
            return context.Permission.Single(x => x.Id == id);
        }

        public void UpdatePermission(Permission entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public Permission CreatePermission(Permission entity)
        {
            context.Permission.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public void DeletePermission(Permission entity)
        {
            context.Permission.Remove(entity);
            context.SaveChanges();
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
