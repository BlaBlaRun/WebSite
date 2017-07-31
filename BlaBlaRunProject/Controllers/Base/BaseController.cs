using BlaBlaRunProject.DataAccess.Abstract;
using BlaBlaRunProject.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlaBlaRunProject.Controllers.Base
{
    public class BaseController<TKey, TEntity> : Controller
        where TKey : struct, IComparable, IComparable<TKey>, IFormattable, IConvertible, IEquatable<TKey>
        where TEntity : class, IIdentityKey<TKey>
    {
        protected IUnitOfWork unitOfWork;
        protected IRepository<long, Audit> oAuditRepository;
        protected IRepository<TKey, TEntity> repository;

        public BaseController(IUnitOfWork uow)
        {
            unitOfWork = uow;
            oAuditRepository = unitOfWork.Repository<long, Audit>();
            repository = unitOfWork.Repository<TKey, TEntity>();
        }

        // GET: TEntity
        protected async Task<ActionResult> Index()
        {
            var sActionType = "OnClick";
            var sElement = "Index";
            CreateAudit(sActionType, sElement);
            return View(await repository.EntitiesAsync);
        }

        private void CreateAudit(string sActionType, string sElement)
        {
            var oAudit = new Audit();
            oAudit.UserIp = GetIPAddress();
            oAudit.UserAgent = Request.UserAgent;
            oAudit.ActionType = sActionType;
            oAudit.Element = sElement;
            oAudit.ActionUTCDate = DateTime.UtcNow;
            oAuditRepository.Insert(oAudit);
        }

        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }


        // GET: TEntity
        protected async Task<ActionResult> Index(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            var sActionType = "OnClick";
            var sElement = "Index";
            CreateAudit(sActionType, sElement);
            var list = await repository.EntitiesAsync;
            var view = await Task.Run(() => list.Where(predicate));
            
            return View(view);
        }


        // GET: TEntity/Details/5
        protected async Task<ActionResult> Details(TKey? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TEntity TEntity = await repository.GetByIdAsync(id.Value);
            if (TEntity == null)
            {
                return HttpNotFound();
            }
            return View(TEntity);
        }

        // GET: TEntity/Create
        protected ActionResult Create()
        {

            //var usersRepository = unitOfWork.Repository<TKey, Users>();
            //ViewBag.UsersId = new SelectList(usersRepository.Entities, "Id", "Id");
            return View();
        }

        // POST: TEntity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        protected async Task<ActionResult> Create([Bind] TEntity TEntity)
        {
            if (ModelState.IsValid)
            {
                await repository.InsertAsync(TEntity);
                return RedirectToAction("Index");
            }

            //var usersRepository = unitOfWork.Repository<TKey, Users>();
            //ViewBag.UsersId = new SelectList(usersRepository.Entities, "Id", "Id");
            return View(TEntity);
        }

        // GET: TEntity/Edit/5
        protected async Task<ActionResult> Edit(TKey? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TEntity TEntity = await repository.GetByIdAsync(id.Value);
            if (TEntity == null)
            {
                return HttpNotFound();
            }
            //var usersRepository = unitOfWork.Repository<TKey, Users>();
            //ViewBag.UsersId = new SelectList(usersRepository.Entities, "Id", "Id");
            return View(TEntity);
        }

        // POST: TEntity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        protected async Task<ActionResult> Edit([Bind] TEntity TEntity)
        {
            if (ModelState.IsValid)
            {
                await repository.UpdateAsync(TEntity);
                return RedirectToAction("Index");
            }
            //var usersRepository = unitOfWork.Repository<TKey, Users>();
            //ViewBag.UsersId = new SelectList(usersRepository.Entities, "Id", "Id");
            return View(TEntity);
        }

        // GET: TEntity/Delete/5
        protected async Task<ActionResult> Delete(TKey? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TEntity TEntity = await repository.GetByIdAsync(id.Value);
            if (TEntity == null)
            {
                return HttpNotFound();
            }
            return View(TEntity);
        }

        // POST: TEntity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        protected async Task<ActionResult> DeleteConfirmed(TKey id)
        {
            TEntity TEntity = await repository.GetByIdAsync(id);
            await repository.DeleteAsync(TEntity);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}