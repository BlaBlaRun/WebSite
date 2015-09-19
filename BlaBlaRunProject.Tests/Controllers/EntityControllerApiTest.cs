using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using BlaBlaRunProject.Controllers;
using BlaBlaRunProject.DataAccess.Abstract;
using BlaBlaRunProject.Domain.Concrete;
using BlaBlaRunProject.Tests.Common;
using System.Net.Http;
using BlaBlaRunProject.WebUI.Controllers;
using System.Web.Http;
using System.Threading.Tasks;

namespace BlaBlaRunProject.Tests.Controllers
{
    public class EntityControllerApiTest<TKey, TEntity, TController>
        where TKey : struct, IComparable, IComparable<TKey>, IFormattable, IConvertible, IEquatable<TKey>
        where TEntity : class, IIdentityKey<TKey>
        where TController : ApiController, IApiController<TKey, TEntity>
    {
        public delegate void ParamsFunc(TEntity oEntity);


        #region Mock
        public void Get(Func<List<TEntity>> SetupList)
        {
            var lEntities = SetupList();
            var oRepository = GenericMethods.SetupRepository<TKey, TEntity, IRepository<TKey, TEntity>>(lEntities);
            var oUnitOfWork = GenericMethods.SetupUnitOfWork<TKey, TEntity, IRepository<TKey, TEntity>>(oRepository);


            var iEntitiesCount = oRepository.Object.Entities.Count();

            // Arrange
            TController controller = (TController)Activator.CreateInstance(typeof(TController), oUnitOfWork.Object);

            // Act
            IQueryable<TEntity> result = controller.Get() as IQueryable<TEntity>;
            List<TEntity> lEntitiesController = result.ToList() as List<TEntity>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(iEntitiesCount, lEntitiesController.Count);
        }
        

        public async void GetById(Func<List<TEntity>> SetupList)
        {
            var lEntities = SetupList();
            var oRepository = GenericMethods.SetupRepository<TKey, TEntity, IRepository<TKey, TEntity>>(lEntities);
            var oUnitOfWork = GenericMethods.SetupUnitOfWork<TKey, TEntity, IRepository<TKey, TEntity>>(oRepository);
            
            var oEntity = oRepository.Object.Entities.FirstOrDefault();

            // Arrange
            TController controller = (TController)Activator.CreateInstance(typeof(TController), oUnitOfWork.Object);

            // Act
            IHttpActionResult result = await controller.Get(oEntity.Id);

            // Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual(oEntity, result.ToString);
        }

        public async Task Post(Func<List<TEntity>> SetupList, Func<TKey, TEntity> CreateEntity, Func<TEntity, bool> TestEditEntity)
        {
            var lEntities = SetupList();
            var oRepository = GenericMethods.SetupRepository<TKey, TEntity, IRepository<TKey, TEntity>>(lEntities);
            var oUnitOfWork = GenericMethods.SetupUnitOfWork<TKey, TEntity, IRepository<TKey, TEntity>>(oRepository);

            var iEntitiesCount = oRepository.Object.Entities.Count();
            var oEntityLastOld = oRepository.Object.Entities.OrderByDescending(x => x.Id).FirstOrDefault();

            // Arrange
            TController controller = (TController)Activator.CreateInstance(typeof(TController), oUnitOfWork.Object);

            dynamic z = iEntitiesCount;
            TEntity oEntity = CreateEntity(z);

            // Act
            IHttpActionResult result = await controller.Post(oEntity) as IHttpActionResult;
            var oEntityNew = oRepository.Object.Entities.OrderByDescending(x => x.Id).FirstOrDefault();

            // Assert            
            //HttpResponseMessage response2 = result;// await result.ExecuteAsync();
            //if (response2.IsSuccessStatusCode)
            //{
            //    TEntity resultEntity = await response2.Content.ReadAsAsync<TEntity>();

            //}
            dynamic oEntityLastOldId = oEntityLastOld.Id;

            Assert.AreEqual(oEntityLastOldId + 1, oEntity.Id);
            Assert.AreEqual(iEntitiesCount + 1, oRepository.Object.Entities.Count());
            Assert.IsTrue(TestEditEntity(oEntityNew));
        }

        public async Task Put(Func<List<TEntity>> SetupList, Func<TEntity, TEntity> EditEntity, Func<TEntity, bool> TestEditEntity)
        {
            var lEntities = SetupList();
            var oRepository = GenericMethods.SetupRepository<TKey, TEntity, IRepository<TKey, TEntity>>(lEntities);
            var oUnitOfWork = GenericMethods.SetupUnitOfWork<TKey, TEntity, IRepository<TKey, TEntity>>(oRepository);
            var oEntity = oRepository.Object.Entities.FirstOrDefault();
            var oEntityId = oEntity.Id;

            // Arrange
            TController controller = (TController)Activator.CreateInstance(typeof(TController), oUnitOfWork.Object);

            HttpConfiguration configuration = new HttpConfiguration();
            HttpRequestMessage request = new HttpRequestMessage();
            controller.Request = request;
            controller.Request.Properties["MS_HttpConfiguration"] = configuration;

            var oEntityDelta = EditEntity(oEntity);

            // Act
            IHttpActionResult result = await controller.Put(oEntity.Id, oEntityDelta) as IHttpActionResult;
            var oEntityNew = oRepository.Object.Entities.FirstOrDefault();

            // Assert
            Assert.AreEqual(oEntityId, oEntityNew.Id);
            Assert.IsTrue(TestEditEntity(oEntityNew));

        }
        

        public async Task Delete(Func<List<TEntity>> SetupList)
        {
            var lEntities = SetupList();
            var oRepository = GenericMethods.SetupRepository<TKey, TEntity, IRepository<TKey, TEntity>>(lEntities);
            var oUnitOfWork = GenericMethods.SetupUnitOfWork<TKey, TEntity, IRepository<TKey, TEntity>>(oRepository);

            var iEntitiesCount = oRepository.Object.Entities.Count();
            var oEntityLastOld = oRepository.Object.Entities.OrderByDescending(x => x.Id).FirstOrDefault();

            var oEntityLastOldId = oEntityLastOld.Id;

            // Arrange
            TController controller = (TController)Activator.CreateInstance(typeof(TController), oUnitOfWork.Object);


            // Act
            IHttpActionResult result = await controller.Delete(oEntityLastOld.Id) as IHttpActionResult;


            // Assert
            Assert.AreEqual(iEntitiesCount - 1, oRepository.Object.Entities.Count());
        }



        #endregion


        #region DB
        public void GetDB()
        {
            var oUnitOfWork = new UnitOfWork(new EFDBContextContainer());
            IRepository<TKey, TEntity> oRepository = oUnitOfWork.Repository<TKey, TEntity>();

            var iEntitiesCount = oRepository.Entities.Count();

            // Arrange
            TController controller = (TController)Activator.CreateInstance(typeof(TController), oUnitOfWork);

            // Act
            IQueryable<TEntity> result = controller.Get() as IQueryable<TEntity>;
            List<TEntity> lEntitiesController = result.ToList() as List<TEntity>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(iEntitiesCount, lEntitiesController.Count);

        }
        


        #endregion
    }
}
