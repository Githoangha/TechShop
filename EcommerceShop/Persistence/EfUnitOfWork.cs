using Domain.Abstractrions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public EfUnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
