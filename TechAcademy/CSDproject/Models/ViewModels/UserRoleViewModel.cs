using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSDproject.Models.ViewModels
{
    public class UserRoleViewModel
    {
        [Required(ErrorMessage = "User ID is required.")]
        [RegularExpression(@"^[a-fA-F0-9]{24}$", ErrorMessage = "Invalid User ID format.")] // Adjust if using GUID
        public string UserId { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "At least one role is required.")]
        [MinLength(1, ErrorMessage = "User must have at least one role.")]
        public List<string> Roles { get; set; } = new List<string>();

        public bool IsAdminExists { get; set; } // No validation needed since it's a boolean flag
    }
}

