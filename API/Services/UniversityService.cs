using API.Contracts;
using API.DTOs.Universities;

namespace API.Services;

public class UniversityService
{
    private readonly IUniversityRepository _universityRepository;

    public UniversityService(IUniversityRepository universityRepository)
    {
        _universityRepository = universityRepository;
    }

    public IEnumerable<UniversityDto> GetAll()
    {
        var entity = _universityRepository.GetAll(); // Mengambil semua entitas university dari repositori
        if (!entity.Any()) // Memeriksa apakah tidak ada entitas yang ditemukan
            return Enumerable.Empty<UniversityDto>(); // Mengembalikan koleksi kosong dari UniversityDto

        var universityDto = new List<UniversityDto>(); // Membuat list universityDto
        foreach (var university in entity) // Melakukan loop melalui entitas university
            universityDto.Add((UniversityDto)university); // Menambahkan entitas university ke universityDto

        return universityDto; // Mengembalikan universityDto
    }

    public UniversityDto? GetByGuid(Guid guid)
    {
        var entity = _universityRepository.GetByGuid(guid); // Mengambil entitas university berdasarkan GUID
        if (entity is null) // Memeriksa apakah entitas null
            return null; // Mengembalikan null jika tidak ada entitas yang ditemukan

        return (UniversityDto)entity; // Mengembalikan entitas university sebagai UniversityDto
    }

    public UniversityDto? Create(CreateUniversityDto createUniversityDto)
    {
        var entity = _universityRepository.Create(createUniversityDto); // Membuat entitas university baru
        if (entity is null) // Memeriksa apakah entitas null
            return null; // Mengembalikan null jika tidak ada entitas yang ditemukan

        return (UniversityDto)entity; // Mengembalikan entitas university sebagai UniversityDto
    }
}