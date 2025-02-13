using PayPal.Api;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Persistence.Entities;
using BLL.Services.BLL.Services;
using Payment = PayPal.Api.Payment;

namespace BLL.Services
{
    public class PayPalService
    {
        private readonly PayPalSettings _payPalSettings;

        public PayPalService(IOptions<PayPalSettings> payPalSettings)
        {
            _payPalSettings = payPalSettings.Value;
        }

        private APIContext GetAPIContext()
        {
            var config = new Dictionary<string, string>
            {
                { "mode", _payPalSettings.Mode },
                { "clientId", _payPalSettings.ClientId },
                { "clientSecret", _payPalSettings.ClientSecret }
            };

            var apiContext = new APIContext(new OAuthTokenCredential(config).GetAccessToken())
            {
                Config = config
            };

            return apiContext;
        }

        public Payment CreatePayment(decimal amount, string currency, string returnUrl, string cancelUrl)
        {
            var payment = new Payment
            {
                intent = "sale",
                payer = new Payer { payment_method = "paypal" },
                transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        description = "Course Enrollment Payment",
                        amount = new Amount
                        {
                            currency = currency,
                            total = amount.ToString("0.00")
                        },
                        item_list = new ItemList
                        {
                            items = new List<Item>
                            {
                                new Item
                                {
                                    name = "Course Enrollment",
                                    currency = currency,
                                    price = amount.ToString("0.00"),
                                    quantity = "1"
                                }
                            }
                        }
                    }
                },
                redirect_urls = new RedirectUrls
                {
                    return_url = returnUrl,
                    cancel_url = cancelUrl
                }
            };

            var apiContext = GetAPIContext();
            return payment.Create(apiContext);
        }

        public Payment ExecutePayment(string paymentId, string payerId)
        {
            var apiContext = GetAPIContext();
            var payment = Payment.Get(apiContext, paymentId);
            return payment.Execute(apiContext, new PaymentExecution { payer_id = payerId });
        }

        public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, string returnUrl, string cancelUrl)
        {
            var payment = CreatePayment(amount, "USD", returnUrl, cancelUrl);
            if (payment.state.ToLower() == "created")
            {
                return new PaymentResult
                {
                    IsSuccessful = true,
                    PaymentId = payment.id,
                    RedirectUrl = payment.links.Find(l => l.rel == "approval_url")?.href
                };
            }
            return new PaymentResult { IsSuccessful = false };
        }
    }

    public class PaymentResult
    {
        public bool IsSuccessful { get; set; }
        public string PaymentId { get; set; }
        public string RedirectUrl { get; set; }
    }
}
