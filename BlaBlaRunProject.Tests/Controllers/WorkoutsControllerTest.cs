using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlaBlaRunProject.Domain.Concrete;
using System.Threading.Tasks;
using BlaBlaRunProject.Tests.Common;
using BlaBlaRunProject.Controllers;
using System.Data.Entity.Spatial;

namespace BlaBlaRunProject.Tests.Controllers
{
    [TestClass]
    public class WorkoutsControllerTest: EntityControllerApiTest<long, Workouts, WorkoutsController>
    {

        #region Mock
        [TestMethod]
        [TestCategory("WorkoutsController")]
        public void Get()
        {
            base.Get(GenericMethods.SetupWorkoutsList);
        }

        [TestMethod]
        [TestCategory("WorkoutsController")]
        public void GetById()
        {

            base.GetById(GenericMethods.SetupWorkoutsList);
        }

        [TestMethod]
        [TestCategory("WorkoutsController")]
        public async Task Post()
        {
            await base.Post(GenericMethods.SetupWorkoutsList, CreateEntity, TestEditEntity);
        }

        [TestMethod]
        [TestCategory("WorkoutsController")]
        public async Task Put()
        {
            await base.Put(GenericMethods.SetupWorkoutsList, EditEntity, TestEditEntity);
        }
        

        [TestMethod]
        [TestCategory("WorkoutsController")]
        public async Task Delete()
        {
            await base.Delete(GenericMethods.SetupWorkoutsList);
        }




        #endregion


        public Workouts CreateEntity(long IdParam)
        {
            Workouts oWorkouts = new Workouts()
            {
                Id = IdParam,
                StartLocation = DbGeography.FromText("POINT(-122.335197 47.646711)")
            };
            return oWorkouts;

        }

        public Workouts EditEntity(Workouts oWorkouts)
        {
            oWorkouts.StartLocation =  DbGeography.FromText("POINT(-122.335197 47.646711)");
            return oWorkouts;
        }

        public bool TestEditEntity(Workouts oWorkouts)
        {
            bool bResult = oWorkouts.StartLocation.Distance(DbGeography.FromText("POINT(-122.335197 47.646711)")) == 0;
            return bResult;

        }


    }
}
