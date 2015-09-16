using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlaBlaRunProject.Domain.Concrete;
using System.Threading.Tasks;
using BlaBlaRunProject.Tests.Common;
using BlaBlaRunProject.Controllers;

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
        public async Task Patch()
        {
            await base.Patch(GenericMethods.SetupWorkoutsList, EditEntity, TestEditEntity);
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
                //Company = "TestUsers"
            };
            return oWorkouts;

        }

        //public Delta<Workouts> EditEntity(Workouts oWorkouts)
        //{
        //    var oUserDelta = new Delta<Workouts>(typeof(Workouts));
        //    oUserDelta.TrySetPropertyValue("Id", oUser.Id);
        //    oUserDelta.TrySetPropertyValue("Company", "TestUsers");
        //    return oUserDelta;
        //}

        //public bool TestEditEntity(Workouts oWorkouts)
        //{
        //    bool bResult = oWorkouts.Company == "TestUsers";
        //    return bResult;

        //}


    }
}
