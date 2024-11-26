namespace CarRental.DAL.Persistence.Entities;

public class Media : BaseEntity<int>
{
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public string FilePath { get; set; }
}
