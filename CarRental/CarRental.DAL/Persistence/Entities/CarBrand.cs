using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.DAL.Persistence.Entities;

public class CarBrand : BaseEntity<int>
{
    public string Name { get; set; }
    public string Logo { get; set; }
}
