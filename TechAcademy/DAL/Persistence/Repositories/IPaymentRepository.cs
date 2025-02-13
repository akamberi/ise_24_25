using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Persistence.Entities;

namespace DAL.Persistence.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> GetPaymentByIdAsync(int id);
        Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(string userId);
        Task<Payment> ProcessPaymentAsync(Payment payment);
        Task<Payment> GetPaymentByTransactionIdAsync(string transactionId); // New Method
    }
}
