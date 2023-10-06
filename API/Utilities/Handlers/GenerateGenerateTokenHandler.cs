using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Utilities.Handlers;
using API.Contracts;
using Microsoft.IdentityModel.Tokens;

namespace API.Utilities.Handlers;

public class GenerateGenerateTokenHandler : IGenerateTokenHandler
{
    private readonly IConfiguration _configuration;

    public GenerateGenerateTokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Generate(IEnumerable<Claim> claims)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtService:SecretKey"]));
        var sigingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokenOptions = new JwtSecurityToken(issuer: _configuration["JWTService:Issuer"],
            audience: _configuration["JWTService:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: sigingCredentials); 
        
        var encodedtoken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        
        return encodedtoken;
    }
}