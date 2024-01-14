using Domain.Abstractrions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class EfRepository1<TEntity, Tkey> : IRepository1<TEntity, Tkey> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        public EfRepository1(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<TEntity> FindAll()
        {
            //var result =  _context.Set<TEntity>().ToList();
            //return result;
            return _context.Set<TEntity>().ToList();
        }

        public IQueryable<TEntity> FindAllAsync()
        {
            var result = _context.Set<TEntity>().AsQueryable();
            return result;
        }

        public async Task<TEntity> FindById(Tkey id)
        {
            var result = await _context.Set<TEntity>().FindAsync(id);
            return result;
        }
        public void Add(TEntity Object)
        {
            _context.Set<TEntity>().Add(Object);
        }
        public void Update(TEntity Object)
        {
            _context.Set<TEntity>().Update(Object);
        }
        

        public void Delete(TEntity Object)
        {
            _context.Set<TEntity>().Remove(Object);
        }
        
    }
}
