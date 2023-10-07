using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Utilities.Handlers;
using API.Contracts;
using Microsoft.IdentityModel.Tokens;

namespace API.Utilities.Handlers;

public class GenerateTokenHandler : IGenerateTokenHandler 
{
    private readonly IConfiguration _configuration;

    public GenerateTokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Generate(IEnumerable<Claim> claims)
    {
        // Membuat kunci rahasia simetris berdasarkan nilai yang tersimpan dalam konfigurasi
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtService:SecretKey"]));
        
        // Membuat objek SigningCredentials yang digunakan untuk menandatangani token
        var sigingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        
        // Membuat token JWT dengan konfigurasi yang diberikan, termasuk claims
        var tokenOptions = new JwtSecurityToken(issuer: _configuration["JWTService:Issuer"], // Penerbit token (dalam hal ini, server)
            audience: _configuration["JWTService:Audience"], // Penerima token (dalam hal ini, aplikasi yang menggunakan token)
            claims: claims, // Data claims yang akan disertakan dalam token
            expires: DateTime.Now.AddMinutes(10), // Token akan kedaluwarsa dalam 10 menit
            signingCredentials: sigingCredentials); // Kunci untuk penerbit token
        
        // Encode token JWT ke dalam string yang dapat dikirimkan sebagai respons
        var encodedtoken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return encodedtoken;
    }
}