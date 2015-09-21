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
using BlaBlaRunProject.DataAccess.Abstract;
using BlaBlaRunProject.WebUI.Controllers.Interfaces;
using Newtonsoft.Json;

namespace BlaBlaRunProject.Controllers
{
    public class WorkoutsApiController : BaseApiController<long, Workouts>, IApiController<long, Workouts>
    {       

        public WorkoutsApiController(IUnitOfWork uow):base(uow)
        {
        }



        // GET: api/Workouts/5
        [ResponseType(typeof(Workouts))]
        public async Task<IHttpActionResult> Get(long id)
        {
            return await base.Get(id);
        }



        // POST: api/Workouts
        [ResponseType(typeof(Workouts))]
        public async Task<IHttpActionResult> Post(Workouts TEntity)
        {

            return await base.Post(TEntity);
        }

        // DELETE: api/Workouts/5
        [ResponseType(typeof(Workouts))]
        public async Task<IHttpActionResult> Delete(long id)
        {
            return await base.Delete(id);
        }




    }
}