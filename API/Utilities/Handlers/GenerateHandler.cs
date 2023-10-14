namespace API.Utilities.Handler;

public class GenerateHandler // Class untuk menghandle generate NIK baru
{
    public static string Nik(string? nik = null)
    {
        if (nik is null) // Jika NIK terakhir belum ada, maka NIK baru akan dimulai dari 111111
            return "111111";

        var generateNik = int.Parse(nik) + 1; // Jika NIK terakhir sudah ada, maka NIK baru akan ditambahkan 1

        return generateNik.ToString(); // Mengembalikan NIK baru dalam bentuk string
    }
}