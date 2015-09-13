using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlaBlaRunProject.DataAccess.Abstract
{
    public class Repository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : class, IIdentityKey<TKey>
    {
        private readonly DbContext context;
        private IDbSet<TEntity> entities;
        string errorMessage = string.Empty;

        public Repository(DbContext context)
        {
            this.context = context;
        }

        public TEntity GetById(TKey id)
        {
            return this.Entities.Find(id);
        }

        public void Insert(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.Entities.Add(entity);
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        public void Update(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                if (this.context.Entry(entity).State == EntityState.Detached)
                {
                    var attached = this.entities.Find(entity.Id);
                    if (attached != null)
                    {
                        var attachedEntry = this.context.Entry(attached);
                        attachedEntry.CurrentValues.SetValues(entity);
                    }
                    else
                        this.context.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    this.context.Entry(entity).State = EntityState.Modified;   
                }
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                throw new Exception(errorMessage, dbEx);
            }
        }

        public void Delete(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.Entities.Remove(entity);
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await Task.Run(() => this.Entities.Find(id));
        }

        public async Task InsertAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.Entities.Add(entity);
                await Task.Run(() => this.context.SaveChanges());
            }
            catch (DbEntityValidationException dbEx)
            {

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        public async Task UpdateAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }
                if (this.context.Entry(entity).State == EntityState.Detached)
                {
                    var attached = this.entities.Find(entity.Id);
                    if (attached != null)
                    {
                        var attachedEntry = this.context.Entry(attached);
                        attachedEntry.CurrentValues.SetValues(entity);
                    }
                    else
                        this.context.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    this.context.Entry(entity).State = EntityState.Modified;
                }
                await Task.Run(() => this.context.SaveChanges());
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                throw new Exception(errorMessage, dbEx);
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.Entities.Remove(entity);
                await Task.Run(() => this.context.SaveChanges());
            }
            catch (DbEntityValidationException dbEx)
            {

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        public IDbSet<TEntity> Entities
        {
            get
            {
                if (entities == null)
                {
                    entities = context.Set<TEntity>();
                }
                return entities;
            }
        }

        public Task<IDbSet<TEntity>> EntitiesAsync
        {
            get
            {
                if (entities == null)
                {
                    entities = context.Set<TEntity>();
                }
                return Task.Run(() => entities);
            }
        }
    }

}
