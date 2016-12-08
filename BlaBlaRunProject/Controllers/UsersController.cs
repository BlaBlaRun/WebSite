using BlaBlaRunProject.Controllers.Base;
using BlaBlaRunProject.Domain.Concrete;
using BlaBlaRunProject.WebUI.Controllers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using BlaBlaRunProject.DataAccess.Abstract;

namespace BlaBlaRunProject.Controllers
{

    [Authorize(Roles = "Admin")]
    public class UsersController : BaseController<long, Users>, IMVCController<long, Users>
    {

        public UsersController(IUnitOfWork uow) : base(uow)
        {
        }
        // GET: Users
        public async Task<ActionResult> Index()
        {
            return await Task.Run(() => View());
        }


        public ActionResult UsersList() => PartialView();

        public async Task<ActionResult> GetAllUsers()
        {

            var list = await repository.EntitiesAsync;
            //var view = await Task.Run(() => list.Where(predicate));
            var data = list.Select(x => new
            {
                Id = x.Id,
                AspNetUserId = x.AspNetUserId,
                UserName = x.UserName
            }).AsEnumerable();


            var returnJson = Json(data,JsonRequestBehavior.AllowGet);
            return returnJson;
        }
        

        Task<ActionResult> IMVCController<long, Users>.Details(long? id)
        {
            throw new NotImplementedException();
        }

        ActionResult IMVCController<long, Users>.Create()
        {
            throw new NotImplementedException();
        }

        Task<ActionResult> IMVCController<long, Users>.Create(Users oEntity)
        {
            throw new NotImplementedException();
        }

        Task<ActionResult> IMVCController<long, Users>.Edit(long? id)
        {
            throw new NotImplementedException();
        }

        Task<ActionResult> IMVCController<long, Users>.Edit(Users id)
        {
            throw new NotImplementedException();
        }

        Task<ActionResult> IMVCController<long, Users>.Delete(long? id)
        {
            throw new NotImplementedException();
        }

        Task<ActionResult> IMVCController<long, Users>.DeleteConfirmed(long id)
        {
            throw new NotImplementedException();
        }
    }
}