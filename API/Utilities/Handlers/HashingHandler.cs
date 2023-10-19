

namespace API.Utilities.Handler;

public class HashingHandler
{
    private static string GetRandomSalt() // Method untuk mengembalikan string random salt.
    {
        return BCrypt.Net.BCrypt.GenerateSalt(12); // Default salt 12
    }

    public static string HashPassword(string password) // Method untuk mengembalikan string password yang sudah di hash.
    {
        return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
    }

    public static bool VerifyPassword(string password, string hashedPassword) // Method untuk memverifikasi password.
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}