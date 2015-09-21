using BlaBlaRunProject.DataAccess.Abstract;
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
        private IUnitOfWork unitOfWork;
        private IRepository<TKey, TEntity> repository;

        public BaseController(IUnitOfWork uow)
        {
            unitOfWork = uow;
            repository = unitOfWork.Repository<TKey, TEntity>();
        }
        
        // GET: TEntity
        public async Task<ActionResult> Index()
        {
            //var TEntitySet = repository.Entities.Include(w => w.Users);
            var TEntitySet = repository.Entities;
            return View(await TEntitySet.ToListAsync());
        }

        // GET: TEntity/Details/5
        public async Task<ActionResult> Details(TKey? id)
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
        public ActionResult Create()
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
        public async Task<ActionResult> Create([Bind] TEntity TEntity)
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
        public async Task<ActionResult> Edit(TKey? id)
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
        public async Task<ActionResult> Edit([Bind] TEntity TEntity)
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
        public async Task<ActionResult> Delete(TKey? id)
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
        public async Task<ActionResult> DeleteConfirmed(TKey id)
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