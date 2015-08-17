using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace VMSWebApplication.Domain.Abstract
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;
        private bool disposed;
        private Dictionary<string, object> repositories;

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public UnitOfWork()
        {
            //context = new DbContext("");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }
        public void Save()
        {
            context.SaveChanges();
        }

        public IRepository<TKey, TEntity> Repository<TKey, TEntity>() where TEntity : class, IIdentityKey<TKey>
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }

            var type = typeof(TEntity).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<TKey, TEntity>);

                var repositoryInstance = Activator.CreateInstance(typeof(Repository<TKey, TEntity>), context);
                repositories.Add(type, repositoryInstance);
            }
            return (IRepository<TKey, TEntity>)repositories[type];
        }
    }
}
