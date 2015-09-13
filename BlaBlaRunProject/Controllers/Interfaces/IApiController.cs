using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using BlaBlaRunProject.DataAccess.Abstract;

namespace BlaBlaRunProject.WebUI.Controllers
{
    public interface IApiController<TKey, TEntity>
        where TEntity : class, IIdentityKey<TKey>
        where TKey : struct, IComparable, IComparable<TKey>, IFormattable, IConvertible,  IEquatable<TKey>
    {
        IQueryable<TEntity> Get();
        Task<IHttpActionResult> Get(TKey id);
        Task<IHttpActionResult> Put(TKey id, TEntity entity);
        Task<IHttpActionResult> Post(TEntity entity);
        Task<IHttpActionResult> Delete(TKey id);

        //void Dispose(bool disposing);


    }
}
