using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        // Method to get any repository on the fly
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;

        // Save changes to the database asynchronously
        Task<int> CompleteAsync();
    }
}
