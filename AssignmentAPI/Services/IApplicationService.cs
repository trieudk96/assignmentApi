using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AssignmentAPI.Models; 

namespace AssignmentAPI.Services
{
   public interface IApplicationService<TEntity,TType> where TEntity : BaseEntity<TType>
   {
       Response<TEntity> Insert(TEntity item);
       Response<TEntity> Update(TEntity item);
       Response<TEntity> Delete(TType key);
       Response<TEntity> Insert(ICollection<TEntity> items);
       Response<TEntity> Update(ICollection<TEntity> items);
       Response<TEntity> Delete(ICollection<TType> keys);
       ICollection<TEntity> Search(Expression<Func<TEntity, bool>> express, int pageSize = 20, int pageIndex = 1);
       ICollection<TEntity> Search(SearchRequest<TEntity> searchRequest);
       ICollection<TEntity> GetAll();
       TEntity GetById(TType id);

       Task<Response<TEntity>> InsertAsync(TEntity item);
       Task<Response<TEntity>> UpdateAsync(TEntity item);
       Task<Response<TEntity>> DeleteAsync(TType key);
       Task<Response<TEntity>> InsertAsync(ICollection<TEntity> items);
       Task<Response<TEntity>> UpdateAsync(ICollection<TEntity> items);
       Task<Response<TEntity>> DeleteAsync(ICollection<TType> keys);
       Task<ICollection<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> express, int pageSize = 20, int pageIndex = 1);
       Task<ICollection<TEntity>> SearchAsync(SearchRequest<TEntity> searchRequest);
       Task<ICollection<TEntity>> GetAllAsync();
       Task<TEntity> GetByIdAsync(TType id);
    }
   public interface IApplicationService<TEntity> :IApplicationService<TEntity,int> where TEntity : BaseEntity { }
}
