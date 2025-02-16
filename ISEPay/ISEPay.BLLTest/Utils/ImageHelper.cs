using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ISEPay.BLL.Utils
{

    public static class ImageHelper
    {
        private static readonly string _secretKey = "Kj8yVn$3bP!qzD6mXfL9cG#Wt7@Np2Yh"; // Change this to a secure key

        public static string SaveImageToFile(string base64String, string fileExtension)
        {
            // Ensure file extension is formatted correctly
            fileExtension = fileExtension.TrimStart('.'); // Remove dot if present

            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedImages");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Generate secure filename
            string fileName = GenerateSecureFileName() + "." + fileExtension;
            string filePath = Path.Combine(directoryPath, fileName);

            try
            {
                // Convert Base64 to Image File
                byte[] imageBytes = Convert.FromBase64String(base64String);
                File.WriteAllBytes(filePath, imageBytes);

                // Return **relative** path instead of absolute path
                return Path.Combine("UploadedImages", fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving image: {ex.Message}");
                return null;
            }
        }


        private static string GenerateSecureFileName()
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString() + _secretKey));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
