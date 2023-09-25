namespace API.Models;

public class Education : BaseEntity
{
    public string Major { get; set; }
    public string Degree { get; set; }
    public float Gpa { get; set; }
    public Guid UniversityGuid { get; set; } // Foreign Key.
}