using ISEPay.BLL.Services.Scoped;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ISEPay.Controllers
{
    [ApiController]
    [Route("otp/")]
    [Authorize(Policy = "Public")]
    public class OTPController : ControllerBase
    {
        private readonly IOtpService _otpService;

        public OTPController(IOtpService otpService)
        {
            _otpService = otpService;
        }

        [HttpPost("send")]
        public IActionResult SendOTP([FromQuery] string email, [FromQuery] string phoneNumber)
        {
            var otp = _otpService.GenerateOTP();

            if (!string.IsNullOrEmpty(email))
            {
                _otpService.SendOTPEmail(email, otp);
            }

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                _otpService.SendOTPSMS(phoneNumber, otp);
            }

            return Ok(new { Message = "OTP sent successfully!" });
        }
        [HttpPost("validate")]
        public IActionResult ValidateOTP([FromQuery] string inputOtp)
        {

            var isValid = _otpService.ValidateOTP(inputOtp);

            if (!isValid)
            {
                return BadRequest(new { Message = "Invalid or expired OTP." });
            }

            return Ok(new { Message = "OTP validated successfully!" });
        }
    }

}
