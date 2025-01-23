using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEPay.DAL.Persistence.Entities
{
    public class Image : BaseEntity<Guid>
    {

        public string ImageName {  get; set; }
        public string ImageUrl { get; set; }
        public string ImageType { get; set; }

        public Guid UserId { get; set; }        // Foreign key for the related user
        public User User { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
