using CarRental.Common.Enums;

namespace CarRental.DAL.Persistence.Entities;

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
