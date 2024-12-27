using ISEPay.DAL.Persistence.Entities;

namespace ISEPay.DAL.Persistence.Repositories
{
    public interface IFriendsRepository : _IBaseRepository<Friend, Guid>
    {
        // You can add methods specific to FriendsRepository here if needed
    }

    internal class FriendsRepository : _BaseRepository<Friend, Guid>, IFriendsRepository
    {

        public FriendsRepository(ISEPayDBContext dbContext) : base(dbContext)
        {
        }
      
    }
}
