using API.Models;

namespace API.DTOs.Accounts;

public class AccountDto
{
    public Guid Guid { get; set; }
    public string Password { get; set; }
    public int Otp { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpiredDate { get; set; }

    public static explicit operator
        AccountDto(Account account) // Operator explicit untuk mengkonversi Account menjadi AccountDto.
    {
        return new AccountDto // Mengembalikan object AccountDto dengan data dari property Account.
        {
            Guid = account.Guid,
            Password = account.Password,
            Otp = account.Otp,
            IsUsed = account.IsUsed,
            ExpiredDate = account.ExpiredDate
        };
    }

    public static implicit operator
        Account(AccountDto accountDto) // Operator implicit untuk mengkonversi AccountDto menjadi Account.
    {
        return new Account // Mengembalikan object Account dengan data dari property AccountDto.
        {
            Guid = accountDto.Guid,
            Password = accountDto.Password,
            Otp = accountDto.Otp,
            IsUsed = accountDto.IsUsed,
            ExpiredDate = accountDto.ExpiredDate,
            ModifiedDate = DateTime.Now
        };
    }
}