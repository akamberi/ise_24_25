
using System.ComponentModel.DataAnnotations;
using ISEPay.Common;
using ISEPay.Common.Enums;

namespace ISEPay.DAL.Persistence.Entities
{
    public class Friend : BaseEntity<Guid>
    {
        //[Required]
        public Guid UserId { get; set; }

        internal User User { get; set; }

       // [Required]
        public Guid FriendId { get; set; }

        internal User FriendUser { get; set; }

        [Required]
        public FriendStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }

  
}
