using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlaBlaRunProject.Domain.Concrete;
using System.Threading.Tasks;
using BlaBlaRunProject.Tests.Common;
using BlaBlaRunProject.Controllers;
using System.Data.Entity.Spatial;
using BlaBlaRunProject.DataAccess.Abstract;
using System.Linq;

namespace BlaBlaRunProject.Tests.Controllers
{
    [TestClass]
    public class WorkoutsControllerTest: EntityControllerApiTest<long, Workouts, WorkoutsApiController>
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


        #region DB
        [TestMethod]
        [TestCategory("DB.WorkoutsController")]
        public void GetDB()
        {
            base.GetDB();
        }

        [TestMethod]
        [TestCategory("DB.WorkoutsController")]
        public void GetByIdDB()
        {

            base.GetByIdDB();
        }

        [TestMethod]
        [TestCategory("DB.WorkoutsController")]
        public async Task PostDB()
        {
            await base.PostDB(CreateValidatedEntity, TestEditEntity);
        }

        [TestMethod]
        [TestCategory("DB.WorkoutsController")]
        public async Task PutDB()
        {
            await base.PutDB(EditEntity, TestEditEntity);
        }


        [TestMethod]
        [TestCategory("DB.WorkoutsController")]
        public async Task DeleteDB()
        {
            await base.DeleteDB();
        }




        #endregion


        #region Intergration
        
        [TestMethod]
        [TestCategory("Intergration.WorkoutsController")]
        public void GetIntergration()
        {
            var oUnitOfWork = new UnitOfWork(new EFDBContextContainer());
            IRepository<long, Workouts> oRepository = oUnitOfWork.Repository<long, Workouts>();


            int count = oRepository.Entities.Count();

            Assert.IsTrue(count >= 0);
        }


        [TestMethod]
        [TestCategory("Intergration.WorkoutsController")]
        public async Task AddAndDelete()
        {
            await PostDBUser();
            await base.AddAndDelete(CreateValidatedEntity, TestEditEntity);
            await DeleteDBUser();

        }


        [TestMethod]
        [TestCategory("Intergration.WorkoutsController")]
        public async Task AddUpdateDeleted()
        {
            await PostDBUser();
            await base.AddUpdateDeleted(CreateValidatedEntity, EditEntity, TestEditEntity);
            await DeleteDBUser();
        }

        private async Task PostDBUser()
        {
            var oUsersControllerTest = new UsersControllerTest();
            await oUsersControllerTest.PostDB();
        }

        private async Task DeleteDBUser()
        {
            var oUsersControllerTest = new UsersControllerTest();
            await oUsersControllerTest.DeleteDB();
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


        public Workouts CreateValidatedEntity(long IdParam)
        {

            var oUnitOfWork = new UnitOfWork(new EFDBContextContainer());
            IRepository<long , Workouts > oWorkoutsRepository = oUnitOfWork.Repository<long, Workouts>();
            IRepository<long, Users> oUsersRepository = oUnitOfWork.Repository<long, Users>();

            long oEntityLastUsersId = -1;
            var oEntityLastUsers = oUsersRepository.Entities.OrderByDescending(x => x.Id).FirstOrDefault();
            if (oEntityLastUsers != null)
            {
                oEntityLastUsersId = oEntityLastUsers.Id;
            }
            else
            {
                Assert.Fail("No Users created");
            }

            Workouts oWorkouts = new Workouts()
            {
                Id = IdParam,
                UsersId = oEntityLastUsersId,
                StartLocation = DbGeography.FromText("POINT(-122.335197 47.646711)"),
                StartDateTime = DateTime.Now,
                Circular = true
            };
            
        //    public long Id { get; set; }
        //public long UsersId { get; set; }
        //public System.DateTime StartDateTime { get; set; }
        //public System.Data.Entity.Spatial.DbGeography StartLocation { get; set; }

            //public bool Circular { get; set; }
            //public string City { get; set; }
            //public string Region { get; set; }
            //public string Country { get; set; }
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
