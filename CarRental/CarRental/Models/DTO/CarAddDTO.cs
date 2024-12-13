using CarRental.Common.Enums;

namespace CarRental.Models.DTO;

public class CarAddDTO
{
    public string Name { get; set; }
    public FuelType FuelType { get; set; }
    public int BrandId { get; set; }
}
