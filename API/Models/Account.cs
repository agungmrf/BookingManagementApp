namespace API.Models;

public class Account : BaseEntity
{
    public string Password { get; set; }
    public bool IsDeleted { get; set; }
    public int Otp { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpiredDate { get; set; }
}