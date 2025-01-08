using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ISEPay.Common.Enums;

namespace ISEPay.DAL.Persistence.Entities
{
    public class User : BaseEntity<Guid>
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [MaxLength(10)]
        public string? Gender { get; set; }
        public string? CardID { get; set; }

        [MaxLength(50)]
        public string? Nationality { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        [MaxLength(50)]
        public string? BirthCity { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [EmailAddress, Required]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        public UserStatus Status { get; set; } = UserStatus.PENDING;

        public Guid AdressID { get; set; }
        internal Address Address { get; set; }

        public Guid RoleID { get; set; }
        internal Role Role { get; set; }

        // New collection for Accounts
       public ICollection<Account> Accounts { get; set; }// A user can have many accounts
    }
}
