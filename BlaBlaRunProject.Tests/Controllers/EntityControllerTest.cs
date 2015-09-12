using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlaBlaRunProject.Domain.Abstract;
using BlaBlaRunProject.Tests.Common;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using BlaBlaRunProject.WebUI.Controllers;

namespace BlaBlaRunProject.Tests.Controllers
{
    public class EntityControllerTest<TEntity, TController, TIdentityType> 
        where TEntity : class, IIdentityKey<TIdentityType>
        where TController : Controller, IFMCController<TEntity, TIdentityType>
        where TIdentityType : struct,
          IComparable,
          IComparable<TIdentityType>,
          IConvertible,
          IEquatable<TIdentityType>,
          IFormattable
    {
        public delegate void ParamsFunc(TEntity oEntity);


        #region Mock
        public async void Index(Func<List<TEntity>> SetupList)
        {
            var lEntities = SetupList();
            var oRepository = GenericMethods.SetupRepository<TEntity, IRepository<TIdentityType, TEntity>, TIdentityType>(lEntities);
            var oUnitOfWork = GenericMethods.SetupUnitOfWork<TIdentityType, TEntity, IRepository<TIdentityType, TEntity>>(oRepository);


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
            var oRepository = GenericMethods.SetupRepository<TEntity, IRepository<TIdentityType, TEntity>, TIdentityType>(lEntities);
            var oUnitOfWork = GenericMethods.SetupUnitOfWork<TIdentityType, TEntity, IRepository<TIdentityType, TEntity>>(oRepository);

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
            var oRepository = GenericMethods.SetupRepository<TEntity, IRepository<TIdentityType, TEntity>, TIdentityType>(lEntities);
            var oUnitOfWork = GenericMethods.SetupUnitOfWork<TIdentityType, TEntity, IRepository<TIdentityType, TEntity>>(oRepository);

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
            var oRepository = GenericMethods.SetupRepository<TEntity, IRepository<TIdentityType, TEntity>, TIdentityType>(lEntities);
            var oUnitOfWork = GenericMethods.SetupUnitOfWork<TIdentityType, TEntity, IRepository<TIdentityType, TEntity>>(oRepository);
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
            var oRepository = GenericMethods.SetupRepository<TEntity, IRepository<TIdentityType, TEntity>, TIdentityType>(lEntities);
            var oUnitOfWork = GenericMethods.SetupUnitOfWork<TIdentityType, TEntity, IRepository<TIdentityType, TEntity>>(oRepository);
            var oEntity = oRepository.Object.Entities.FirstOrDefault();
            var oEntityLastOldId = oEntity.Id;

            // Arrange
            TController controller = (TController)Activator.CreateInstance(typeof(TController), oUnitOfWork.Object);

            TestEditEntity(oEntity);

            // Act
            ViewResult result = await controller.Edit(oEntity) as ViewResult;


            // Assert
            Assert.AreEqual(oEntityLastOldId, oEntity.Id);
        }

        #endregion


        #region DB
        public async void IndexWithDB()
        {
            //var unitOfWork = new Moq.Mock<IUnitOfWork>();
            //IRepository<int,Vessel> oRepository = unitOfWork.Object.Repository<int,Vessel>();
            var oUnitOfWork = new UnitOfWork();
            IRepository<TIdentityType, TEntity> oRepository = oUnitOfWork.Repository<TIdentityType, TEntity>();

            var iEntitiesCount = oRepository.Entities.Count();

            // Arrange
            TController controller = (TController)Activator.CreateInstance(typeof(TController), oUnitOfWork);

            // Act
            ViewResult result = await controller.Index() as ViewResult;
            List<TEntity> lEntitiesController = result.Model as List<TEntity>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(iEntitiesCount, lEntitiesController.Count);

        }

        public async void DetailsWithDB()
        {
            var oUnitOfWork = new UnitOfWork();
            IRepository<TIdentityType, TEntity> oRepository = oUnitOfWork.Repository<TIdentityType, TEntity>();
            var oEntity = oRepository.Entities.FirstOrDefault();

            // Arrange
            TController controller = (TController)Activator.CreateInstance(typeof(TController), oUnitOfWork);

            // Act
            ViewResult result = await controller.Details(oEntity.Id) as ViewResult;


            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(oEntity, result.Model);
        }

        public async void CreateWithDB(Func<TEntity> TestCreator)
        {
            var oUnitOfWork = new UnitOfWork();
            IRepository<TIdentityType, TEntity> oRepository = oUnitOfWork.Repository<TIdentityType, TEntity>();
            var lEntities = oRepository.Entities.ToList();
            TEntity oEntityLastOld = lEntities.OrderByDescending(x => x.Id).FirstOrDefault();
            //TEntity oEntityLastOld = oRepository.Entities.OrderByDescending(x => x.Id).FirstOrDefault();

            // Arrange
            TController controller = (TController)Activator.CreateInstance(typeof(TController), oUnitOfWork);

            TEntity oEntity = TestCreator();

            // Act
            ViewResult result = await controller.Create(oEntity) as ViewResult;


            // Assert
            Assert.IsNotNull(result);
            dynamic z = oEntityLastOld.Id;
            Assert.AreEqual(z + 1, oEntity.Id);
            Assert.AreEqual(oEntity, result.Model);
        }

        public async void EditWithDB()
        {
            var oUnitOfWork = new UnitOfWork();
            IRepository<TIdentityType, TEntity> oRepository = oUnitOfWork.Repository<TIdentityType, TEntity>();
            var oEntity = oRepository.Entities.FirstOrDefault();

            // Arrange
            TController controller = (TController)Activator.CreateInstance(typeof(TController), oUnitOfWork);

            // Act
            ViewResult result = await controller.Edit(oEntity.Id) as ViewResult;


            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(oEntity, result.Model);
        }

        public async void EditWithPushDB(ParamsFunc TestEditEntity)
        {
            var oUnitOfWork = new UnitOfWork();
            IRepository<TIdentityType, TEntity> oRepository = oUnitOfWork.Repository<TIdentityType, TEntity>();
            var oEntity = oRepository.Entities.FirstOrDefault();
            var oEntityLastOldId = oEntity.Id;

            // Arrange
            TController controller = (TController)Activator.CreateInstance(typeof(TController), oUnitOfWork);

            TestEditEntity(oEntity);

            // Act
            ViewResult result = await controller.Edit(oEntity) as ViewResult;


            // Assert
            Assert.AreEqual(oEntityLastOldId, oEntity.Id);
        }


        #endregion
    }
}
