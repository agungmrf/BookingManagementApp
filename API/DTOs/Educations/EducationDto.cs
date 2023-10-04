using API.Models;

namespace API.DTOs.Educations;

public class EducationDto
{
    public Guid Guid { get; set; }
    public string Major { get; set; }
    public string Degree { get; set; }
    public float Gpa { get; set; }
    public Guid UniversityGuid { get; set; }
    
    public static explicit operator EducationDto(Education education) // Operator explicit untuk mengkonversi Education menjadi EducationDto.
    {
        return new EducationDto // Mengembalikan object EducationDto dengan data dari property Education.
        {
            Guid = education.Guid,
            Major = education.Major,
            Degree = education.Degree,
            Gpa = education.Gpa,
            UniversityGuid = education.UniversityGuid
        };
    }
    
    public static implicit operator Education(EducationDto educationDto) // Operator implicit untuk mengkonversi EducationDto menjadi Education.
    {
        return new Education // Mengembalikan object Education dengan data dari property EducationDto.
        {
            Guid = educationDto.Guid,
            Major = educationDto.Major,
            Degree = educationDto.Degree,
            Gpa = educationDto.Gpa,
            UniversityGuid = educationDto.UniversityGuid,
            ModifiedDate = DateTime.Now
        };
    }
}