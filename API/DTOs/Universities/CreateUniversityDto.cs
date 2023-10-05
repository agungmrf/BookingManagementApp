using API.Models;

namespace API.DTOs.Universities;

public class CreateUniversityDto
{
    public string Code { get; set; }
    public string Name { get; set; }

    public static implicit operator University(CreateUniversityDto createUniversityDto) // Operator implicit untuk mengkonversi CreateUniversityDto menjadi University.
    {
        return new University // Mengembalikan object University dengan data dari property CreateUniversityDto.
        {
            Guid = new Guid(),
            Code = createUniversityDto.Code,
            Name = createUniversityDto.Name,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }
}