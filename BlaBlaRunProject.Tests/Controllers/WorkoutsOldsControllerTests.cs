using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlaBlaRunProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaBlaRunProject.DataPopulator;
using BlaBlaRunProject.Domain.Concrete;
using BlaBlaRunProject.DataAccess.Abstract;
using BlaBlaRunProject.Tests.Controllers;
using System.Data.Entity.Spatial;
using System.Threading;

namespace BlaBlaRunProject.Controllers.Tests
{
    [TestClass()]
    public class WorkoutsOldsControllerTests : EntityControllerTest<long, WorkoutsOld, WorkoutsOldsController>
    {
        [TestMethod()]
        public void PopulateWorkoutsOldsDB()
        {
            var list = WorkoutsOldInitializer.PopulateData();

            foreach (var item in list)
            {
                var oUnitOfWork = new UnitOfWork(new EFDBContextContainer());
                IRepository<long, WorkoutsOld> oRepository = oUnitOfWork.Repository<long, WorkoutsOld>();
                oRepository.Insert(item);
                //Thread.Sleep(1000);

            }
        }



        #region DB

        [TestMethod]
        [TestCategory("DB.WorkoutsOldController")]
        public async Task IndexWithDB()
        {
            await base.IndexWithDB();
        }


        [TestMethod]
        [TestCategory("DB.WorkoutsOldController")]
        public async Task DetailsWithDB()
        {
            await base.DetailsWithDB();
        }

        [TestMethod]
        [TestCategory("DB.WorkoutsOldController")]
        public async Task CreateWithDB()
        {
            await base.CreateWithDB(CreateEntity, TestCreateEntity);
        }

        [TestMethod]
        [TestCategory("DB.WorkoutsOldController")]
        public async Task EditWithDB()
        {
            await base.EditWithDB(EditEntity, TestEditEntity);
        }


        [TestMethod]
        [TestCategory("DB.WorkoutsOldController")]
        public async Task DeleteWithDB()
        {
            await base.DeleteWithDB();
        }
        
        #endregion



        public override WorkoutsOld CreateEntity(long IdParam)
        {
            WorkoutsOld oWorkoutsOld = new WorkoutsOld()
            {
                Id = IdParam,
                StartLocation = DbGeography.FromText("POINT(-122.335197 57.646711)"),
                StartDateTime = DateTime.Now,
                Circular = true
            };
            return oWorkoutsOld;

        }

        public override bool TestCreateEntity(WorkoutsOld oWorkoutsOld)
        {
            bool bResult = oWorkoutsOld.StartLocation.Distance(DbGeography.FromText("POINT(-122.335197 57.646711)")) == 0;
            return bResult;

        }

        public override WorkoutsOld EditEntity(WorkoutsOld oWorkoutsOld)
        {
            oWorkoutsOld.StartLocation = DbGeography.FromText("POINT(-122.335197 47.646711)");
            return oWorkoutsOld;
        }

        public override bool TestEditEntity(WorkoutsOld oWorkoutsOld)
        {
            bool bResult = oWorkoutsOld.StartLocation.Distance(DbGeography.FromText("POINT(-122.335197 47.646711)")) == 0;
            return bResult;

        }
    }
}