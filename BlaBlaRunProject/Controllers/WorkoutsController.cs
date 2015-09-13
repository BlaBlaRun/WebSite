using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BlaBlaRunProject.Domain.Concrete;
using BlaBlaRunProject.WebUI.Controllers;
using BlaBlaRunProject.DataAccess.Abstract;

namespace BlaBlaRunProject.Controllers
{
    public class WorkoutsController : ApiController, IApiController<long, Workouts>
    {
        private IUnitOfWork unitOfWork;
        private IRepository<long, Workouts> repository;

        public WorkoutsController(IUnitOfWork uow)
        {
            unitOfWork = uow;
            repository = unitOfWork.Repository<long, Workouts>();
        }

        // GET: api/Workouts
        public IQueryable<Workouts> Get()
        {
            //return db.WorkoutsSet;
            return repository.Entities;
        }

        // GET: api/Workouts/5
        [ResponseType(typeof(Workouts))]
        public async Task<IHttpActionResult> Get(long id)
        {
            //Workouts workouts = await db.WorkoutsSet.FindAsync(id);
            Workouts workouts = await repository.GetByIdAsync(id);
            if (workouts == null)
            {
                return NotFound();
            }

            return Ok(workouts);
        }

        // PUT: api/Workouts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Put(long id, Workouts workouts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workouts.Id)
            {
                return BadRequest();
            }

            //db.Entry(workouts).State = EntityState.Modified;

            //try
            //{
            //    await db.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!WorkoutsExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            await repository.InsertAsync(workouts);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Workouts
        [ResponseType(typeof(Workouts))]
        public async Task<IHttpActionResult> Post(Workouts workouts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //db.WorkoutsSet.Add(workouts);
            //await db.SaveChangesAsync();
            await repository.UpdateAsync(workouts);

            return CreatedAtRoute("DefaultApi", new { id = workouts.Id }, workouts);
        }

        // DELETE: api/Workouts/5
        [ResponseType(typeof(Workouts))]
        public async Task<IHttpActionResult> Delete(long id)
        {
            //Workouts workouts = await db.WorkoutsSet.FindAsync(id);
            Workouts workouts = await repository.GetByIdAsync(id);
            if (workouts == null)
            {
                return NotFound();
            }

            //db.WorkoutsSet.Remove(workouts);
            //await db.SaveChangesAsync();
            await repository.DeleteAsync(workouts);

            return Ok(workouts);
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

        private bool WorkoutsExists(int id)
        {
            //return db.WorkoutsSet.Count(e => e.Id == id) > 0;
            return repository.Entities.Count(e => e.Id == id) > 0;
        }
    }
}