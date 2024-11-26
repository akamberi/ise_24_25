namespace CarRental.DAL.Persistence.Entities;

public class CarFeature : BaseEntity
{
    public CarFeatureType Type { get; set; }
    public int CarId { get; set; }
    internal Car Car { get; set; }
    public string Value { get; set; }
}
