using System.Net;
using API.Contracts;
using API.DTOs.Employees;
using API.Models;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] // Untuk menunjukkan bahwa ini adalah controller API
[Route("api/[controller]")] // Untuk menunjukkan route dari controller ini
public class EmployeeController : ControllerBase // ControllerBase untuk controller tanpa view
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IUniversityRepository _universityRepository;

    public EmployeeController(IEmployeeRepository employeeRepository, IEducationRepository educationRepository, IUniversityRepository universityRepository)
    {
        _employeeRepository = employeeRepository;
        _educationRepository = educationRepository;
        _universityRepository = universityRepository;
    }

    [HttpGet("details")]
    public IActionResult GetDetails()
    {
        var employees = _employeeRepository.GetAll();
        var educations = _educationRepository.GetAll();
        var universities = _universityRepository.GetAll();

        if (!(employees.Any() && educations.Any() && universities.Any()))
        {
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        }

        var employeeDetails = from emp in employees
            join edu in educations on emp.Guid equals edu.Guid
            join unv in universities on edu.UniversityGuid equals unv.Guid
            select new EmployeeDetailDto
            {
                Guid = emp.Guid,
                Nik = emp.Nik,
                FullName = string.Concat(emp.FirstName, " ", emp.LastName),
                BirthDate = emp.BirthDate,
                Gender = emp.Gender.ToString(),
                HiringDate = emp.HiringDate,
                Email = emp.Email,
                PhoneNumber = emp.PhoneNumber,
                Major = edu.Major,
                Degree = edu.Degree,
                Gpa = edu.Gpa,
                University = unv.Name
            };
        
        return Ok(new ResponseOKHandler<IEnumerable<EmployeeDetailDto>>(employeeDetails));
    }
    
    // Untuk menangani request GET dengan route api/[controller]
    [HttpGet]
    public IActionResult GetAll()
    {
        // Mengambil semua data dari database.
        var result = _employeeRepository.GetAll();
        if (!result.Any())
        {
            // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        }
        // Mengubah IEnumerable<Employee> menjadi IEnumerable<EmployeeDto>.
        var data = result.Select(x => (EmployeeDto)x);
        
        // Jika ada data, maka akan mengembalikan response 200 OK.
        return Ok(new ResponseOKHandler<IEnumerable<EmployeeDto>>(data));
    }
    
    // Untuk menangani request GET dengan route api/[controller]/guid
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        // Mengambil data dari database berdasarkan guid.
        var result = _employeeRepository.GetByGuid(guid);
        if (result is null)
        {
            // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        }
        // Jika ada data, maka akan mengembalikan response 200 OK.
        return Ok(new ResponseOKHandler<EmployeeDto>((EmployeeDto)result));
    }
    
    // Untuk menangani request POST dengan route api/[controller]
    [HttpPost]
    public IActionResult Create(CreateEmployeeDto createEmployeeDto)
    {
        try
        {
            Employee toCreate = createEmployeeDto;
            // Generate NIK baru dengan memanggil method Nik dari class GenerateHandler.
            toCreate.Nik = GenerateHandler.Nik(_employeeRepository.GetLastNik()); 

            // Membuat data baru di database.
            var result = _employeeRepository.Create(toCreate);

            // Setelah data berhasil dibuat, maka akan mengembalikan response 201 Created.
            return Ok(new ResponseOKHandler<EmployeeDto>("Data has been created successfully") { Data = (EmployeeDto)result });
        }
        catch (ExceptionHandler ex)
        {
            // Jika terjadi error, maka akan mengembalikan response 500 Internal Server Error.
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseServerErrorHandler("Failed to create data", ex.Message));
        }
    }
    
    // Untuk menangani request PUT dengan route api/[controller]
    [HttpPut]
    public IActionResult Update(EmployeeDto employeeDto)
    {
        try
        {
            // Mengambil data dari database berdasarkan guid.
            var entity = _employeeRepository.GetByGuid(employeeDto.Guid);
            if (entity is null)
            {
                // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
            
            Employee toUpdate = employeeDto;
            toUpdate.Nik = entity.Nik; // NIK tidak boleh diubah
            toUpdate.CreatedDate = entity.CreatedDate; // Menyalin CreatedDate dari entity yang diambil dari database.

            _employeeRepository.Update(toUpdate);

            // Setelah data berhasil diubah, maka akan mengembalikan response 200 OK.
            return Ok(new ResponseOKHandler<string>("Data has been updated successfully"));
        }
        catch (ExceptionHandler ex)
        {
            // Jika terjadi error, maka akan mengembalikan response 500 Internal Server Error.
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseServerErrorHandler("Failed to update data", ex.Message));
        }
    }
    
    // Untuk menangani request DELETE dengan route api/[controller]/guid
    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        try
        {
            // Menghapus data dari database berdasarkan guid.
            var entity = _employeeRepository.GetByGuid(guid);
            if (entity is null)
            {
                // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }

            _employeeRepository.Delete(entity);
            
            // Setelah data berhasil dihapus, maka akan mengembalikan response 200 OK.
            return Ok(new ResponseOKHandler<string>("Data has been deleted successfully"));
        }
        catch (ExceptionHandler ex)
        {
            // Jika terjadi error, maka akan mengembalikan response 500 Internal Server Error.
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseServerErrorHandler("Failed to delete data", ex.Message));
        }
    }
}