using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMSWebApplication.Domain.Abstract
{
    public interface IRepository<Tkey, TEntity> where TEntity : class, IIdentityKey<Tkey>
    {
        TEntity GetById(Tkey id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IDbSet<TEntity> Entities { get; }
    }
}
