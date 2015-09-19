using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BlaBlaRunProject.DataAccess.Abstract;
using BlaBlaRunProject.Domain.Concrete;
using System.Threading.Tasks;

namespace BlaBlaRunProject.Tests.Common
{
    public class GenericMethods
    {

        public static List<Workouts> SetupWorkoutsList()
        {
            var oWorkout1 = new Workouts() { Id = 0 };
            var oWorkout2 = new Workouts() { Id = 1 };
            var oWorkout3 = new Workouts() { Id = 2 };
            var oWorkout4 = Activator.CreateInstance(typeof(Workouts));

            var lWorkouts = new List<Workouts>() { oWorkout1, oWorkout2, oWorkout3 };

            return lWorkouts;
        }

        public static Moq.Mock<IUnitOfWork> SetupUnitOfWork<TKey, TModel, TRepo>(Moq.Mock<TRepo> oRepository)
            where TModel : class, IIdentityKey<TKey>
            where TRepo : class, IRepository<TKey, TModel>
        {
            Moq.Mock<IUnitOfWork> unitofwork = new Moq.Mock<IUnitOfWork>();
            unitofwork.Setup(x => x.Repository<TKey, TModel>()).Returns(oRepository.Object);

            return unitofwork;
        }


        public static Moq.Mock<TRepo> SetupRepository<TKey, TModel, TRepo>(List<TModel> lModels)
            where TModel : class, IIdentityKey<TKey>
            where TRepo : class, IRepository<TKey, TModel>
            where TKey : struct,
              IComparable,
              IComparable<TKey>,
              IConvertible,
              IEquatable<TKey>,
              IFormattable
        {
            IQueryable<TModel> queryableList = lModels.AsQueryable();

            var mockSet = new Moq.Mock<IDbSet<TModel>>();
            mockSet.As<IQueryable<TModel>>().Setup(m => m.Provider).Returns(queryableList.Provider);
            mockSet.As<IQueryable<TModel>>().Setup(m => m.Expression).Returns(queryableList.Expression);
            mockSet.As<IQueryable<TModel>>().Setup(m => m.ElementType).Returns(queryableList.ElementType);
            mockSet.As<IQueryable<TModel>>().Setup(m => m.GetEnumerator()).Returns(queryableList.GetEnumerator());

            Moq.Mock<TRepo> repository = new Moq.Mock<TRepo>();
            repository.Setup(x => x.Entities).Returns(mockSet.Object);
            repository.Setup(x => x.GetById(Moq.It.IsAny<TKey>())).Returns((int i) => lModels.Find(x => x.Id.Equals(i)));
            repository.Setup(x => x.Insert(Moq.It.IsAny<TModel>())).Callback<TModel>(y =>
            {
                var item = lModels.OrderByDescending(x => x.Id).FirstOrDefault();
                if (item != null)
                {
                    dynamic z = item.Id;
                    y.Id = z + 1;
                    lModels.Add(y);
                }
            });
            repository.Setup(x => x.Update(Moq.It.IsAny<TModel>())).Callback<TModel>(y =>
            {
                var i = lModels.FindIndex(x => x.Id.Equals(y.Id));
                if (i != -1)
                {
                    lModels[i] = y;
                }
            });
            repository.Setup(x => x.Delete(Moq.It.IsAny<TModel>())).Callback<TModel>(y =>
            {
                var item = lModels.Where(x => x.Id.Equals(y.Id)).FirstOrDefault();
                if (item != null)
                {
                    lModels.Remove(item);
                }
            });
            repository.Setup(x => x.GetByIdAsync(Moq.It.IsAny<TKey>())).Returns((TKey i) => { return Task.FromResult(lModels.Find(x => x.Id.Equals(i))); });
            repository.Setup(x => x.InsertAsync(Moq.It.IsAny<TModel>())).Returns((TModel y) =>
            Task.Run(() =>
            {
                var item = lModels.OrderByDescending(x => x.Id).FirstOrDefault();
                if (item != null)
                {
                    dynamic z = item.Id;
                    y.Id = z + 1;
                    lModels.Add(y);
                }
            })
            );
            repository.Setup(x => x.UpdateAsync(Moq.It.IsAny<TModel>())).Returns((TModel y) =>
            Task.Run(() =>
            {
                var i = lModels.FindIndex(x => x.Id.Equals(y.Id));
                if (i != -1)
                {
                    lModels[i] = y;
                }
            })
            );
            repository.Setup(x => x.DeleteAsync(Moq.It.IsAny<TModel>())).Returns((TModel y) =>
            Task.Run(() =>
            {
                var item = lModels.Where(x => x.Id.Equals(y.Id)).FirstOrDefault();
                if (item != null)
                {
                    lModels.Remove(item);
                }
            })
            );



            return repository;
        }

        public class FactoryList<T>
        {
            private FactoryList() { }

            static readonly Dictionary<int, Type> _dict = new Dictionary<int, Type>();

            public static T Create(int id, params object[] args)
            {
                Type type = null;
                if (_dict.TryGetValue(id, out type))
                    return (T)Activator.CreateInstance(type, args);

                throw new ArgumentException("No type registered for this id");
            }

            public static void Register<Tderived>(int id) where Tderived : T
            {
                var type = typeof(Tderived);

                if (type.IsInterface || type.IsAbstract)
                    throw new ArgumentException("...");

                _dict.Add(id, type);
            }
        }
    }
}
