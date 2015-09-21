using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlaBlaRunProject.Domain.Concrete;
using BlaBlaRunProject.DataAccess.Abstract;
using System.Web.Script.Serialization;

namespace BlaBlaRunProject.Controllers
{
    public class WorkoutsController: Controller
    {
        private IUnitOfWork unitOfWork;
        private IRepository<long, Workouts> repository;

        public WorkoutsController(IUnitOfWork uow)
        {
            unitOfWork = uow;
            repository = unitOfWork.Repository<long, Workouts>();
        }
        // GET: Workouts
        public async Task<ActionResult> Index()
        {
            var workoutsSet = repository.Entities.Include(w => w.Users);
            return View(await workoutsSet.ToListAsync());
        }

        // GET: Workouts/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workouts workouts = await repository.GetByIdAsync(id.Value);
            if (workouts == null)
            {
                return HttpNotFound();
            }
            return View(workouts);
        }

        // GET: Workouts/Create
        public ActionResult Create()
        {

            var usersRepository = unitOfWork.Repository<long, Users>();
            ViewBag.UsersId = new SelectList(usersRepository.Entities, "Id", "Id");
            return View();
        }

        // POST: Workouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,UsersId,StartDateTime,StartLocation,AVGPace,Circular,EndLocation,Distance,MaxNumberPeaople,City,Region,Country,ElevationGain")] Workouts workouts)
        {
            if (ModelState.IsValid)
            {
                await repository.InsertAsync(workouts);
                return RedirectToAction("Index");
            }

            var usersRepository = unitOfWork.Repository<long, Users>();
            ViewBag.UsersId = new SelectList(usersRepository.Entities, "Id", "Id");
            return View(workouts);
        }

        // GET: Workouts/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workouts workouts = await repository.GetByIdAsync(id.Value);
            if (workouts == null)
            {
                return HttpNotFound();
            }
            var usersRepository = unitOfWork.Repository<long, Users>();
            ViewBag.UsersId = new SelectList(usersRepository.Entities, "Id", "Id");
            return View(workouts);
        }

        // POST: Workouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,UsersId,StartDateTime,StartLocation,AVGPace,Circular,EndLocation,Distance,MaxNumberPeaople,City,Region,Country,ElevationGain")] Workouts workouts)
        {
            if (ModelState.IsValid)
            {
                await repository.UpdateAsync(workouts);
                return RedirectToAction("Index");
            }
            var usersRepository = unitOfWork.Repository<long, Users>();
            ViewBag.UsersId = new SelectList(usersRepository.Entities, "Id", "Id");
            return View(workouts);
        }

        // GET: Workouts/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workouts workouts = await repository.GetByIdAsync(id.Value);
            if (workouts == null)
            {
                return HttpNotFound();
            }
            return View(workouts);
        }

        // POST: Workouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Workouts workouts = await repository.GetByIdAsync(id);
            await repository.DeleteAsync(workouts);
            return RedirectToAction("Index");
        }


        public async Task<ActionResult> ListOld()
        {
            return View();
        }

        public System.Web.Mvc.ActionResult GetWorkoutsDataJson(string selectedCategory = "All")
        {
            var data = repository.Entities.AsEnumerable();
            var json = Json(new
            {
                data
            }, JsonRequestBehavior.AllowGet);
            string s = new JavaScriptSerializer().Serialize(json.Data);
            return Json(new
            {
                data
            }, JsonRequestBehavior.AllowGet);

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
