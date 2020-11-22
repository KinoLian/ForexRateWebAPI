using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ForexRateWebAPI.Web.Models.Interface
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Create(TEntity instance);

        void Update(TEntity instance);

        void Delete(TEntity instance);

        TEntity Get(params object[] keyValues);

        IQueryable<TEntity> GetAll();

        void SaveChanges();

        bool Exists(params object[] keyValues);
    }
}