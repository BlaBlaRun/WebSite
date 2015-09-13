using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaBlaRunProject.DataAccess.Abstract
{
    public interface IRepository<Tkey, TEntity> where TEntity : class, IIdentityKey<Tkey>
    {
        TEntity GetById(Tkey id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IDbSet<TEntity> Entities { get; }


        Task<TEntity> GetByIdAsync(Tkey id);
        Task InsertAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<IDbSet<TEntity>> EntitiesAsync { get; }
    }
}
