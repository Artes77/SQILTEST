using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Data.Interface;

namespace WebApplication5.Data
{
    public class UserRepository: IUser
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }

        public List<User> GetUsers()
        {
            foreach (User u in context.User.Include(p => p.Role));
            return context.User.OrderBy(x => x.Id).ToList();
        }

        public User GetUserById(int id)
        {
            foreach (User u in context.User.Include(p => p.Role)) ;
            return context.User.Single(x => x.Id == id);
        }

        public void UpdateUser(User entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public User CreateUser(User entity)
        {
            context.User.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public void DeleteUser(User entity)
        {
            context.User.Remove(entity);
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
