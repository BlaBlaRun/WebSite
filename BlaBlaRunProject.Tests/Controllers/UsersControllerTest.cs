using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlaBlaRunProject.Tests.Common;
using BlaBlaRunProject.Controllers;
using BlaBlaRunProject.DataAccess.Abstract;
using System.Linq;
using BlaBlaRunProject.Domain.Concrete;
using System.Threading.Tasks;

namespace BlaBlaRunProject.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTest : EntityControllerApiTest<long, Users, UsersController>
    {
        Guid GuidTest = Guid.NewGuid();
        #region Mock
        [TestMethod]
        [TestCategory("UsersController")]
        public void Get()
        {
            base.Get(GenericMethods.SetupUsersList);
        }

        [TestMethod]
        [TestCategory("UsersController")]
        public void GetById()
        {

            base.GetById(GenericMethods.SetupUsersList);
        }

        [TestMethod]
        [TestCategory("UsersController")]
        public async Task Post()
        {
            await base.Post(GenericMethods.SetupUsersList, CreateEntity, TestEditEntity);
        }

        [TestMethod]
        [TestCategory("UsersController")]
        public async Task Put()
        {
            await base.Put(GenericMethods.SetupUsersList, EditEntity, TestEditEntity);
        }


        [TestMethod]
        [TestCategory("UsersController")]
        public async Task Delete()
        {
            await base.Delete(GenericMethods.SetupUsersList);
        }




        #endregion


        #region DB
        [TestMethod]
        [TestCategory("DB.UsersController")]
        public void GetDB()
        {
            base.GetDB();
        }

        [TestMethod]
        [TestCategory("DB.UsersController")]
        public void GetByIdDB()
        {

            base.GetByIdDB();
        }

        [TestMethod]
        [TestCategory("DB.UsersController")]
        public async Task PostDB()
        {
            await base.PostDB(CreateValidatedEntity, TestEditEntity);
        }

        [TestMethod]
        [TestCategory("DB.UsersController")]
        public async Task PutDB()
        {
            await base.PutDB(EditEntity, TestEditEntity);
        }


        [TestMethod]
        [TestCategory("DB.UsersController")]
        public async Task DeleteDB()
        {
            await base.DeleteDB();
        }




        #endregion


        #region Intergration

        [TestMethod]
        [TestCategory("Intergration.UsersController")]
        public void GetIntergration()
        {
            var oUnitOfWork = new UnitOfWork(new EFDBContextContainer());
            IRepository<long, Users> oRepository = oUnitOfWork.Repository<long, Users>();


            int count = oRepository.Entities.Count();

            Assert.IsTrue(count >= 0);
        }


        [TestMethod]
        [TestCategory("Intergration.UsersController")]
        public async Task AddAndDelete()
        {
            PostDBUser();
            await base.AddAndDelete(CreateValidatedEntity, TestEditEntity);
            DeleteDBUser();

        }

        private void PostDBUser()
        {
            throw new NotImplementedException();
        }

        private void DeleteDBUser()
        {
            throw new NotImplementedException();
        }


        [TestMethod]
        [TestCategory("Intergration.UsersController")]
        public async Task AddUpdateDeleted()
        {
            await base.AddUpdateDeleted(CreateValidatedEntity, EditEntity, TestEditEntity);
        }

        #endregion

        public Users CreateEntity(long IdParam)
        {
            Users oUsers = new Users()
            {
                Id = IdParam,
                AspNetUserId = GuidTest
            };
            return oUsers;

        }


        public Users CreateValidatedEntity(long IdParam)
        {

            var oUnitOfWork = new UnitOfWork(new EFDBContextContainer());
            IRepository<long, Users> oUsersRepository = oUnitOfWork.Repository<long, Users>();

            Users oUsers = new Users()
            {
                Id = IdParam,
                AspNetUserId = GuidTest
            };

            return oUsers;

        }

        public Users EditEntity(Users oUsers)
        {

            oUsers.AspNetUserId = GuidTest;
            return oUsers;
        }

        public bool TestEditEntity(Users oUsers)
        {
            bool bResult = oUsers.AspNetUserId == GuidTest;
            return bResult;

        }

    }
}
