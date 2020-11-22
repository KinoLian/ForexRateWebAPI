using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using ForexRateWebAPI.Web.Models.Interface;

namespace ForexRateWebAPI.Web.Models.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext _context { get; set; }

        public GenericRepository() : this(new ForexRateDBEntities()) { }

        public GenericRepository(DbContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            this._context = context;
        }

        public GenericRepository(ObjectContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            this._context = new DbContext(context, true);
        }


        public void Create(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Set<TEntity>().Add(instance);
                this.SaveChanges();
            }

        }

        public void Update(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Entry(instance).State = EntityState.Modified;
                this.SaveChanges();
            }
        }


        public void Delete(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this._context.Entry(instance).State = EntityState.Deleted;
                this.SaveChanges();
            }
        }


        public TEntity Get(params object[] keyValues)
        {
            return this._context.Set<TEntity>().Find(keyValues);
        }

        public IQueryable<TEntity> GetAll()
        {
            return this._context.Set<TEntity>().AsQueryable();
        }


        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        public bool Exists(params object[] keyValues)
        {
            TEntity t =this._context.Set<TEntity>().Find(keyValues);
            if (t == null) return false; else return true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._context != null)
                {
                    this._context.Dispose();
                    this._context = null;
                }
            }
        }
    }
}
