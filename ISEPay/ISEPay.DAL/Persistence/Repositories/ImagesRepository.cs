using ISEPay.DAL.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEPay.DAL.Persistence.Repositories
{

    public interface _ImagesRepository : _IBaseRepository<Image, Guid>
    {
        // You can add methods specific to FriendsRepository here if needed
    }
    internal class ImagesRepository : _BaseRepository<Image, Guid>, _ImagesRepository
    {

        public ImagesRepository(ISEPayDBContext dbContext) : base(dbContext)
        {

        }
    }
}
