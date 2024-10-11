using Microsoft.EntityFrameworkCore;
using OnlineEducationPlatform.DAL.Data.DbHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EducationPlatformContext _Context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(EducationPlatformContext context)
        {
            _Context=context;
            _dbSet = _Context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            var entits = _dbSet.Find();
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }
       
        public void Update(T entity)
        {
           
        }

        public void SaveChange()
        {
            _Context.SaveChanges();
        }

       
    }
}
