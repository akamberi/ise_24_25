using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Models.DTO;

public class CarBrandDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Thumbnail { get; set; }
}

public class CarBrandAddDTO : IValidatableObject
{
    [Required(ErrorMessage = "Vendos emrin")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Gjatesia e emrit duhet te jete midis 3 dhe 50 karaktere")]
    [DisplayName("Shkruaj brand e makines")]
    public string Name { get; set; }
    public IFormFile Image { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (this.Name.StartsWith("Afrim"))
            yield return new ValidationResult("Emri nuk duhet te filloje me germe te vogel");
    }
}

public class CarBrandEditDTO
{
    [Required(ErrorMessage = "Ndrysho Emrin")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Gjatesia e emrit duhet te jete midis 3 dhe 50 karaktere")]
    [DisplayName("Shkruaj emrin e brandit")]
    public string Name { get; set; }
    public string Thumbnail { get; set; }
}
