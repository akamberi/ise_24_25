using System;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp; // Ensure this is included
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using ISEPay.BLL.ISEPay.Domain.Models;

namespace ISEPay.BLL.Services.Scoped
{

    public interface IOtpService
    {
        string GenerateOTP();
        void SendOTPSMS(string phoneNumber, string otp);
        void SendOTPEmail(string emailAddress, string otp);

        bool ValidateOTP(string otp);


    }
    public class OTPService : IOtpService
    {
        private readonly IConfiguration _configuration;

        // Store OTP and its metadata (this could be moved to a database or more persistent storage if needed)
        private static string _generatedOtp;
        private static DateTime _otpTimestamp;
        private static int _attempts;

        public int RetryLimit { get; set; } = 3;  // Maximum allowed attempts

        public OTPService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        // Generate OTP
        public string GenerateOTP()
        {
            var random = new Random();
            _generatedOtp = random.Next(100000, 999999).ToString(); // Generate a 6-digit OTP
            _otpTimestamp = DateTime.Now; // Set timestamp when OTP is generated
            _attempts = 0; // Reset attempts when generating a new OTP
            return _generatedOtp;
        }

        // Send OTP via SMS
        public void SendOTPSMS(string phoneNumber, string otp)
        {
            try
            {
                var accountSid = _configuration["Twilio:AccountSid"];
                var authToken = _configuration["Twilio:AuthToken"];
                var fromPhoneNumber = _configuration["Twilio:FromPhoneNumber"];

                if (string.IsNullOrEmpty(accountSid) || string.IsNullOrEmpty(authToken) || string.IsNullOrEmpty(fromPhoneNumber))
                {
                    throw new InvalidOperationException("Twilio configuration values are missing.");
                }

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: $"Your OTP code is: {otp}",
                    from: new Twilio.Types.PhoneNumber(fromPhoneNumber),
                    to: new Twilio.Types.PhoneNumber(phoneNumber)
                );

                Console.WriteLine($"SMS sent: {message.Sid}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send SMS: {ex.Message}");
            }
        }

        // Send OTP via Email
        public void SendOTPEmail(string emailAddress, string otp)
        {
            try
            {
                var smtpServer = _configuration["Email:SmtpServer"];
                var smtpPort = int.Parse(_configuration["Email:SmtpPort"]);
                var senderEmail = _configuration["Email:SenderEmail"];
                var senderPassword = _configuration["Email:SenderPassword"];

                if (string.IsNullOrEmpty(smtpServer) || string.IsNullOrEmpty(senderEmail) || string.IsNullOrEmpty(senderPassword))
                {
                    throw new InvalidOperationException("Email configuration values are missing.");
                }

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("OTP Service", senderEmail));
                message.To.Add(new MailboxAddress("", emailAddress));
                message.Subject = "Your OTP Code";
                message.Body = new TextPart("plain")
                {
                    Text = $"Your OTP code is: {otp}"
                };

                using (var client = new MailKit.Net.Smtp.SmtpClient()) // Fully qualified name
                {
                    client.Connect(smtpServer, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate(senderEmail, senderPassword);
                    client.Send(message);
                    client.Disconnect(true);
                }

                Console.WriteLine($"Email sent to: {emailAddress}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send Email: {ex.Message}");
            }
        }

        // Validate OTP
        public bool ValidateOTP(string inputOtp)
        {
            if (_generatedOtp == null || _otpTimestamp == DateTime.MinValue)
            {
                Console.WriteLine("OTP has not been generated yet.");
                return false;
            }

            // Check if the OTP has expired (e.g., 5 minutes validity)
            var expiryTime = TimeSpan.FromMinutes(5); // Expiry time set to 5 minutes
            if ((DateTime.Now - _otpTimestamp) > expiryTime)
            {
                Console.WriteLine("OTP has expired.");
                return false;
            }

            // Check if the OTP matches
            if (_generatedOtp == inputOtp)
            {
                Console.WriteLine("OTP is valid.");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid OTP.");
                _attempts++;
                return false;
            }
        }

        // Reset OTP
        public void ResetOTP()
        {
            _generatedOtp = null;
            _otpTimestamp = DateTime.MinValue;
            _attempts = 0;
        }
    }
}
