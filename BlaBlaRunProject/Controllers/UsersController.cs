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

namespace BlaBlaRunProject.Controllers
{
    public class UsersController : BaseApiController<long, Users>, IApiController<long, Users>
    {

        public UsersController(IUnitOfWork uow):base(uow)
        {
        }



        // GET: api/Users/5
        [ResponseType(typeof(Users))]
        public async Task<IHttpActionResult> Get(long id)
        {
            return await base.Get(id);
        }



        // POST: api/Users
        [ResponseType(typeof(Users))]
        public async Task<IHttpActionResult> Post(Users TEntity)
        {

            return await base.Post(TEntity);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(Users))]
        public async Task<IHttpActionResult> Delete(long id)
        {
            return await base.Delete(id);
        }
        

    }
}