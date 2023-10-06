using System.Security.Claims;

namespace API.Contracts;

public interface IGenerateTokenHandler
{
    string Generate(IEnumerable<Claim> claims);
}