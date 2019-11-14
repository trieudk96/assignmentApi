using System.Reflection;

namespace AssignmentAPI.Models
{
    public abstract class BaseEntity<TType>
    {
        public TType Id { get; set; }
        //public string CreatedBy { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public string UpdatedBy { get; set; }
        //public DateTime? UpdatedDate { get; set; }
        //public int? Status { get; set; }
        public bool IsDeleted { get; set; }
        //public string Deleted { get; set; }
        public virtual void Copy<TEntity>(TEntity item) where TEntity : BaseEntity<TType>
        {
            //CreatedDate = item.CreatedDate;
            //CreatedBy = item.CreatedBy;
            //UpdatedDate = item.UpdatedDate;
            //UpdatedBy = item.UpdatedBy;
            //Status = item.Status;
            //IsDeleted = IsDeleted;
        }
        public void CopyAll<TEntity>(TEntity item, string ignors = "Id") where TEntity : BaseEntity<TType>
        {
            var type = typeof(TEntity);

            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                if (ignors.Contains(prop.Name)) continue;
                type.GetProperty(prop.Name)?.SetValue(this, prop.GetValue(item));
            }
        }
    }
    public class BaseEntity : BaseEntity<int> { }
}
