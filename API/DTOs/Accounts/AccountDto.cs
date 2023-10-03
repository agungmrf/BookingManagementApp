using API.Models;

namespace API.DTOs.Accounts;

public class AccountDto
{
    public Guid Guid { get; set; }
    public int Otp { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpiredDate { get; set; }
    
    public static explicit operator AccountDto(Account account)
    {
        return new AccountDto
        {
            Guid = account.Guid,
            Otp = account.Otp,
            IsUsed = account.IsUsed,
            ExpiredDate = account.ExpiredDate
        };
    }
    
    public static implicit operator Account(AccountDto accountDto)
    {
        return new Account
        {
            Guid = accountDto.Guid,
            Otp = accountDto.Otp,
            IsUsed = accountDto.IsUsed,
            ExpiredDate = accountDto.ExpiredDate,
            ModifiedDate = DateTime.Now
        };
    }
}