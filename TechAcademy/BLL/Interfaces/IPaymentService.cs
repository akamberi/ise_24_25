using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTOs;

namespace BLL.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResultDto> ProcessPaymentAsync(string userId, int courseId, decimal amount);
        Task<bool> VerifyPaymentAsync(string transactionId);
    }
}
