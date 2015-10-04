using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BlaBlaRunProject.DataAccess.Abstract;

namespace BlaBlaRunProject.WebUI.Controllers.Interfaces
{
    public interface IMVCController<TKey, TEntity>
        where TKey : struct,
          IComparable,
          IComparable<TKey>,
          IConvertible,
          IEquatable<TKey>,
          IFormattable
        where TEntity : class, IIdentityKey<TKey>
    {

        Task<ActionResult> Index();
        Task<ActionResult> Details(TKey? id);
        ActionResult Create();
        Task<ActionResult> Create(TEntity oEntity);
        Task<ActionResult> Edit(TKey? id);
        Task<ActionResult> Edit(TEntity id);
        Task<ActionResult> Delete(TKey? id);
        Task<ActionResult> DeleteConfirmed(TKey id);

    }
}
