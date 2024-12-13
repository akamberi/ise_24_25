using CarRental.Common.Enums;

namespace CarRental.DAL.Persistence.Entities;

public class CarImage : BaseEntity
{
    public int CarId { get; set; }
    internal Car Car { get; set; }
    public int MediaId { get; set; }
    internal Media Media { get; set; }
    public MediaType MediaType { get; set; }
}
