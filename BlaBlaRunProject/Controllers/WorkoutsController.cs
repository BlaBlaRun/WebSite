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
using BlaBlaRunProject.Controllers.Interfaces;

namespace BlaBlaRunProject.Controllers
{
    public class WorkoutsController : BaseApiController<long, Workouts>, IApiController<long, Workouts>
    {       

        public WorkoutsController(IUnitOfWork uow):base(uow)
        {
        }



        // GET: api/TEntity/5
        [ResponseType(typeof(Workouts))]
        public async Task<IHttpActionResult> Get(long id)
        {
            return await base.Get(id);
        }



        // POST: api/TEntity
        [ResponseType(typeof(Workouts))]
        public async Task<IHttpActionResult> Post(Workouts TEntity)
        {

            return await base.Post(TEntity);
        }

        // DELETE: api/TEntity/5
        [ResponseType(typeof(Workouts))]
        public async Task<IHttpActionResult> Delete(long id)
        {
            return await base.Delete(id);
        }

    }
}