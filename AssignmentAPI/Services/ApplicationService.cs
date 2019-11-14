using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AssignmentAPI.Data;
using AssignmentAPI.Models; 
using TOS.API.Services;

namespace AssignmentAPI.Services
{
    public class ApplicationService<TEntity, TType> : IApplicationService<TEntity, TType> where TEntity : BaseEntity<TType>
    {
        protected readonly IRepository<TEntity, TType> _repository;

        public ApplicationService(IRepository<TEntity, TType> repository)
        {
            _repository = repository;
        }
        public Response<TEntity> Insert(TEntity item)
        {
            return _repository.Insert(item);
        }

        public Response<TEntity> Update(TEntity item)
        {
            return _repository.Update(item);
        }

        public Response<TEntity> Delete(TType key)
        {
            return _repository.Delete(key);
        }

        public Response<TEntity> Insert(ICollection<TEntity> items)
        {
            return _repository.Insert(items);
        }

        public Response<TEntity> Update(ICollection<TEntity> items)
        {
            return _repository.Update(items);
        }

        public Response<TEntity> Delete(ICollection<TType> keys)
        {
            return _repository.Delete(keys);
        }

        public ICollection<TEntity> Search(Expression<Func<TEntity, bool>> express, int pageSize = 20, int pageIndex = 1)
        {
            return _repository.Search(express, pageSize, pageIndex);
        }

        public ICollection<TEntity> Search(SearchRequest<TEntity> searchRequest)
        {
            return _repository.Search(searchRequest);
        }

        public ICollection<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public TEntity GetById(TType id)
        {
            return _repository.GetById(id);
        }

        public Task<Response<TEntity>> InsertAsync(TEntity item)
        {
            return _repository.InsertAsync(item);
        }

        public Task<Response<TEntity>> UpdateAsync(TEntity item)
        {
            return _repository.UpdateAsync(item);
        }

        public Task<Response<TEntity>> DeleteAsync(TType key)
        {
            return _repository.DeleteAsync(key);
        }

        public Task<Response<TEntity>> InsertAsync(ICollection<TEntity> items)
        {
            return _repository.InsertAsync(items);
        }

        public Task<Response<TEntity>> UpdateAsync(ICollection<TEntity> items)
        {
            return _repository.UpdateAsync(items);
        }

        public Task<Response<TEntity>> DeleteAsync(ICollection<TType> keys)
        {
            return _repository.DeleteAsync(keys);
        }

        public Task<ICollection<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> express, int pageSize = 20, int pageIndex = 1)
        {
            return _repository.SearchAsync(express, pageSize, pageIndex);
        }

        public Task<ICollection<TEntity>> SearchAsync(SearchRequest<TEntity> searchRequest)
        {
            return _repository.SearchAsync(searchRequest);
        }

        public Task<ICollection<TEntity>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<TEntity> GetByIdAsync(TType id)
        {
            return _repository.GetByIdAsync(id);
        }
    }

    public class ApplicationService<TEntity> : ApplicationService<TEntity, int> where TEntity : BaseEntity
    {
        public ApplicationService(IRepository<TEntity> repository) : base(repository)
        {
           
        }
    }
}
