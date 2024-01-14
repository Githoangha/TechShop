using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractrions
{
    public interface IUnitOfWork
    {
        Task SaveChangeAsync();
    }
}
