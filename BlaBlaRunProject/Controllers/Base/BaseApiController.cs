using BlaBlaRunProject.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace BlaBlaRunProject.WebUI.Controllers.Interfaces
{
    public class BaseApiController<TKey, TEntity> : ApiController
        where TKey : struct, IComparable, IComparable<TKey>, IFormattable, IConvertible, IEquatable<TKey>
        where TEntity : class, IIdentityKey<TKey>
    {
        private IUnitOfWork unitOfWork;
        private IRepository<TKey, TEntity> repository;

        public BaseApiController(IUnitOfWork uow)
        {
            unitOfWork = uow;
            repository = unitOfWork.Repository<TKey, TEntity>();
        }

        // GET: api/TEntity
        public IQueryable<TEntity> Get()
        {
            //return db.TEntitySet;
            return repository.Entities;
        }

        // GET: api/TEntity/5
        public async Task<IHttpActionResult> Get(TKey id)
        {
            //TEntity TEntity = await db.TEntitySet.FindAsync(id);
            TEntity TEntity = await repository.GetByIdAsync(id);
            if (TEntity == null)
            {
                return NotFound();
            }

            return Ok(TEntity);
        }

        // PUT: api/TEntity/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Put(TKey id, TEntity TEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!id.Equals(TEntity.Id))
            {
                return BadRequest();
            }

            
            await repository.UpdateAsync(TEntity);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TEntity
        public async Task<IHttpActionResult> Post(TEntity TEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //db.TEntitySet.Add(TEntity);
            //await db.SaveChangesAsync();
            await repository.InsertAsync(TEntity);

            return CreatedAtRoute("DefaultApi", new { id = TEntity.Id }, TEntity);
        }

        // DELETE: api/TEntity/5
        public async Task<IHttpActionResult> Delete(TKey id)
        {
            //TEntity TEntity = await db.TEntitySet.FindAsync(id);
            TEntity TEntity = await repository.GetByIdAsync(id);
            if (TEntity == null)
            {
                return NotFound();
            }

            //db.TEntitySet.Remove(TEntity);
            //await db.SaveChangesAsync();
            await repository.DeleteAsync(TEntity);

            return Ok(TEntity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TEntityExists(int id)
        {
            //return db.TEntitySet.Count(e => e.Id == id) > 0;
            return repository.Entities.Count(e => e.Id.Equals(id)) > 0;
        }
    }

}
