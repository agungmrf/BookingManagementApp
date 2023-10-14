using API.Models;

namespace API.DTOs.Universities;

public class UniversityDto
{
    public Guid Guid { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

    public static explicit operator
        UniversityDto(University university) // Operator explicit untuk mengkonversi University menjadi UniversityDto.
    {
        return new UniversityDto // Mengembalikan object UniversityDto dengan data dari property University.
        {
            Guid = university.Guid,
            Code = university.Code,
            Name = university.Name
        };
    }

    public static implicit operator University(UniversityDto universityDto)
    {
        return new University
        {
            Guid = universityDto.Guid,
            Code = universityDto.Code,
            Name = universityDto.Name,
            ModifiedDate = DateTime.Now
        };
    }
}