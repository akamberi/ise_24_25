using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusPortal.DAL.Persistence.Entities;

namespace BusPortal.DAL.Persistence.Repositories
{
    public interface IPaymentRepository : _IBaseRepository<Payment, Guid>
    {
    }
    internal class PaymentRepository : _BaseRepository<Payment, Guid>, IPaymentRepository
    {
        public PaymentRepository(DALDbContext dbContext) : base(dbContext)
        {
        }
    }
}
