
namespace BusPortal.DAL.Persistence.Entities
{
    public class BaseEntity
    {
    }

    public class BaseEntity<T> : BaseEntity
    {
        public T Id { get; set; }
    }
}
