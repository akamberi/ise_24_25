namespace CSDproject.Models.ViewModels
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; } // Ensure this matches
        public bool IsAdminExists { get; set; } // Track if Admin exists
    }
}

