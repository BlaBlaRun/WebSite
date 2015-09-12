using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BlaBlaRunProject.Domain.Abstract;


namespace BlaBlaRunProject.WebUI.Controllers
{
    public interface IFMCController<TEntity, TIdentityType>
        where TEntity : class, IIdentityKey<TIdentityType>
        where TIdentityType : struct,
          IComparable,
          IComparable<TIdentityType>,
          IConvertible,
          IEquatable<TIdentityType>,
          IFormattable
    {

        Task<ActionResult> Index();
        Task<ActionResult> Details(TIdentityType? id);
        Task<ActionResult> Create(TEntity oEntity);
        Task<ActionResult> Edit(TIdentityType? id);
        Task<ActionResult> Edit(TEntity oEntity);
        
    }
}
