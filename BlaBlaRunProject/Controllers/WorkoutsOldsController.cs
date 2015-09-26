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
using BlaBlaRunProject.Controllers.Base;

namespace BlaBlaRunProject.Controllers
{
    public class WorkoutsOldsController : BaseController<long, WorkoutsOld>
    {
        public WorkoutsOldsController(IUnitOfWork uow) : base(uow)
        {
        }
        // GET: WorkoutsOlds
        public async Task<ActionResult> Index()
        {
            return await base.Index();
        }

        // GET: WorkoutsOlds/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            return await base.Details(id);
        }

        // GET: WorkoutsOlds/Create
        public ActionResult Create()
        {
            return  base.Create();
        }

        // POST: WorkoutsOlds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,UsersId,StartDateTime,StartLocation,AVGPace,Circular,EndLocation,Distance,MaxNumberPeaople,City,Region,Country,ElevationGain")] WorkoutsOld workoutsOld)
        {
            return await base.Create(workoutsOld);
        }

        // GET: WorkoutsOlds/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            return await base.Edit(id);
        }

        // POST: WorkoutsOlds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,UsersId,StartDateTime,StartLocation,AVGPace,Circular,EndLocation,Distance,MaxNumberPeaople,City,Region,Country,ElevationGain")] WorkoutsOld workoutsOld)
        {
            return await base.Edit(workoutsOld);
        }

        // GET: WorkoutsOlds/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            return await base.Delete(id);
        }

        // POST: WorkoutsOlds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            return await base.DeleteConfirmed(id);
        }
        
    }
}
