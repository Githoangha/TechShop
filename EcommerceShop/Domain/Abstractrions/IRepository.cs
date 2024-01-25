using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractrions
{
    public interface IRepository1<TEntity, TKey> where TEntity : class
    {
        IQueryable<TEntity> FindAllAsync();
        IQueryable<TEntity> FindAll();
        Task<TEntity> FindById(TKey id);
        void Add(TEntity Object);
        void Update(TEntity Object);
        void Delete(TEntity Object);
        
    }

}
