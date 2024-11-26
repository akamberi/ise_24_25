namespace CarRental.DAL.Persistence.Entities;

public enum FuelType : byte
{
    DIESEL = 1,
    PETROL = 2,
    ELECTRIC = 3,
    HYBRID = 4,
    LPG = 5
}

public enum TransmissionType : byte
{
    MANUAL = 1,
    AUTOMATIC = 2,
    SEMI_AUTOMATIC = 3,
    TRIPTRONIC = 4
}

public enum CarFeatureType : byte
{
    COLOR,
    ENGINE,
    NO_OF_DOORS,
    NO_OF_SEATS,
}

public enum MediaType : byte
{
    THUMBNAIL = 1,
    MAIN_SLIDER = 2
}

public class Car : BaseEntity<int>
{
    public string Name { get; set; }
    public int Year { get; set; }
    public FuelType FuelType { get; set; }
    public TransmissionType TransmissionType { get; set; }
    public int Mileage { get; set; }
    public string VIN { get; set; }
    public int BrandId { get; set; }
    internal CarBrand Brand { get; set; }
}
