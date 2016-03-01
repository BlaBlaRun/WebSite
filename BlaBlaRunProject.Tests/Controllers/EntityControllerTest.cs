using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlaBlaRunProject.Tests.Common;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using BlaBlaRunProject.DataAccess.Abstract;
using BlaBlaRunProject.WebUI.Controllers.Interfaces;
using System.Threading.Tasks;
using BlaBlaRunProject.Domain.Concrete;
using System.Data.Entity;

namespace BlaBlaRunProject.Tests.Controllers
{
    public class EntityControllerTest<TKey, TEntity, TController>
        where TKey : struct, IComparable, IComparable<TKey>, IConvertible, IEquatable<TKey>, IFormattable
        where TEntity : class, IIdentityKey<TKey>
        where TController : Controller, IMVCController<TKey, TEntity>
    {
        public delegate void ParamsFunc(TEntity oEntity);


        #region Mock
        public async void Index(Func<List<TEntity>> SetupList)
        {
            var lEntities = SetupList();
            var oRepository = GenericMethods.SetupRepository<TKey, TEntity, IRepository<TKey, TEntity>>(lEntities);
            var oUnitOfWork = GenericMethods.SetupUnitOfWork<TKey, TEntity, IRepository<TKey, TEntity>>(oRepository);


            var iEntitiesCount = oRepository.Object.Entities.Count();

            // Arrange
            TController controller = (TController)Activator.CreateInstance(typeof(TController), oUnitOfWork.Object);

            // Act
            ViewResult result = await controller.Index() as ViewResult;
            List<TEntity> lEntitiesController= result.Model as List<TEntity>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(iEntitiesCount, lEntitiesController.Count);
        }

        
        public async void Details(Func<List<TEntity>> SetupList)
        {
            var lEntities = SetupList();
            var oRepository = GenericMethods.SetupRepository<TKey, TEntity, IRepository<TKey, TEntity>>(lEntities);
            var oUnitOfWork = GenericMethods.SetupUnitOfWork<TKey, TEntity, IRepository<TKey, TEntity>>(oRepository);

            var oEntity = oRepository.Object.Entities.FirstOrDefault();


            // Arrange
            TController controller = (TController)Activator.CreateInstance(typeof(TController), oUnitOfWork.Object);

            // Act
            ViewResult result = await controller.Details(oEntity.Id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(oEntity, result.Model);
        }

        public async void Create(Func<List<TEntity>> SetupList, Func<TEntity> TestCreator)
        {
            var lEntities = SetupList();
            var oRepository = GenericMethods.SetupRepository<TKey, TEntity, IRepository<TKey, TEntity>>(lEntities);
            var oUnitOfWork = GenericMethods.SetupUnitOfWork<TKey, TEntity, IRepository<TKey, TEntity>>(oRepository);

            var iEntitiesCount = oRepository.Object.Entities.Count();
            var oEntityLastOld = oRepository.Object.Entities.OrderByDescending(x => x.Id).FirstOrDefault();

            // Arrange
            TController controller = (TController)Activator.CreateInstance(typeof(TController), oUnitOfWork.Object);

            TEntity oEntity = TestCreator();
            
            // Act
            ViewResult result = await controller.Create(oEntity) as ViewResult;

            // Assert

            dynamic z = oEntityLastOld.Id;
            Assert.AreEqual(z + 1, oEntity.Id);
            Assert.AreEqual(iEntitiesCount +1 , oRepository.Object.Entities.Count());
        }

        public async void Edit(Func<List<TEntity>> SetupList)
        {
            var lEntities = SetupList();
            var oRepository = GenericMethods.SetupRepository<TKey, TEntity, IRepository<TKey, TEntity>>(lEntities);
            var oUnitOfWork = GenericMethods.SetupUnitOfWork<TKey, TEntity, IRepository<TKey, TEntity>>(oRepository);
            var oEntity = oRepository.Object.Entities.FirstOrDefault();

            // Arrange
            TController controller = (TController)Activator.CreateInstance(typeof(TController), oUnitOfWork.Object);

            // Act
            ViewResult result = await controller.Edit(oEntity.Id) as ViewResult;


            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(oEntity, result.Model);
        }

        public async void EditPost(Func<List<TEntity>> SetupList, ParamsFunc TestEditEntity)
        {
            var lEntities = SetupList();
            var oRepository = GenericMethods.SetupRepository<TKey, TEntity, IRepository<TKey, TEntity>>(lEntities);
            var oUnitOfWork = GenericMethods.SetupUnitOfWork<TKey, TEntity, IRepository<TKey, TEntity>>(oRepository);
            var oEntity = oRepository.Object.Entities.FirstOrDefault();
            var oEntityLastOldId = oEntity.Id;

            // Arrange
            TController controller = (TController)Activator.CreateInstance(typeof(TController), oUnitOfWork.Object);

            TestEditEntity(oEntity);

            // Act
            ViewResult result = await controller.Edit(oEntityLastOldId) as ViewResult;


            // Assert
            Assert.AreEqual(oEntityLastOldId, oEntity.Id);
        }

        #endregion


        #region DB

        public async Task IndexWithDB()
        {
            var oUnitOfWork = new UnitOfWork(new EFDBContextContainer());
            IRepository<TKey, TEntity> oRepository = oUnitOfWork.Repository<TKey, TEntity>();

            var iEntitiesCount = oRepository.Entities.Count();
            // Arrange
            TController controller = (TController)Activator.CreateInstance(typeof(TController), oUnitOfWork);

            // Act
            ViewResult result = await controller.Index() as ViewResult;
            //IDbSet<TEntity> lEntitiesController = result.Model as IDbSet<TEntity>;
            IQueryable<TEntity> lEntitiesController = result.Model as IQueryable<TEntity>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(iEntitiesCount, lEntitiesController.Count());
        }


        public async Task DetailsWithDB()
        {
            var oUnitOfWork = new UnitOfWork(new EFDBContextContainer());
            IRepository<TKey, TEntity> oRepository = oUnitOfWork.Repository<TKey, TEntity>();

            var oEntity = oRepository.Entities.FirstOrDefault();
            // Arrange
            TController controller = (TController)Activator.CreateInstance(typeof(TController), oUnitOfWork);

            // Act
            ViewResult result = await controller.Details(oEntity.Id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(oEntity, result.Model);
        }


        public async Task CreateWithDB()
        {
            await CreateWithDB(CreateEntity, TestCreateEntity);
        }


        public async Task CreateWithDB(Func<TKey, TEntity> CreateEntity, Func<TEntity, bool> TestCreateEntity)
        {
            var oUnitOfWork = new UnitOfWork(new EFDBContextContainer());
            IRepository<TKey, TEntity> oRepository = oUnitOfWork.Repository<TKey, TEntity>();

            var iEntitiesCount = oRepository.Entities.Count();
            var oEntityLastOld = oRepository.Entities.OrderByDescending(x => x.Id).FirstOrDefault();

            // Arrange
            TController controller = (TController)Activator.CreateInstance(typeof(TController), oUnitOfWork);

            dynamic z = iEntitiesCount;
            TEntity oEntity = CreateEntity(z);

            // Act
            ActionResult resultId = controller.Create() as ActionResult;
            Assert.IsNotNull(resultId);

            ActionResult result = await controller.Create(oEntity) as ActionResult;
            var oEntityNew = oRepository.Entities.OrderByDescending(x => x.Id).FirstOrDefault();

            // Assert            
            //HttpResponseMessage response2 = result;// await result.ExecuteAsync();
            //if (response2.IsSuccessStatusCode)
            //{
            //    TEntity resultEntity = await response2.Content.ReadAsAsync<TEntity>();

            //}

            dynamic oEntityLastOldId = -1;
            if (oEntityLastOld != null)
            {
                oEntityLastOldId = oEntityLastOld.Id;
            }

            Assert.IsNotNull(result);
            Assert.IsTrue(oEntityLastOldId < oEntityNew.Id);
            Assert.AreEqual(iEntitiesCount + 1, oRepository.Entities.Count());
            Assert.IsTrue(TestCreateEntity(oEntityNew));

        }

        public async Task EditWithDB()
        {
            await EditWithDB(EditEntity, TestEditEntity);
        }

        public async Task EditWithDB(Func<TEntity, TEntity> EditEntity, Func<TEntity, bool> TestEditEntity)
        {
            var oUnitOfWork = new UnitOfWork(new EFDBContextContainer());
            IRepository<TKey, TEntity> oRepository = oUnitOfWork.Repository<TKey, TEntity>();

            var oEntity = oRepository.Entities.FirstOrDefault();
            var oEntityLastOldId = oEntity.Id;

            // Arrange
            TController controller = (TController)Activator.CreateInstance(typeof(TController), oUnitOfWork);


            // Act
            ViewResult resultId = await controller.Edit(oEntityLastOldId) as ViewResult;

            Assert.IsNotNull(resultId);
            Assert.AreEqual(oEntity, resultId.Model);
            var oEntityDelta = EditEntity((TEntity)resultId.Model);

            ActionResult result = await controller.Edit(oEntityDelta) as ActionResult;
            var oEntityNew = oRepository.Entities.FirstOrDefault();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual(oEntityLastOldId, oEntity.Id);
            Assert.AreEqual(oEntityDelta, oEntityNew);
            Assert.IsTrue(TestEditEntity(oEntityNew));
        }


        public async Task DeleteWithDB()
        {
            var oUnitOfWork = new UnitOfWork(new EFDBContextContainer());
            IRepository<TKey, TEntity> oRepository = oUnitOfWork.Repository<TKey, TEntity>();

            var iEntitiesCount = oRepository.Entities.Count();
            var oEntityLastOld = oRepository.Entities.OrderByDescending(x => x.Id).FirstOrDefault();

            var oEntityLastOldId = oEntityLastOld.Id;

            // Arrange
            TController controller = (TController)Activator.CreateInstance(typeof(TController), oUnitOfWork);


            // Act
            ViewResult resultId = await controller.Delete(oEntityLastOldId) as ViewResult;

            Assert.IsNotNull(resultId);
            Assert.AreEqual(oEntityLastOld, resultId.Model);

            ActionResult result = await controller.DeleteConfirmed(((TEntity)resultId.Model).Id) as ActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual(iEntitiesCount - 1, oRepository.Entities.Count());
        }

        #endregion



        #region DBById


        public async Task DetailsWithDB(TKey id)
        {
            var oUnitOfWork = new UnitOfWork(new EFDBContextContainer());
            IRepository<TKey, TEntity> oRepository = oUnitOfWork.Repository<TKey, TEntity>();

            var oEntity = oRepository.Entities.Where(x => x.Id.Equals(id)).FirstOrDefault();
            // Arrange
            TController controller = (TController)Activator.CreateInstance(typeof(TController), oUnitOfWork);

            // Act
            ViewResult result = await controller.Details(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(oEntity, result.Model);
        }


        public async Task EditWithDB(TKey id, Func<TEntity, TEntity> EditEntity, Func<TEntity, bool> TestEditEntity)
        {
            var oUnitOfWork = new UnitOfWork(new EFDBContextContainer());
            IRepository<TKey, TEntity> oRepository = oUnitOfWork.Repository<TKey, TEntity>();

            var oEntity = oRepository.Entities.Where(x => x.Id.Equals(id)).FirstOrDefault();
            var oEntityLastOldId = oEntity.Id;

            // Arrange
            TController controller = (TController)Activator.CreateInstance(typeof(TController), oUnitOfWork);


            // Act
            ViewResult resultId = await controller.Edit(id) as ViewResult;

            Assert.IsNotNull(resultId);
            Assert.AreEqual(oEntity, resultId.Model);
            var oEntityDelta = EditEntity((TEntity)resultId.Model);

            ActionResult result = await controller.Edit(oEntityDelta) as ActionResult;
            var oEntityNew = oRepository.Entities.Where(x => x.Id.Equals(id)).FirstOrDefault();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual(oEntityLastOldId, oEntity.Id);
            Assert.AreEqual(oEntityDelta, oEntityNew);
            Assert.IsTrue(TestEditEntity(oEntityNew));
        }


        public async Task DeleteWithDB(TKey id)
        {
            var oUnitOfWork = new UnitOfWork(new EFDBContextContainer());
            IRepository<TKey, TEntity> oRepository = oUnitOfWork.Repository<TKey, TEntity>();

            var iEntitiesCount = oRepository.Entities.Count();
            var oEntityLastOld = oRepository.Entities.Where(x => x.Id.Equals(id)).FirstOrDefault();

            var oEntityLastOldId = oEntityLastOld.Id;

            // Arrange
            TController controller = (TController)Activator.CreateInstance(typeof(TController), oUnitOfWork);


            // Act
            ViewResult resultId = await controller.Delete(id) as ViewResult;

            Assert.IsNotNull(resultId);
            Assert.AreEqual(oEntityLastOld, resultId.Model);

            ActionResult result = await controller.DeleteConfirmed(((TEntity)resultId.Model).Id) as ActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual(iEntitiesCount - 1, oRepository.Entities.Count());
        }

        #endregion



        public virtual TEntity CreateEntity(TKey IdParam)
        {
            TEntity oTEntityUsers = null;
            return oTEntityUsers;

        }


        public virtual bool TestCreateEntity(TEntity oTEntity)
        {
            bool bResult = false;
            return bResult;

        }

        public virtual TEntity EditEntity(TEntity oTEntity)
        {
            return oTEntity;
        }

        public virtual bool TestEditEntity(TEntity oTEntity)
        {
            bool bResult = false;
            return bResult;

        }


    }
}
