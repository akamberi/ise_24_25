using System;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Persistence.Entities;
using DAL.Persistence.Repositories;
using Microsoft.Extensions.Logging;
using Common.DTOs;
using Microsoft.Extensions.Options;
using BLL.Services.BLL.Services;

namespace BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly PayPalService _payPalService;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<PaymentService> _logger;
        private readonly PayPalSettings _payPalSettings;

        public PaymentService(PayPalService payPalService, IPaymentRepository paymentRepository, ILogger<PaymentService> logger, IOptions<PayPalSettings> payPalSettings)
        {
            _payPalService = payPalService;
            _paymentRepository = paymentRepository;
            _logger = logger;
            _payPalSettings = payPalSettings.Value;
        }

        public async Task<PaymentResultDto> ProcessPaymentAsync(string userId, int courseId, decimal amount)
        {
            try
            {
                var paymentResult = await _payPalService.ProcessPaymentAsync(
                    amount,
                    _payPalSettings.ReturnUrl,
                    _payPalSettings.CancelUrl
                );

                if (!paymentResult.IsSuccessful)
                {
                    return new PaymentResultDto
                    {
                        IsSuccessful = false,
                        Message = "Payment failed.",
                        RedirectUrl = _payPalSettings.CancelUrl
                    };
                }

                var payment = new Payment
                {
                    UserId = userId,
                    CourseId = courseId,
                    Amount = amount,
                    PaymentDate = DateTime.UtcNow,
                    PaymentMethod = "PayPal",
                    IsSuccessful = true,
                    TransactionId = paymentResult.PaymentId
                };

                await _paymentRepository.ProcessPaymentAsync(payment);

                return new PaymentResultDto
                {
                    IsSuccessful = true,
                    TransactionId = payment.TransactionId,
                    Message = "Payment processed successfully.",
                    RedirectUrl = _payPalSettings.ReturnUrl
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing payment: {ex.Message}");
                return new PaymentResultDto
                {
                    IsSuccessful = false,
                    Message = "An error occurred while processing payment.",
                    RedirectUrl = _payPalSettings.CancelUrl
                };
            }
        }

        public async Task<bool> VerifyPaymentAsync(string transactionId)
        {
            var payment = await _paymentRepository.GetPaymentByTransactionIdAsync(transactionId);

            if (payment == null || !payment.IsSuccessful)
            {
                _logger.LogWarning($"Payment verification failed for Transaction ID: {transactionId}");
                return false;
            }

            _logger.LogInformation($"Payment verified successfully for Transaction ID: {transactionId}");
            return true;
        }

    }
}
