using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AssignmentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AssignmentAPI.Data
{
    public class ApplicationRepository<TEntity, TType> : IRepository<TEntity, TType> where TEntity : BaseEntity<TType>
    {
        public DbContext Context { get; }

        public ApplicationRepository(DbContext context)
        {
            Context = context ?? throw new ArgumentException(nameof(context));
        }

        public Response<TEntity> Delete(TType key)
        {
            if (key == null) throw new ArgumentException(nameof(key));
            var res = new Response<TEntity>();
            try
            {
                var item = Context.Set<TEntity>().Find(key);
                //Context.Set<TEntity>().Remove(item);
                item.IsDeleted = true;
                Context.Set<TEntity>().Update(item);
                res.Susscess = Context.SaveChanges() > 0;
                return res;
            }
            catch (Exception e)
            {
                res.ErrorMessage = e.InnerException == null ? e.Message : e.Message + e.InnerException.Message;
                res.IsException = true;
                res.Susscess = false;
                return res;
            }
        }

        public Response<TEntity> Delete(ICollection<TType> keys)
        {
            if (keys == null) throw new ArgumentException(nameof(keys));
            var res = new Response<TEntity>();
            try
            {
                foreach (var key in keys)
                {
                    var item = Context.Set<TEntity>().Find(key);

                    //if (item != null) Context.Set<TEntity>().Remove(item);
                    if (item == null) continue;
                    item.IsDeleted = true;
                    Context.Set<TEntity>().Update(item);
                }
                res.Susscess = Context.SaveChanges() > 0;
                return res;
            }
            catch (Exception e)
            {
                res.ErrorMessage = e.InnerException == null ? e.Message : e.Message + e.InnerException.Message;
                res.IsException = true;
                res.Susscess = false;
                return res;
            }

        }

        public async Task<Response<TEntity>> DeleteAsync(TType key)
        {
            if (key == null) throw new ArgumentException(nameof(key));
            var res = new Response<TEntity>();
            try
            {
                var item = Context.Set<TEntity>().Find(key);
                //                if (item != null) Context.Set<TEntity>().Remove(item);
                if (item != null)
                {
                    item.IsDeleted = true;
                    Context.Set<TEntity>().Update(item);
                }
                res.Susscess = await Context.SaveChangesAsync() > 0;
                return res;
            }
            catch (Exception e)
            {
                res.ErrorMessage = e.InnerException == null ? e.Message : e.Message + e.InnerException.Message;
                res.IsException = true;
                res.Susscess = false;
                return res;
            }
        }

        public async Task<Response<TEntity>> DeleteAsync(ICollection<TType> keys)
        {
            if (keys == null) throw new ArgumentException(nameof(keys));
            var res = new Response<TEntity>();
            try
            {
                foreach (var key in keys)
                {
                    var item = Context.Set<TEntity>().Find(key);
                    //                if (item != null) Context.Set<TEntity>().Remove(item);
                    if (item == null) continue;
                    item.IsDeleted = true;
                    Context.Set<TEntity>().Update(item);
                }
                res.Susscess = await Context.SaveChangesAsync() > 0;

                return res;
            }
            catch (Exception e)
            {
                res.ErrorMessage = e.InnerException == null ? e.Message : e.Message + e.InnerException.Message;
                res.IsException = true;
                res.Susscess = false;
                return res;
            }
        }

        public TEntity GetById(TType id)
        {
            if (id == null) throw new ArgumentException(nameof(id));
            return Context.Set<TEntity>().AsNoTracking().FirstOrDefault(s => s.Id.Equals(id) && !s.IsDeleted);
        }

        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            var items = await Context.Set<TEntity>().Where(s => !s.IsDeleted).ToListAsync();
            return items;
        }

        public async Task<TEntity> GetByIdAsync(TType id)
        {
            if (id == null) throw new ArgumentException(nameof(id));
            return await Task.Run(() => Context.Set<TEntity>().AsNoTracking().FirstOrDefault(s => s.Id.Equals(id)));
        }

        public Response<TEntity> Insert(TEntity item)
        {
            if (item == null) throw new ArgumentException(nameof(item));
            var res = new Response<TEntity>();
            try
            {
                // var curItem = _context.At
                Context.Set<TEntity>().Add(item);
                res.Susscess = Context.SaveChanges() > 0;
                return res;
            }
            catch (Exception e)
            {
                res.ErrorMessage = e.InnerException == null ? e.Message : e.Message + e.InnerException.Message;
                res.IsException = true;
                res.Susscess = false;
                return res;
            }
        }

        public ICollection<TEntity> GetAll()
        {
            return Context.Set<TEntity>().Where(s => !s.IsDeleted).ToList();
        }

        public Response<TEntity> Insert(ICollection<TEntity> items)
        {
            if (items == null) throw new ArgumentException(nameof(items));
            var res = new Response<TEntity>();
            try
            {
                foreach (var item in items)
                {
                    Context.Set<TEntity>().Add(item);
                }
                res.Susscess = Context.SaveChanges() > 0;
                return res;
            }
            catch (Exception e)
            {
                res.ErrorMessage = e.InnerException == null ? e.Message : e.Message + e.InnerException.Message;
                res.IsException = true;
                res.Susscess = false;
                return res;
            }

        }

        public async Task<Response<TEntity>> InsertAsync(TEntity item)
        {
            if (item == null) throw new ArgumentException(nameof(item));
            var res = new Response<TEntity>();
            try
            {
                // var curItem = _context.At
                var resItem = Context.Set<TEntity>().Add(item);
                res.Payload = resItem.Entity;
                res.Susscess = await Context.SaveChangesAsync() > 0;
                return res;
            }
            catch (Exception e)
            {
                res.ErrorMessage = e.InnerException == null ? e.Message : e.Message + e.InnerException.Message;
                res.IsException = true;
                res.Susscess = false;
                return res;
            }
        }

        public async Task<Response<TEntity>> InsertAsync(ICollection<TEntity> items)
        {
            if (items == null) throw new ArgumentException(nameof(items));
            var res = new Response<TEntity>();
            try
            {
                foreach (var item in items)
                {
                    Context.Set<TEntity>().Add(item);
                }
                res.Susscess = await Context.SaveChangesAsync() > 0;
                return res;
            }
            catch (Exception e)
            {
                res.ErrorMessage = e.InnerException == null ? e.Message : e.Message + e.InnerException.Message;
                res.IsException = true;
                res.Susscess = false;
                return res;
            }
        }

        public async Task<ICollection<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> express, int pageSize = 20, int pageIndex = 1)
        {
            if (pageSize < 0) throw new ArgumentException(nameof(pageSize));
            if (pageIndex < 0) throw new ArgumentException(nameof(pageIndex));
            return await Task.Run(() => Context.Set<TEntity>().Where(express).Skip(pageIndex * pageSize).Take(pageSize).AsNoTracking().ToList());
        }

        public async Task<ICollection<TEntity>> SearchAsync(SearchRequest<TEntity> searchRequest)
        {
            if (searchRequest == null) throw new ArgumentException(nameof(searchRequest));
            if (searchRequest.PageSize < 0) throw new ArgumentException(nameof(searchRequest.PageSize));
            if (searchRequest.PageIndex < 0) throw new ArgumentException(nameof(searchRequest.PageIndex));
            if (searchRequest.Expression == null) throw new ArgumentException(nameof(searchRequest.Expression));
            return await Task.Run(() => Context.Set<TEntity>().Where(searchRequest.Expression).Skip(searchRequest.PageIndex * searchRequest.PageSize).Take(searchRequest.PageSize).AsNoTracking().ToList());
        }

        public Response<TEntity> Update(TEntity item)
        {
            if (item == null) throw new ArgumentException(nameof(item));
            var res = new Response<TEntity>();
            try
            {
                var oldItem = Context.Set<TEntity>().Find(item.Id);
                oldItem.CopyAll(item);
                Context.Set<TEntity>().Update(oldItem);
                res.Susscess = Context.SaveChanges() > 0;
                return res;
            }
            catch (Exception e)
            {
                res.ErrorMessage = e.InnerException == null ? e.Message : e.Message + e.InnerException.Message;
                res.IsException = true;
                res.Susscess = false;
                return res;
            }
        }

        public Response<TEntity> Update(ICollection<TEntity> items)
        {
            if (items == null) throw new ArgumentException(nameof(items));
            var res = new Response<TEntity>();
            try
            {
                foreach (var item in items)
                {
                    var oldItem = Context.Set<TEntity>().Find(item.Id);
                    oldItem.CopyAll(item);
                    Context.Set<TEntity>().Update(oldItem);
                }
                res.Susscess = Context.SaveChanges() > 0;
                return res;
            }
            catch (Exception e)
            {
                res.ErrorMessage = e.InnerException == null ? e.Message : e.Message + e.InnerException.Message;
                res.IsException = true;
                res.Susscess = false;
                return res;
            }
        }

        public async Task<Response<TEntity>> UpdateAsync(TEntity item)
        {
            if (item == null) throw new ArgumentException(nameof(item));
            var res = new Response<TEntity>();
            try
            {
                var oldItem = Context.Set<TEntity>().Find(item.Id);
                oldItem.CopyAll(item);
                Context.Entry(oldItem).State = EntityState.Modified;
                Context.Set<TEntity>().Update(oldItem);
                res.Susscess = await Context.SaveChangesAsync() > 0;
                return res;
            }
            catch (Exception e)
            {
                res.ErrorMessage = e.InnerException == null ? e.Message : e.Message + e.InnerException.Message;
                res.IsException = true;
                res.Susscess = false;
                return res;
            }
        }

        public async Task<Response<TEntity>> UpdateAsync(ICollection<TEntity> items)
        {
            if (items == null) throw new ArgumentException(nameof(items));
            var res = new Response<TEntity>();
            try
            {
                foreach (var item in items)
                {
                    var oldItem = Context.Set<TEntity>().Find(item.Id);
                    oldItem.CopyAll(item);
                    Context.Set<TEntity>().Update(oldItem);
                }
                res.Susscess = await Context.SaveChangesAsync() > 0;
                return res;
            }
            catch (Exception e)
            {
                res.ErrorMessage = e.InnerException == null ? e.Message : e.Message + e.InnerException.Message;
                res.IsException = true;
                res.Susscess = false;
                return res;
            }
        }

        ICollection<TEntity> IRepository<TEntity, TType>.Search(Expression<Func<TEntity, bool>> express, int pageSize, int pageIndex)
        {
            if (pageSize < 0) throw new ArgumentException(nameof(pageSize));
            if (pageIndex < 0) throw new ArgumentException(nameof(pageIndex));
            return Context.Set<TEntity>().Where(express).Skip(pageIndex * pageSize).Take(pageSize).AsNoTracking().ToList();
        }

        ICollection<TEntity> IRepository<TEntity, TType>.Search(SearchRequest<TEntity> searchRequest)
        {
            if (searchRequest == null) throw new ArgumentException(nameof(searchRequest));
            if (searchRequest.PageSize < 0) throw new ArgumentException(nameof(searchRequest.PageSize));
            if (searchRequest.PageIndex < 0) throw new ArgumentException(nameof(searchRequest.PageIndex));
            if (searchRequest.Expression == null) throw new ArgumentException(nameof(searchRequest.Expression));
            return Context.Set<TEntity>().Where(searchRequest.Expression).Skip(searchRequest.PageIndex * searchRequest.PageSize).Take(searchRequest.PageSize).AsNoTracking().ToList();
        }
    }

    public class ApplicationRepository<TEntity> : ApplicationRepository<TEntity, int> where TEntity : BaseEntity
    {
        public ApplicationRepository(DbContext context) : base(context)
        {
        }
    }
}
