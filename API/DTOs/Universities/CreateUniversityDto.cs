using API.Models;

namespace API.DTOs.Universities;

public class CreateUniversityDto
{
    public string Code { get; set; }
    public string Name { get; set; }

    public static implicit operator University(CreateUniversityDto createUniversityDto)
    {
        return new University
        {
            Code = createUniversityDto.Code,
            Name = createUniversityDto.Name,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }
}