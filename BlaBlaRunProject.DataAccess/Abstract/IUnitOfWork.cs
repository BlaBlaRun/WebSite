﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaBlaRunProject.DataAccess.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        IRepository<TKey, TEntity> Repository<TKey, TEntity>() where TEntity : class, IIdentityKey<TKey>;
    }
}
