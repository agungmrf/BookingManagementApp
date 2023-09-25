namespace API.Models;

public class Employee : BaseEntity
{
    public string Nik { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public int Gender { get; set; }
    public DateTime HiringDate { get; set; }
    public string Email { get; set; } 
    public string PhoneNumber { get; set; }
}